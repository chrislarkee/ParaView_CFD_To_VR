using UnityEngine;
using System.Collections;

public class BlinkingMovement : MonoBehaviour {

	public float moveSpeed = 3f;
	public float turnIncrement = 45f;

	private Vector2 leftstick = Vector3.zero;
	private Vector2 rightstick = Vector3.zero;

	private GameObject head;
	private blinkingAction blink;
	private bool movementTriggered = false;
	public static bool movementLocked = true;

    //NEW GEAR VR CONTROLLS
    private Vector2 gearVrPad;
    //DONE

	// Use this for initialization
	void Start () {
		head = GameObject.Find ("CenterEyeAnchor");
		blink = GameObject.Find ("eyelid").GetComponent<blinkingAction> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		leftstick = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick) + OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

		//keyboard commands.
		if (Input.GetKey(KeyCode.W)) leftstick.y += 1.0f;
		if (Input.GetKey(KeyCode.A)) leftstick.x -= 0.8f;
		if (Input.GetKey(KeyCode.S)) leftstick.y -= 1.0f;
		if (Input.GetKey(KeyCode.D)) leftstick.x += 0.8f;

		if (leftstick.sqrMagnitude > 0.02f && !movementTriggered) {
			StartCoroutine (movement());
		}

		rightstick = OVRInput.Get (OVRInput.Axis2D.SecondaryThumbstick);
		if (rightstick.sqrMagnitude > 0.5f && !movementTriggered) {
			StartCoroutine (turning(rightstick));
		}			
	}

	IEnumerator movement(){
		movementTriggered = true;

		yield return new WaitForEndOfFrame ();

		StartCoroutine(blink.blink());
		while (movementLocked) {
			yield return new WaitForEndOfFrame ();
		}
			
		//movement must always be relative to the head's direction
		//convert the joystick's vector into a 3d vector of movement, aligned to the head.
		Vector3 vec3;
		float boost;
		while (leftstick.sqrMagnitude > 0.1f) {
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) != 0f)
				boost = (OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger) * 3f) + 1f;
			else if (OVRInput.Get (OVRInput.Button.PrimaryTouchpad))
				boost = 4f;
			else if (Input.GetKey (KeyCode.LeftShift))
				boost = 4f;
			else
				boost = 1f;

			vec3 = head.transform.TransformDirection (new Vector3 (leftstick.x, 0f, leftstick.y));
			transform.Translate (vec3 * moveSpeed * boost * Time.deltaTime, relativeTo: Space.World);
			yield return new WaitForEndOfFrame ();
		}

        movementTriggered = false;
	}

	IEnumerator turning(Vector2 turnVec){
		movementTriggered = true;


		StartCoroutine(blink.blink());
		while (movementLocked) {
			yield return new WaitForEndOfFrame ();
		}

		//straighten the vector out.
		Vector2 fixedVec = new Vector2 (Mathf.Round(turnVec.x), Mathf.Round(turnVec.y));
		//map it to a vector3
		Vector3 vec3 = new Vector3 (fixedVec.y * turnIncrement, fixedVec.x * turnIncrement, 0f);
		//add it to the current rotation
		vec3 += transform.localRotation.eulerAngles;
		//finally, apply it
		transform.localEulerAngles = vec3;
//		Debug.Log (fixedVec);
//		Debug.Log (vec3);

		//place a hold until the user releases the stick. Tapping is required for additional rotation.
		while (rightstick.sqrMagnitude > 0.5f) {
			yield return new WaitForEndOfFrame ();
		}

		yield return new WaitForEndOfFrame ();

		movementTriggered = false;	
	}

}
