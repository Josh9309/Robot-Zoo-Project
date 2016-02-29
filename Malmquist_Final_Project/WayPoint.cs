using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour {
    //attributes
    public float range = 5.0f;
    public bool startPoint = false;
    public int wayPointNum;
    public int nextPoint;
    protected Vector3 postition;

	// Use this for initialization
	void Start () {
        postition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
