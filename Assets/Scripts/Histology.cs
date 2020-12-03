using UnityEngine;
using System.Collections;

public class Histology : MonoBehaviour {

	public Texture2D MyTexture;
	public GameObject histologyStack;
	private MeshRenderer histologyStackR;

	// Use this for initialization
	void Start () {
		histologyStackR = histologyStack.GetComponent<MeshRenderer> ();
	}

	void OnTriggerEnter () {
		//notify??
		histologyStackR.material.mainTexture = MyTexture;
		Debug.Log ("Switching histology image to " + MyTexture.name);
	}

	void OnTriggerExit () {
	
	}



}
