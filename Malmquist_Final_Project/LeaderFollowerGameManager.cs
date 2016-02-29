
using UnityEngine;
using System.Collections;

//add using System.Collections.Generic; to use the generic list format
using System.Collections.Generic;

public class LeaderFollowerGameManager : MonoBehaviour {

    public GameObject[] obstacles;

	public GameObject[] Obstacles
	{
		get{ return obstacles;}
	}

	private Vector3 centroid;

	public Vector3 Centroid
	{
		get{ return centroid;}
	}
	
	private Vector3 followerDirection;

	public Vector3 FollowerDirection
	{
		get{ return followerDirection;}
	}

	public GameObject[] followers;

    public GameObject gameManager;

	public int numberFollowers;
    public string obstacleTagStr;
	void Start () 
    {		
        //# of Followers
        numberFollowers = followers.Length;
        if (obstacleTagStr != "")
        {
            obstacles = GameObject.FindGameObjectsWithTag(obstacleTagStr);
        }

	}
	

	void Update () 
    {
		
	}


    private void CalcCentroid() 
    {
		//sum the positons of the flock
		Vector3 totalPosition = Vector3.zero;
		for (int i = 0; i < followers.Length; i++) 
		{
			totalPosition += followers[i].transform.position;
		}

		centroid = totalPosition / followers.Length;
    }

    private void CalcFlockDirection() 
    {
        Vector3 totalDirection = Vector3.zero;
        for (int i = 0; i < followers.Length; i++)
        {
            totalDirection += followers[i].transform.forward;
        }

        followerDirection = totalDirection.normalized;
        Debug.Log("Flock Direction: " + followerDirection);
    }


}
