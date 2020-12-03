
using System.Collections;
using UnityEngine;
using UnityEngine.VR;

public class mouseMode : MonoBehaviour {

	private Ray ray;
	private RaycastHit hit;

    private GameObject HUD;
    private Transform HUDTransform;

	//Use this for initialization
	void Start () {	
		if (UnityEngine.XR.XRSettings.isDeviceActive == false) {
			GameObject centerAnchor = GameObject.Find ("CenterEyeAnchor");
			transform.parent = centerAnchor.transform;
			Debug.Log ("In mouse mode.");

            //NEW
            //HUD = GameObject.Find("HUD 1");
            //HUDTransform = HUD.transform;

            //HUDTransform.eulerAngles = new Vector3(28f, 0f, 0f);

		} else {
			Debug.Log ("In VR mode");
			this.enabled = false;

            //NEW
            HUD = GameObject.Find("HUD 1");
            HUDTransform = HUD.transform;

            HUDTransform.eulerAngles = new Vector3(-7.2f, 0f, 0f);
        }
	}

	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			transform.LookAt(hit.point);
			//Debug.Log ("Mouse cursor hit " + hit.transform.name);
		}
	}
}
