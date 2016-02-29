using UnityEngine;
using System.Collections;

public class Follower : Vehicle {
    public GameObject Leader;

    //Weighting!!!!!!!!!!!!!!!!!
    private Vector3 ultimateForce;
    public float safeDistance = 10.0f;
    public float followLeaderWeight = 75.0f;
    public float seperationWeight = 50.0f;
    public float evadeWeight = 75.0f;

    public float evadeDistance = 10.0f;

    // Use this for initialization
    void Start()
    {
        base.Start();
        ultimateForce = Vector3.zero;
    }

    protected override void CalcSteeringForces()
    {
        ultimateForce = Vector3.zero;

        ultimateForce += followLeader(Leader, evadeWeight) * followLeaderWeight;
        ultimateForce += Seperation(safeDistance) * seperationWeight;

        //evade obstacles
        for (int i = 0; i < GM.Obstacles.Length; i++)
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

            if (isInLeaderSight(GM.Obstacles[i], ahead))
            {
                Debug.DrawLine(transform.position, obstaclePos, Color.black);
                ultimateForce += Evade(obstaclePos, obstacleVelocity) * evadeWeight;
            }

        }

        //limit the ultimate steering force
        ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);

        //apply that acceleration (applyForce())
        ApplyForce(ultimateForce);


    }
}
