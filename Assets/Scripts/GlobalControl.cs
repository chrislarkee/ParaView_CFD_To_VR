using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {

	public static int? leftUserSelection = null;

	private GameObject start;
	private Quaternion initRotation;
	private Vector3 initPosition;

	void Start (){
		DontDestroyOnLoad (this);
		start = this.gameObject;
		initRotation = start.transform.rotation;
		initPosition = start.transform.position;
		Invoke ("loadLevel", 0.1f);
	}

	void loadLevel(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}

	void Update () {
		if (OVRInput.GetUp(OVRInput.Button.Back))
			activateJump();

		if (OVRInput.GetUp (OVRInput.Button.Start, OVRInput.Controller.Gamepad))
			activateJump();
	}

	void activateJump(){
		//go to the menu.
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex != 1) {
			//reset stuff for the return to the menu
			start.transform.position = initPosition;
			start.transform.rotation = initRotation;
			Destroy(GameObject.Find("HUD"));
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
		}
		
	}

}
