using UnityEngine;
using System.Collections;

public class BlinkPointer : MonoBehaviour {

	private MeshRenderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer>();
		StartCoroutine("blinkMarker");	
	}

	IEnumerator blinkMarker() {
		while (true) {
			rend.enabled = true;
			yield return new WaitForSeconds (1.00f);
			rend.enabled = false;
			yield return new WaitForSeconds (0.2f);
		}
	}
}
