using UnityEngine;
using System.Collections;

public class Farmer_Billboard : MonoBehaviour 
{
	public Camera camera;

	void Update () 
	{
		transform.LookAt(transform.position + camera.transform.rotation * Vector3.back,
		                 camera.transform.rotation * Vector3.up);
	}
}
