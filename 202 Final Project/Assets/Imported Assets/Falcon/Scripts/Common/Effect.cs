using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour 
{


	public GameObject wood;



	void Start()
	{
		wood.SetActive(false);

	}



	void Wood()
	{
		wood.SetActive(true);


	}



	void Destroy()
	{
		wood.SetActive(false);

	}

}
