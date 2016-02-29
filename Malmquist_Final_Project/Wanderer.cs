using UnityEngine;
using System.Collections;

public class Wanderer : Vehicle {

    //Weighting!!!!
    private Vector3 ultimateForce;
    public float safeDistance = 10.0f;
    public float avoidWeight = 100.0f;
    public float wanderWeight = 50.0f;
    public float evadeWeight = 100.0f;
    
    //Wander Method Stuff
    public float circle_distance = 0;
    public float circleRadius = 10.0f;
    public float wanderAngle;
    public float angleChange;

    public float evadeDistance = 10.0f;

    //boundaries Stuf
    public float minX = 20.2f;
    public float maxX = 29.7f;
    public float minZ = -13f;
    public float maxZ = 6.7f;

    public float WanderAngle
    {
        get { return wanderAngle; }
        set
        {
            wanderAngle = value;
        }
    }

    public float AngleChange
    {
        get { return angleChange; }
        set
        {
            angleChange = value;
        }
    }

    private Vehicle veh;
    // Use this for initialization
	void Start () {

        base.Start();
        wanderAngle = Random.Range(0, 360);
        ultimateForce = Vector3.zero;
        veh = GetComponent<Vehicle>();
        float i = veh.radius;
	}

    protected override void CalcSteeringForces()
    {
        ultimateForce = Vector3.zero;

        //Call the Wander 

        if (inbounds() == true)
        {

            ultimateForce += Wander(this) * wanderWeight;
        }
        else
        {
            Debug.DrawLine(transform.position, gameManage.transform.position, Color.grey);
            ultimateForce += Seek(gameManage.transform.position);
        }

        //evade obstacles
        for (int i = 0; i < GM.Obstacles.Length ; i++)
        {
            Vector3 obstaclePos = GM.Obstacles[i].transform.position;
            Vector3 obstacleVelocity = Vector3.zero;
            if (GM.Obstacles[i].GetComponent<Vehicle>() != null)
            {
                obstacleVelocity = GM.Obstacles[i].GetComponent<Vehicle>().Velocity;
            }

            Vector3 ahead = Vector3.zero;

            //Calculate the ahead point
            obstacleVelocity.Normalize();
            obstacleVelocity.Scale(new Vector3(evadeDistance, evadeDistance, evadeDistance));
            ahead = obstaclePos + obstacleVelocity;

            if(isInLeaderSight(GM.Obstacles[i], ahead))
            {
                Debug.DrawLine(transform.position, obstaclePos, Color.black);
                ultimateForce += Evade(obstaclePos, obstacleVelocity) * evadeWeight;
            }

        }

            //ultimateForce += Seek(new Vector3(0, 0, 0));
            //limit the ultimate steering force
            ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);

        //apply that acceleration (applyForce())
        ApplyForce(ultimateForce);
	}

    private bool inbounds()
    {
        Debug.LogWarning("px: " + transform.localPosition.x);
        Debug.LogWarning("pz: " + transform.position.y);
        if (transform.localPosition.x > minX && transform.localPosition.x < maxX && transform.localPosition.z > minZ && transform.localPosition.z < maxZ)
        {
            Debug.LogWarning("true");
            return true;
        }
        else
        {
            Debug.LogWarning("false");
            return false;
        }
    }
}
