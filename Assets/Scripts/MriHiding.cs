using UnityEngine;
using System.Collections;

public class MriHiding : MonoBehaviour {

	public static GameObject CameraObject;
	private float StartFadeDistance = 6;
	private float StopFadeDistance = 8;

	private float distanceCurrent;
	private float distanceCached = 0;

	void Start () {
		if (CameraObject == null) 
			CameraObject = GameObject.Find ("HeadNode");
	}

	void Update () {
		//calculate the distance
		distanceCurrent = Mathf.Abs(CameraObject.transform.position.z - transform.position.z);

		//if it hasn't changed since the last frame, skip the material change, for performance
		if (distanceCurrent != distanceCached) {
			MRIfade();
		}
	
		//save the current value for next time
		distanceCached = distanceCurrent;
	}
	
	void MRIfade() {
		//set the alpha according to the distance
		// < start should be 0, > end should be 0.75

		if (distanceCurrent < StartFadeDistance) {
			//very near objects are invisible
			GetComponent<Renderer>().enabled = false;
		} else if (distanceCurrent >= StartFadeDistance && distanceCurrent <= StopFadeDistance) {
			//middle objects are fading
			GetComponent<Renderer>().enabled = true;
			float alphaFactor = (0.78f / (StopFadeDistance - StartFadeDistance) * distanceCurrent) - 2.34f;
			GetComponent<Renderer>().material.color = new Color(0.78f, 0.78f, 0.78f, alphaFactor);
		} else {
			//far objects have alpha of 0.75
			GetComponent<Renderer>().material.color = new Color(0.78f, 0.78f, 0.78f, 0.78f);
		}
	}

}
