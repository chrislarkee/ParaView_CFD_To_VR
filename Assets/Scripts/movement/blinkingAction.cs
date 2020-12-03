using UnityEngine;
using System.Collections;

public class blinkingAction : MonoBehaviour {

	private GameObject lidTop;
	private MeshRenderer rendTop;

	private GameObject lidBottom;
	private MeshRenderer rendBottom;



	public float blinkspeed = 1f;

	// Use this for initialization
	void Start () {
		lidTop = transform.Find ("EyelidTop").gameObject;
		rendTop = lidTop.GetComponent<MeshRenderer> ();

		lidBottom = transform.Find ("EyelidBottom").gameObject;	
		rendBottom = lidBottom.GetComponent<MeshRenderer>();

		rendTop.enabled = false;
		rendBottom.enabled = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			StopCoroutine ("blink");
			StartCoroutine ("blink");
		}
	}

	
	public IEnumerator blink(){
		rendTop.enabled = true;
		rendBottom.enabled = true;

		Vector3 originalSize = Vector3.one * 100f;

		//close
		float movePointer = 0f;
		while (movePointer < 1f) {
			movePointer += Time.deltaTime * blinkspeed;
			lidTop.transform.localScale = Vector3.LerpUnclamped (Vector3.zero, originalSize, movePointer);
			lidBottom.transform.localScale = Vector3.LerpUnclamped (Vector3.zero, originalSize, movePointer);
			yield return new WaitForEndOfFrame ();
		}
			
		BlinkingMovement.movementLocked = !BlinkingMovement.movementLocked;
		yield return new WaitForSeconds(0.2f);

		//open
		movePointer = 0f;
		while (movePointer < 1f) {
			movePointer += Time.deltaTime * blinkspeed;
			lidTop.transform.localScale = Vector3.LerpUnclamped (originalSize, Vector3.zero, movePointer);
			lidBottom.transform.localScale = Vector3.LerpUnclamped (originalSize, Vector3.zero, movePointer);
			yield return new WaitForEndOfFrame ();
		}

		rendTop.enabled = false;
		rendBottom.enabled = false;			
	}
}
