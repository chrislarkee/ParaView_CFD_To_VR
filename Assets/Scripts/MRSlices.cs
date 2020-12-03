using UnityEngine;
using System.Collections;
using System.Linq;			//linq gets used to sort the arrow's array. line 25.

public class MRSlices : MonoBehaviour {

	private int MRPreviousFrame;
	public static int MRCurrentFrame;

	private GameObject[] slices;
	private GameObject ActiveSlice;
	private int MRSteps;

	private float axisValue;
	private bool inputBlocked = false;

	// Use this for initialization
	void Start () {
		MRCurrentFrame = 0;

		//set up the array, sort it, count it.
		slices = GameObject.FindGameObjectsWithTag("Imaging").OrderBy( go => go.name ).ToArray();
		MRSteps = slices.Length - 1;
		MRPreviousFrame = MRSteps;

		//turn off all the arrows, then turn the first one back on.
		foreach (GameObject slice in slices) {
			slice.gameObject.SetActive(false);
		}
		slices[0].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		//increment frames with the keyboard
		if (OVRInput.Get (OVRInput.Button.DpadUp) && !inputBlocked) { 
			incrementSlice (true);
		} else if (OVRInput.Get (OVRInput.Button.DpadDown) && !inputBlocked) {
			incrementSlice (false);	
		}

		//move Image Slices up or down using up and down arrows on the keyboard
		if (Input.GetKeyUp (KeyCode.UpArrow) && !inputBlocked) { 
			incrementSlice (true);
		} else if (Input.GetKeyUp (KeyCode.DownArrow) && !inputBlocked) {
			incrementSlice (false);	
		}

	}

	public void incrementSlice(bool direction) {
		//direction true = forward. direction false = backward
		inputBlocked = true;
		MRPreviousFrame = MRCurrentFrame;

		//figure out the values for incremental movement...
		if (direction) {
			++MRCurrentFrame;
			if (MRCurrentFrame > MRSteps)
				MRCurrentFrame = MRSteps;
		} else {
			--MRCurrentFrame;
			if (MRCurrentFrame < 0)
				MRCurrentFrame = 0;
		}

		//...then enable visibility of the right things
		ActiveSlice = slices[MRPreviousFrame];
		ActiveSlice.gameObject.SetActive(false);

		ActiveSlice = slices[MRCurrentFrame];
		ActiveSlice.gameObject.SetActive(true);

		//allow another change after about 2 frames, regardless of framerate.
		Invoke("releaseInput", 0.066f);
	}

	void releaseInput(){
		inputBlocked = false;
	}
}
