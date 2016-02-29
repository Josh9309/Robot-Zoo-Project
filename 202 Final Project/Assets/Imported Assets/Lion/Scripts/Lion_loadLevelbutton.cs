using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lion_loadLevelbutton : MonoBehaviour {



	public int loadLevel;






	public void LoadLevel()
	{
		Application.LoadLevel(loadLevel);
		Debug.Log ("Button was pressed");

	}







}
