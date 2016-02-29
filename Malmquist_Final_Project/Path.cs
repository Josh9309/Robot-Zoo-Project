using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
    //attributes
    public string PathName;
    public int numPoints; //number of way points
    public GameObject startNode; //which waypoint do you start at
    public GameObject[] pathWayPoints;

	// Use this for initialization
	void Start () 
    {
        //get the wayPoints and set the start node in the path
        for (int i = 0; i < pathWayPoints.Length; i++)
        {
            WayPoint point = pathWayPoints[i].GetComponent<WayPoint>();
            Debug.Log(point.wayPointNum);
            if (point.startPoint == true)
            {
                Debug.Log(PathName + " Start point is: " + point.wayPointNum);
                startNode = pathWayPoints[i];
            }
        }
 
        //*DEBUG CHECK WHICH POINTS ARE IN PATHWAYPOINTS ARRAY
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
