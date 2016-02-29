using UnityEngine;
using System.Collections;

public class Lion_Effect : MonoBehaviour 
{


	public GameObject Glow_InPrefab;
	public GameObject Glow_OutPrefab;
	public GameObject GlowPrefab;


	void Start()
	{
		Glow_InPrefab.SetActive(false);
		Glow_OutPrefab.SetActive(false);
		GlowPrefab.SetActive(false);
	}



	void Glow_In()
	{
		Glow_InPrefab.SetActive(true);


	}

	
	void Glow_Out()
	{
		Glow_OutPrefab.SetActive(true);
		
	}

	
	void Glow()
	{
		GlowPrefab.SetActive(true);

		
	}

	void Destroy()
	{
		Glow_InPrefab.SetActive(false);
		Glow_OutPrefab.SetActive(false);
		GlowPrefab.SetActive(false);
	}

}
