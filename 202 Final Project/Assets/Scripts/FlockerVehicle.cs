using UnityEngine;
using System.Collections;

//use the Generic system here to make use of a Flocker list later on
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

abstract public class FlockerVehicle : MonoBehaviour {

	//no-position - transform.position will be used instead
	protected Vector3 acceleration;
	protected Vector3 velocity;
	protected Vector3 desired;

	public Vector3 Velocity
	{
		get{ return velocity; }
	}

	public float maxSpeed = 6.0f;
	public float maxForce = 12.0f;
	public float mass = 1.0f;
	public float radius = 1.0f;
    public float Leader_Behind_Dist = 10.0f;
    public float slowingRadius = 10.0f;

	CharacterController charControl;

	//access to GameManager script
	protected FlockerGameManager fgm;


	virtual public void Start()
	{
		acceleration = Vector3.zero;
		velocity = transform.forward;
		desired = Vector3.zero;

		//store assets to character controller component
		charControl = GetComponent<CharacterController> ();

		fgm = GameObject.Find ("GameManagerGO").GetComponent<FlockerGameManager> ();
	}

	
	// Update is called once per frame
	protected void Update () {
		//calculate all necessary steering forces
		CalcSteeringForces();

		//add accel to velocity
		velocity += acceleration * Time.deltaTime;
		velocity.y = 0; //keeps us on the same plane

		//limit velocity to max speed
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);

		transform.forward = velocity.normalized;

		//move the character based on velocity
		charControl.Move (velocity * Time.deltaTime);

        
		//reset acceleration
		acceleration = Vector3.zero;


	}

    abstract protected void CalcSteeringForces();

    protected void ApplyForce(Vector3 steeringForce) 
    {
		acceleration = acceleration + (steeringForce / mass);
    }

    protected Vector3 Seek(Vector3 targetPosition) 
    {
		desired = targetPosition- transform.position;
		desired = desired.normalized * maxSpeed;
		desired -= velocity;
		desired.y = 0;
		return desired;
    }

    protected Vector3 Arrive(Vector3 targetPosition, float slowRadius)
    {
        //calculate the desired velocity
        desired = targetPosition - transform.position;
        float dist = desired.magnitude;

        //check the distance to detect wther the character is inside the slowing area
        if (dist < slowRadius)
        { 
            //inside slowing area
            desired = desired.normalized * maxSpeed * (dist / slowRadius);
        }
        else
        {
            //outside slowing area
            desired = desired.normalized * maxSpeed;
        }
       
        desired -= velocity;
        desired.y = 0;
        return desired;
    }

	protected Vector3 Flee(Vector3 targetPosition)
	{
		return -Seek(targetPosition);
	}

    protected Vector3 Evade(Vector3 targetPosition, Vector3 targetVelocity)
    {
        Vector3 distance = targetPosition - transform.position;
        float futureFrames = distance.magnitude / maxSpeed;
        Vector3 futurePosition = targetPosition + targetVelocity * futureFrames;
        return Flee(futurePosition);
    }

    protected Vector3 followLeader(GameObject leader)
    {
        Vector3 leadVelocity = leader.GetComponent<Vehicle>().Velocity;
        Vector3 ultimateLeaderFollowForce = Vector3.zero;
        Vector3 behind = Vector3.zero;

        //calculate behind point
        leadVelocity.Scale(new Vector3(-1, -1, -1));
        leadVelocity.Normalize();
        leadVelocity.Scale(new Vector3(Leader_Behind_Dist, Leader_Behind_Dist, Leader_Behind_Dist));
        behind = leader.transform.position + leadVelocity;

        //DEBUG CHECK BEHIND POINT
        Debug.DrawLine(transform.position, behind, Color.magenta);

        //Creates a force to arrive at the behind point
        ultimateLeaderFollowForce += Arrive(behind, slowingRadius);

        //ultimateLeaderFollowForce += Seperation(10.0f);
        ultimateLeaderFollowForce.y = 0;
        return ultimateLeaderFollowForce;
    }
    protected Vector3 AvoidObstacle(GameObject obst, float safeDistance)
    {
		Vector3 vecToCenter = obst.transform.position - transform.position;
		float obstRadius = obst.GetComponent<ObstacleScript> ().Radius;

        vecToCenter.y = 0;

		if (vecToCenter.magnitude > safeDistance) 
		{
			return Vector3.zero;
		}

		if (Vector3.Dot (vecToCenter, transform.forward) < 0) 
		{
			return Vector3.zero;
		}

		if (Mathf.Abs (Vector3.Dot (vecToCenter, transform.right)) > (radius + obstRadius)) 
		{
			return Vector3.zero;
		}

        desired = Vector3.zero;
        if (Vector3.Dot(vecToCenter, transform.right) < 0)
        {
            desired = transform.right * maxSpeed;
            Debug.DrawLine(transform.position, obst.transform.position, Color.red);
        }
        else
        {
            desired = transform.right * -maxSpeed;
            Debug.DrawLine(transform.position, obst.transform.position, Color.green);
        }

        return desired;
    }

    protected Vector3 PathFollow(GameObject path, int currentPoint)
    {
        Vector3 ultimatePathFollowForce = Vector3.zero;
        //Get The Path
        Path thePath = path.GetComponent<Path>();
        //find Current Path Point to seek
        for (int i = 0; i < thePath.pathWayPoints.Length; i++)
        {
            WayPoint point = thePath.pathWayPoints[i].GetComponent<WayPoint>();
            if(point.wayPointNum == currentPoint )
            {
                ultimatePathFollowForce = Seek(thePath.pathWayPoints[i].transform.position);
                Debug.DrawLine(transform.position, thePath.pathWayPoints[i].transform.position, Color.white);
            }
        }

        
        return ultimatePathFollowForce;
    }

    public Vector3 Seperation(float separationDistance) 
    {
		List<GameObject> tooCloseList = new List<GameObject> ();
		for (int i = 0; i < fgm.Flock.Count; i++) 
		{
			float dist = Vector3.Distance(transform.position, fgm.Flock[i].transform.position);
			if(dist == 0)
			{
				//do nothing
			}
			else if(dist < separationDistance)
			{
                Debug.DrawLine(transform.position, fgm.Flock[i].transform.position, Color.yellow);
				tooCloseList.Add(fgm.Flock[i]);
			}

		}

		Vector3 ultimateSeparationForce = Vector3.zero;

        //Debug.Log(tooCloseList.Count);
		for (int i =0; i < tooCloseList.Count; i++) 
		{
			Vector3 seperationForce = Flee(tooCloseList[i].transform.position).normalized;
            //Debug.Log(seperationForce);
			float dist = Vector3.Distance(transform.position, tooCloseList[i].transform.position);

			ultimateSeparationForce += (seperationForce * (1/dist));
		} 

		ultimateSeparationForce = ultimateSeparationForce.normalized;
		ultimateSeparationForce = ultimateSeparationForce * maxSpeed;
		ultimateSeparationForce -= velocity;
		return ultimateSeparationForce;
    }

    public Vector3 Alignment(Vector3 alignVector) 
    {
        desired = alignVector * maxSpeed;
        return (desired - velocity);
    }

    public Vector3 Cohesion(Vector3 cohesionVector) 
    {
        return Seek(cohesionVector);
    }

    public Vector3 StayInBounds(float radius, Vector3 center) 
    {
        throw new System.NotImplementedException();
    }

}
