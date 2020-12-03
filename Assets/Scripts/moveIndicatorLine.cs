using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveIndicatorLine : MonoBehaviour {

	//these values will be edited by the user manually
	public float minX;
	public float maxX;

    //same value as the animateGlyph float progressPercent
    private animateGlyphs glyphs;
    private float lerpedPosition;

	private Vector3 startPosition;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
        glyphs = GameObject.Find("ParaView_CFD").GetComponent<animateGlyphs>();
        if (glyphs == null) gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		//syncs the lerpedPosition with the progressPercent in animateGlyphs		
		lerpedPosition = glyphs.progressPercent;

		//this is just for demonstration.
		if (lerpedPosition <= 1.00f)
			lerpedPosition += Time.deltaTime * 0.5f;
		else
			lerpedPosition = 0f;

		updatePosition();		
	}

	private void updatePosition(){
		startPosition.x = Mathf.Lerp(minX, maxX, lerpedPosition);
		transform.localPosition = startPosition;
	}
}
