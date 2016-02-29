using UnityEngine;
using System.Collections;

public class PathFollower : Vehicle {
    
    public GameObject Path;
    public GameObject currentPoint;
    private WayPoint cp;
    
    //Weighting!!!!!!!!!!!!!!!!!
    private Vector3 ultimateForce;

	// Use this for initialization
	void Start () {
        //currentPoint = Path.GetComponent<Path>().startNode;
        cp = currentPoint.GetComponent<WayPoint>();
        base.Start();
        ultimateForce = Vector3.zero;
	}

    protected override void CalcSteeringForces()
    {
        ultimateForce = Vector3.zero;
        
        //Check Current Point
        CheckCurrentPoint();

        //call PathFollow using currentPoint;
        ultimateForce = PathFollow(Path, cp.wayPointNum);
        //ultimateForce = Arrive(currentPoint.transform.position, 10.0f);
        //limit the ultimate steering force
        ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);

        //apply that acceleration (applyForce())
        ApplyForce(ultimateForce);

        //Debug.DrawLine(transform.position, velocity, Color.green);
    }

    private void CheckCurrentPoint()
    {
        cp = currentPoint.GetComponent<WayPoint>();
        //Check if Vehicle is within range of that point
        Vector3 VecToCenter = currentPoint.transform.position - transform.position;
        float dist = VecToCenter.magnitude;
        if (dist < cp.range)
        {
            //Get next Point To seek
            Path thePath = Path.GetComponent<Path>();
            for (int p = 0; p < thePath.pathWayPoints.Length; p++)
            {
                WayPoint NextPoint = thePath.pathWayPoints[p].GetComponent<WayPoint>();
                if (NextPoint.wayPointNum == cp.nextPoint)
                {
                    currentPoint = thePath.pathWayPoints[p];
                }
            }
        }
        else
        {
            //Do Nothing
        }
    }

    /*
     * //Check if Vehicle is within range of that point
                Vector3 VecToCenter = point.transform.position - transform.position;
                float dist = VecToCenter.magnitude;
                if (dist < point.range)
                {
                    //Get next Point To seek
                    for(int p = 0; p < thePath.pathWayPoints.Length; p++)
                    {
                        WayPoint NextPoint = thePath.pathWayPoints[p].GetComponent<WayPoint>();
                        if (NextPoint.wayPointNum == point.nextPoint)
                        {
                            ultimatePathFollowForce = Seek(NextPoint.transform.position);
                        }
                    }
                }
                else
                {
                    //Seek current point
                    ultimatePathFollowForce = Seek(point.transform.position);
                }
     * */
}
