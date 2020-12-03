using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateHud : MonoBehaviour {

	private Transform head;
	public float threshold = 60f;	//how big of a difference do we need to trigger reset of the HUD angle?

	// Use this for initialization
	void Start () {
		head = GameObject.Find("CenterEyeAnchor").transform;
		InvokeRepeating("fixHUD", 2f, 2f);
        //InvokeRepeating("fixHUD", 0f, 0f);
    }

	void fixHUD(){
		//compare the vectors
		if (Mathf.Abs (head.eulerAngles.y - transform.eulerAngles.y) > threshold) {
			//rotate the HUD
			Vector3 newRotation = new Vector3(transform.eulerAngles.x, head.eulerAngles.y, transform.eulerAngles.z);
			transform.eulerAngles = newRotation;
		}
	}
}
