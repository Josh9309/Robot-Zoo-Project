
using UnityEngine;
using System.Collections;

//add using System.Collections.Generic; to use the generic list format
using System.Collections.Generic;

public class FlockerGameManager : MonoBehaviour {
    public GameObject dude;
    public GameObject target;

    public GameObject dudePrefab;
    public GameObject targetPrefab;
    public GameObject obstaclePrefab;

    private GameObject[] obstacles;

	public GameObject[] Obstacles
	{
		get{ return obstacles;}
	}

	private Vector3 centroid;

	public Vector3 Centroid
	{
		get{ return centroid;}
	}
	
	private Vector3 flockDirection;

	public Vector3 FlockDirection
	{
		get{ return flockDirection;}
	}

	private List<GameObject> flock;

	public List<GameObject> Flock 
    {
		get{ return flock;}
	}

    public GameObject gameManager;

	public int numberFlockers;
	
	void Start () 
    {
        gameManager = GameObject.Find("GameManagerGO");

		flock = new List<GameObject> ();
		//make the noodle target
		Vector3 pos = new Vector3 (-136.7f, 4, -161.5f);
		target =  (GameObject)Instantiate (targetPrefab, pos, Quaternion.identity);
		//target =  Instantiate (targetPrefab, pos, Quaternion, identity) as GameObject; Another way to cast as gameobject

		//make dude at 10, 1, 10 no rotation
		Vector3 dudePos = new Vector3 (-126.7f, 0, -151.5f);
		dude = (GameObject)Instantiate (dudePrefab, dudePos, Quaternion.identity);
		flock.Add (dude);

		//create multiple flockers
		numberFlockers = 10;

		for (int i =0; i< numberFlockers; i++) 
		{
			Vector3 flockDudePos = new Vector3(Random.Range(-156.7f, -116.7f), 0, Random.Range(-181.5f, -141.5f));
			GameObject flockDude = (GameObject)Instantiate (dudePrefab, flockDudePos, Quaternion.identity);
			flock.Add(flockDude);
		}

        //calculate centroid
        CalcCentroid();
        gameManager.transform.position = centroid;

        //calculate fleeDirection
        gameManager.transform.forward = flockDirection;

		//set cameras target
        CameraManager CM = GameObject.Find("Camera Game Manager").GetComponent<CameraManager>();
        CM.cameras[0].GetComponent<SmoothFollow>().target = gameManager.transform;

		//set seeker target
		dude.GetComponent<Flocker> ().seekerTarget = target;

		for (int i =0; i< flock.Count; i++) 
		{
			flock[i].GetComponent<Flocker> ().seekerTarget = target;
		}

		//create obstacles and place them in the obstavles array
		for (int i = 0; i < 20; i++) 
		{
			pos = new Vector3(Random.Range(-196.7f, -76.7f),0f,Random.Range(-221.5f,-101.5f));
			Quaternion rot = Quaternion.Euler(0, Random.Range(0,180), 0);
			Instantiate(obstaclePrefab, pos, rot);
		}

		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
        Debug.Log(Obstacles[0].transform.position);

	}
	

	void Update () 
    {
		//compare the distance between the guy and noodle
		//move the noodle if it's close
		for (int i = 0; i < flock.Count; i++) 
		{
			float dist = Vector3.Distance (target.transform.position, flock [i].transform.position);

			//randomize the target's distance
			if (dist < 5f) {
				do {
                    target.transform.position = new Vector3(Random.Range(-196.7f, -76.7f), 4f, Random.Range(-221.5f, -101.5f));
				} while(NearAnObstacle());
			}
            
		}

		CalcCentroid ();
        CalcFlockDirection();
        gameManager.transform.position = centroid;
        gameManager.transform.forward = flockDirection;
	}

    bool NearAnObstacle() 
    {
        //go throught all obstacles and compare the distance between each obstacle and noodle
		//if the noodle is within a 4 unit distance of the noodle return true
		for (int i =0; i< obstacles.Length; i++) 
		{
			if(Vector3.Distance(target.transform.position, obstacles[i].transform.position) < 5.0f)
			{
				return true;
			}
		}
		//otherwise the noodle is not near an obstacle
		return false;
    }

    private void CalcCentroid() 
    {
		//sum the positons of the flock
		Vector3 totalPosition = Vector3.zero;
		for (int i = 0; i < flock.Count; i++) 
		{
			totalPosition += flock[i].transform.position;
		}

		centroid = totalPosition / flock.Count;
    }

    private void CalcFlockDirection() 
    {
        Vector3 totalDirection = Vector3.zero;
        for (int i = 0; i < flock.Count; i++)
        {
            totalDirection += flock[i].transform.forward;
        }

        flockDirection = totalDirection.normalized;
        Debug.Log("Flock Direction: " + flockDirection);
    }


}
