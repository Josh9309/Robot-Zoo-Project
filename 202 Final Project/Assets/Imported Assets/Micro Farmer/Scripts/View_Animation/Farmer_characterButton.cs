using UnityEngine;
using System.Collections;

public class Farmer_characterButton : MonoBehaviour {

	public GameObject frog;
	public GUISkin customSkin;

	
	
	private Rect FpsRect ;
	private string frpString;


	

	void Start () 
	{
	
			}
	
 void OnGUI() 
	{
		GUI.skin = customSkin;

		GUI.Box (new Rect (0, 0, 440, 80),"");
		
		if (GUI.Button(new Rect(30, 20, 70, 30),"Idle")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("MF_Idle");
	  }
//		if (GUI.Button(new Rect(105, 20, 70, 30),"Walk")){
//		 frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("SKG_Walk");
//	  }
		if (GUI.Button(new Rect(105, 20, 70, 30),"Talk")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("MF_Talk");
	  }
		
		if (GUI.Button(new Rect(180, 20, 70, 30),"Walk")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("MF_Walk");
	  }
		 if (GUI.Button(new Rect(255, 20, 70, 30),"Work")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("MF_Work");
			//effect.animation.CrossFade ("test");
	  }
	    if (GUI.Button(new Rect(330, 20, 70, 30),"Beg")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("MF_Beg");
			//effect.animation.CrossFade ("test");
	  }
//		   if (GUI.Button(new Rect(405, 20, 70, 30),"Attack00")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Attack00");
//	  }
//
//	     if (GUI.Button(new Rect(480, 20, 70, 30),"Attack01")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Attack01");
//	  } 
//		if (GUI.Button(new Rect(555, 20, 70, 30),"Combo1_1")){
//		  frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_1");
//	  }
//		if (GUI.Button(new Rect(630, 20, 70, 30),"Combo1_2")){
//		  frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_2");
//
//
//
//	  }
//		if (GUI.Button(new Rect(705, 20, 70, 30),"Combo1_3")){
//		 frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_3");
//	  }
//		
//		if (GUI.Button(new Rect(780, 20, 70, 30),"Skill")){
//		 frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Skill");
//	  }
//		if (GUI.Button(new Rect(30, 60, 70, 30),"Buff")){
//		 frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Buff");
//	  }
//		
//		if (GUI.Button(new Rect(105, 60, 70, 30),"Down")){
//		 frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Down");
//	  }
//		
//		
//		
//	    if (GUI.Button(new Rect(180, 60, 70, 30),"Up")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Up");
//	
//	  }
//		if (GUI.Button(new Rect(255, 60, 70, 30),"Stun")){
//			frog.animation.wrapMode= WrapMode.Loop;
//			frog.animation.CrossFade("CG_Stun");
//			
//		}
//		if (GUI.Button(new Rect(330, 60, 70, 30),"Sleep")){
//			frog.animation.wrapMode= WrapMode.Loop;
//			frog.animation.CrossFade("CG_Sleep");
//			
//		}
//		if (GUI.Button(new Rect(405, 60, 70, 30),"BackStep")){
//			frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_BackStep");
//			
//		}
//		   if (GUI.Button(new Rect(480, 60, 70, 30),"Damage")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Damage");
//	  }
//			   if (GUI.Button(new Rect(555, 60, 70, 30),"Damage1")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Damage1");
//	  }
//			   if (GUI.Button(new Rect(630, 60, 70, 30),"Death")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Death");
//	  }
	    
				if (GUI.Button (new Rect (20, 580, 140, 40), "Ver 1.0")) {
						frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
						frog.GetComponent<Animation>().CrossFade ("MF_Idle");
				}

	
		
 }
	
	// Update is called once per frame
	void Update () 
	{
		
	
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

	}





	
}
