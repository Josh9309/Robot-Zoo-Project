using UnityEngine;
using System.Collections;

public class Seeker : Vehicle {

	public GameObject seekerTarget;

	//Weighting!!!!!!!!!!!!!!!!!
	public float seekWeight = 75.0f;
	public float safeDistance = 10.0f;
	public float avoidWeight = 100.0f;
    public float cohesionWeight = 25.0f;
    public float alignWeight = 25.0f;
	public float seperationWeight = 50.0f; 
	private Vector3 ultimateForce;
    

	// Call Inherited Start and then do our own
	override public void Start () 
	{
		base.Start();
		ultimateForce = Vector3.zero;
	}

    protected override void CalcSteeringForces()
    {
		ultimateForce = Vector3.zero;
		//get a seeking force (based on char movement - for now, just seek
		//add that seeking force to the ultimate steering force
		ultimateForce += Seek (seekerTarget.transform.position) * seekWeight;
        for (int i = 0; i < gm.Obstacles.Length; i++)
        {
            ultimateForce += AvoidObstacle(gm.Obstacles[i], safeDistance) * avoidWeight;
        }
        ultimateForce += Cohesion(gm.Centroid) * cohesionWeight;
        ultimateForce += Alignment(gm.FollowerDirection) * alignWeight;
		ultimateForce += Seperation (10.0f) * seperationWeight;
            
        //limit the ultimate steering force
        ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);
		
        //apply that acceleration (applyForce())
		ApplyForce (ultimateForce);

        
    }
	

}
