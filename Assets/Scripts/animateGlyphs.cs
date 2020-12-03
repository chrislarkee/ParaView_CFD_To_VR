using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;			//linq gets used to sort the arrow's array.
using VRStandardAssets.Utils;

public class animateGlyphs : MonoBehaviour {
	public static int ArrowCurrentFrame = 0;
	public bool AnimationActive = false;

	private AudioSource audioEmitter;
	private GameObject[] cycleArrows;
	private GameObject[] cycleMeshes;

	public static int ArrowTimeSteps;
	private bool inputBlocked = false;

	//Edits
	public float progressPercent;

	// Use this for initialization
	void Start () {		
		audioEmitter = GameObject.Find ("Audio Source").GetComponent<AudioSource>();

		//set up the Arrows array, sort it, count it.
		cycleArrows = GameObject.FindGameObjectsWithTag("Arrows").OrderBy( go => go.name ).ToArray();
		cycleMeshes = GameObject.FindGameObjectsWithTag("Mesh").OrderBy( go => go.name ).ToArray();
		ArrowTimeSteps = cycleArrows.Length - 1;

		ArrowCurrentFrame = ArrowTimeSteps;
		turnAllOff();
		incrementArrows(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetActiveController() != OVRInput.Controller.RTouch) {
			if (OVRInput.GetDown(OVRInput.RawButton.X) || Input.GetKeyDown(KeyCode.Space)) {
				AnimationActive = !AnimationActive;
				if (AnimationActive == true) StartCoroutine(playArrowAnimation());
			}
			
			if (OVRInput.Get(OVRInput.Button.DpadLeft) || Input.GetKey(KeyCode.LeftArrow))
				incrementArrows(false);
			else if (OVRInput.Get(OVRInput.Button.DpadRight) || Input.GetKey(KeyCode.RightArrow))
				incrementArrows(true);
		}
	}

	void turnAllOff(){
		for (int i=0; i < cycleArrows.Length; i++){
			cycleArrows[i].SetActive(false);
			cycleMeshes[i].SetActive(false);
		}
	}

	//direction true = forward. direction false = backward
	public void incrementArrows(bool direction) {
		if (inputBlocked)
			return;
		else
			inputBlocked = true;
		
		//turn off the active arrow
		cycleArrows[ArrowCurrentFrame].SetActive(false);
		cycleMeshes[ArrowCurrentFrame].SetActive(false);

		//increment the value of the active arrow		
		if (direction) {
			++ArrowCurrentFrame;
			if (ArrowCurrentFrame > ArrowTimeSteps)
				ArrowCurrentFrame = 0;
		} else {
			--ArrowCurrentFrame;
			if (ArrowCurrentFrame < 0)
				ArrowCurrentFrame = ArrowTimeSteps;
		}
		
		//...then the right one on
		cycleArrows[ArrowCurrentFrame].SetActive(true);
		cycleMeshes[ArrowCurrentFrame].SetActive(true);
		
		//allow another change after a delay
		if (!AnimationActive)
			progressPercent = (float)ArrowCurrentFrame / (float)ArrowTimeSteps;

		Invoke("releaseInput", 0.25f);
	}
	
	void releaseInput(){
		inputBlocked = false;
	}

	public IEnumerator playArrowAnimation() {
		//reset
		turnAllOff();
		
		//always start on 0 to simplify audio sync
		ArrowCurrentFrame = 0;
		audioEmitter.Play ();
		
		float startTime = Time.time;
		yield return new WaitForEndOfFrame();
		
		int stepLength = 60;
		progressPercent = 0.0f;

		//the loop		
		while (AnimationActive) {
			progressPercent = ((Time.time - startTime) * 1000f) / (ArrowTimeSteps * stepLength);
			
			if (progressPercent < 1.0f) {
				ArrowCurrentFrame = Mathf.FloorToInt(progressPercent * ArrowTimeSteps);
			}
			else {
				startTime = Time.time;
				ArrowCurrentFrame = 0;
				audioEmitter.time = 0f;
				audioEmitter.Play ();
			}
			
			//Debug.Log("Current = " + ArrowCurrentFrame.ToString() + ", Previous = " + ArrowPreviousFrame.ToString());
			yield return new WaitForEndOfFrame();
			turnAllOff();
			cycleArrows[ArrowCurrentFrame].SetActive(true);
			cycleMeshes[ArrowCurrentFrame].SetActive(true);
		}
		
		audioEmitter.Stop ();
	}
}
