using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    ///<summary>
    /// Game Cameras for the player to switch between
    /// The second camera is coded as a follow the guy camera
    /// </summary>
    public Camera[] cameras;
    public GameObject flockerGM;
    public GameObject fpc;
    private FlockerGameManager flockGM;
    private int currentCameraIndex;

	// Use this for initialization
	void Start () 
    {
        flockGM = flockerGM.GetComponent<FlockerGameManager>();
        currentCameraIndex = 0;

        //Turn All cameras off except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        //if any cameras were added to the controller, enable the first one
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
            //Write to the console which camera is enabled
            Debug.Log("Camera: " + cameras[0].name + ", is enabled");
        }

        fpc.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex++;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (currentCameraIndex == 5)
            {
                cameras[5].transform.localRotation = Quaternion.Euler(90, 270, 0);
            }
            if (currentCameraIndex < cameras.Length)
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                if (currentCameraIndex - 1 == 4)
                {
                    fpc.SetActive(false);
                }
                if (currentCameraIndex == 4)
                {
                    fpc.SetActive(true);
                }
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].name + ", is now enabled");
            }
            else
            {
                cameras[currentCameraIndex - 1].gameObject.SetActive(false);
                if (currentCameraIndex-1 == 4)
                {
                    fpc.SetActive(false);
                }
                currentCameraIndex = 0;
                cameras[currentCameraIndex].gameObject.SetActive(true);
                Debug.Log("Camera with name: " + cameras[currentCameraIndex].name + ", is now enabled");
            }
        }
	}
}
