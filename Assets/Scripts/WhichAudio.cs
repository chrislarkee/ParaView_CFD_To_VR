using UnityEngine;
using System.Collections;

public class WhichAudio : MonoBehaviour {

private GameObject audioEmitter;
private float audioTimer;

public AudioClip entranceClip;
public AudioClip exitClip;


	void Start() {
		audioEmitter = GameObject.Find ("Audio Source");
	}

	void OnTriggerEnter () {
	//set the audio sample to self
		audioTimer = audioEmitter.GetComponent<AudioSource>().time;
		audioEmitter.GetComponent<AudioSource>().clip = entranceClip;
		audioEmitter.GetComponent<AudioSource>().time = audioTimer;
		//if (AnimateArrows.ArrowAnimationActive) audioEmitter.GetComponent<AudioSource>().Play (); --- Had to comment out this line to get the animation to work
	}

	void OnTriggerExit () {
	//set the audio sample to exitMesh;
		if (exitClip) {
			audioTimer = audioEmitter.GetComponent<AudioSource>().time;
			audioEmitter.GetComponent<AudioSource>().clip = exitClip;
			audioEmitter.GetComponent<AudioSource>().time = audioTimer;
			//if (AnimateArrows.ArrowAnimationActive) audioEmitter.GetComponent<AudioSource>().Play (); --- had to comment out this to get the animation to work 
		}
	}

}
