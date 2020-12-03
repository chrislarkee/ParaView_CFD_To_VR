using UnityEngine;
using System.Collections;

public class attachToHead : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject head = GameObject.Find ("OVRCameraRig");

		//store local offset?
		Vector3 originalPos = transform.position;
		Quaternion originalRot = transform.rotation;

		//connect to the tracked camera rig, then move the rig to this position
		transform.parent = head.transform;
		transform.parent.position = originalPos;
		transform.parent.rotation = originalRot;

		//reset the offsets
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}
	

}
