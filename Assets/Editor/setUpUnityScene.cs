using UnityEngine;
using System.Collections;
using UnityEditor;


public class setUpUnityScene {
	[MenuItem ("Assets/Setup CFD Scene")]

	static void setupScene () {

		//Define each master gameobject
		GameObject master = GameObject.Find ("ParaView_CFD");
		GameObject masterArrows = GameObject.Find ("Arrows");
		GameObject masterMesh = GameObject.Find ("Mesh");
		GameObject masterMRI = GameObject.Find ("MRI");

		//Find every game object
		GameObject imagingButton = GameObject.Find ("Imaging");
		GameObject imagingPlusButton = GameObject.Find ("Imaging+");
		GameObject imagingMinusButton = GameObject.Find ("Imaging-");

		//create a variable for the transforms
		Transform arrowsTransform;
		Transform meshTransform;
		Transform imagingTransform;




		//Used to apply the correct script on each gameobject - catch just in case the master object does not exist
		//finds the Master FBX file
		if (master == false) {
			Debug.LogWarning ("The FBX file is not found, and the script cannot continue.");
			return;
		} else {
			master.AddComponent<MasterControl> ();
		}

		//Finds the Arrows game object and tags all of its children
		if (masterArrows == false) {
			Debug.Log ("Arrows do not exist");
		} else {
			master.AddComponent<animateGlyphs> ();
			arrowsTransform = masterArrows.transform;

			int arrowsChildren = arrowsTransform.childCount;
			for (int i = 0; i < arrowsChildren; ++i) {
				arrowsTransform.GetChild (i).tag = "Arrows";
			}
		}

		//Finds the Mesh game object and tags all of its children
		if (masterMesh == false) {
			Debug.Log ("Mesh does not exist");
		} else {

			meshTransform = masterMesh.transform;

			int meshChildren = meshTransform.childCount;
			for (int i = 0; i < meshChildren; ++i) {
				meshTransform.GetChild (i).tag = "Mesh";
			}
		}

		//Finds the MRI game object and tags all of its children - turns off buttons if no MRI game object is found
		if (masterMRI == false) {
			Debug.Log ("Imaging does not exist");

			imagingButton.SetActive (false);
			imagingPlusButton.SetActive (false);
			imagingMinusButton.SetActive (false);
		} else {
			masterMRI.AddComponent<MRSlices> ();
			
			imagingTransform = masterMRI.transform;

			int imagingChildren = imagingTransform.childCount;
			for (int i = 0; i < imagingChildren; ++i) {
				imagingTransform.GetChild (i).tag = "Imaging";
			}

			//BELOW is used to apply custom shader to every MRI slice 
			//Find MRI parent object and its transform
			GameObject MRI = GameObject.Find ("MRI");
			Transform mriTransform = MRI.transform;

			//create a transform array that is as long as how many children are in the MRI
			Transform[] children = new Transform[mriTransform.childCount];

			//Put each children object in the array
			for (int i = 0; i <mriTransform.childCount; i++) {
				children [i] = mriTransform.GetChild(i);
			}

			//finds the custom slices shader so the user can see both sides of the image
			Shader mriMaterial = Shader.Find ("Custom/Slices");

			//Applies the custom Slices shader to each object in the array
			foreach (Transform slice in children) {
				slice.GetComponent<Renderer> ().sharedMaterial.shader = mriMaterial;
			}

		}

		}
			


	//Things that script does

	//1) Import .fbx file
	//2) Pre-load everything
	//3) Put the Arrows and Mesh Parent Objects in the Hierarchy
	//4) Tag all the child objects (time steps) as either "Arrow" or "Mesh" or "Imaging"
	//5) assign annimateArrows script to the master parent game objects

}
