using UnityEngine;
using System.Collections;
//using MiddleVR_Unity3D;
//NEW GEAR VR CONTROLLS
using VRStandardAssets.Utils;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//universal state control. toggle things on and off in this script.
public class MasterControl : MonoBehaviour {

	//Find necessary game objects
	public GameObject masterArrow;
	public GameObject histology;
	public GameObject map;
	public GameObject masterMR;

	//used to put the wall indicies and their scales in a gameobject array to easy swap out one another
	public static int wallID;
	GameObject[] wallStates;
	private GameObject[] scaleStates;


    // Use this for initialization
    void Start () {
		//used to find Other Mesh game object and find how many children are in that object
		GameObject masterOtherMesh = GameObject.Find ("Other Mesh");
		Transform otherMeshTransform;
		otherMeshTransform = masterOtherMesh.transform;
		int otherMeshChildren = otherMeshTransform.childCount;

		wallID = 0;

		//Recognize that there is no child object in Other Mesh
		if (otherMeshChildren == 0) {
			//create a game object array for 1 wall indices and 1 scales
			wallStates = new GameObject[1];
			scaleStates = new GameObject[1];

			//find and cache the objects I'm going to need
			wallStates [0] = GameObject.Find ("Mesh");
			scaleStates[0] = GameObject.Find("Main Mesh Scale");
		}

		//Recognize that there is one child game object in Other Mesh
		if (otherMeshChildren == 1) {
			//create a game object array for 2 wall indices and 2 scales
			wallStates = new GameObject[2];
			scaleStates = new GameObject[2];

			//this is done so if the user has only one of TAWSS or OSI it still works
			//find and cache the objects I'm going to need
			wallStates [0] = GameObject.Find ("Mesh");
			wallStates [1] = GameObject.Find ("Mesh_TAWSS");
			if (wallStates [1] == null) {
				wallStates [1] = GameObject.Find ("Mesh_OSI");
			}
			toggleObject (wallStates [1]); //toggles off the OSI or TAWSS indice at the start

			//same thing is done for the scales
			scaleStates[0] = GameObject.Find("Main Mesh Scale");
			scaleStates[1] = GameObject.Find("First Other Mesh Scale");
			toggleObject(scaleStates[1]);
		}

		//Recognize that there is two child game objects in Other Mesh
		if (otherMeshChildren == 2) {
			//create a game object array for 3 wall indices and 3 scales
			wallStates = new GameObject[3];
			scaleStates = new GameObject[3];

			//find and cache the objects I'm going to need
			wallStates [0] = GameObject.Find ("Mesh");
			wallStates [1] = GameObject.Find ("Mesh_TAWSS");
			wallStates [2] = GameObject.Find ("Mesh_OSI");
			toggleObject (wallStates [1]); //toggles off TAWSS at the start
			toggleObject (wallStates [2]); //toggles off OSI at the start

			//Same thing is done for the scales
			scaleStates[0] = GameObject.Find("Main Mesh Scale");
			scaleStates[1] = GameObject.Find("First Other Mesh Scale");
			scaleStates[2] = GameObject.Find("Second Other Mesh Scale");
			toggleObject(scaleStates[1]);
			toggleObject(scaleStates[2]);
		}

		//find necessay game objects
		masterArrow = GameObject.Find ("Arrows");
		histology = GameObject.Find ("HistologyStack");
		map = GameObject.Find ("Map View");
		masterMR = GameObject.Find("MRI");
		
		//turn off Histology and MRI at start
		toggleObject(histology);
		toggleObject(masterMR);
    }
	
	void Update () {

		//Switch Wall textures and scales
		if (OVRInput.GetDown (OVRInput.Button.Four) || Input.GetKeyUp (KeyCode.Z)) {
			cycleWallTexture ();
		}		
			
		//Switch from animated arrows to Imaging
		if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyUp (KeyCode.M)) {
			toggleObject(masterArrow);
			toggleObject(masterMR);
		}
	
//		//switch from animated arrows to Histology
//		if (OVRInput.GetDown(OVRInput.Button.Three) || Input.GetKeyUp (KeyCode.J)) {
//			toggleObject (histology);
//			toggleObject (map);
//		}

    }

	//if Imaging button is pressed the MRI is toggled on or vis versa
    public void MRI()
    {
		if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            toggleObject(masterMR);
        }
    }
		
	//Cycle through the gameobject array to flip through the wall indice and their scales
	public void cycleWallTexture() {
		wallID++;
		if (wallID >= wallStates.Length) wallID = 0;

		foreach (GameObject go in wallStates){
			go.SetActive(false);
		}
		wallStates[wallID].SetActive(true);

		foreach (GameObject go in scaleStates){
			go.SetActive(false);
		}
		scaleStates[wallID].SetActive(true);

	}


	//toggle object script
	public void toggleObject(GameObject go) {
		if (go == null)
			return;

		if (go.activeInHierarchy) go.SetActive (false);
		else go.SetActive (true);
	}
		
}

