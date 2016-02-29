using UnityEngine;
using System.Collections;

public class Intro_GuiButton : MonoBehaviour {

	public GUISkin customSkin;


	void OnGUI() 
	{
		GUI.skin = customSkin;

		if (GUI.Button(new Rect(60, 400, 200, 40),"View Animation"))
		{
			Application.LoadLevel(1);
		}

		if (GUI.Button(new Rect(60, 445, 200, 40),"My Homepage"))
		{
			Application.OpenURL("http://www.crossroadkimys2848.w.pw");
		}

//		if (GUI.Button(new Rect(60, 490, 200, 40),"My Homepage"))
//		{
//			Application.OpenURL("http://www.crossroadkimys2848.w.pw");
//		}



	}





}
