using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GearVRControls : MonoBehaviour {


    //used to dispaly differnt color when mouse/wand is on button
    public ColorBlock highlighted;
    public ColorBlock norm;
    private VRInteractiveItem m_InteractiveItem;

	//Find other scripts
    public static MasterControl MasterControl2;
	public static animateGlyphs AnimateArrows2;
    public static MRSlices MRSlices2;

	//text that displays when the user is hovering over the wall indice scale
    public GameObject scaleText;

	//wtiches bewteen Histology and MRI button- not needed if you do not have histology
    public Text textHistology = null;
    private int counterHistology = 0;

	//switches between histology and MRI button
	public Text textMRI = null;
    private int counterMRI = 0;


    // Use this for initialization
    void Start () {
        //Find the master script
		MasterControl2 = GameObject.Find("ParaView_CFD").GetComponent<MasterControl>();

        //Find AnimateArows script
		AnimateArrows2 = GameObject.Find("ParaView_CFD").GetComponent<animateGlyphs>();

        //Find MR slices script
		if (GameObject.Find ("MRI") == null) {
			Debug.Log ("GearVR Script: MRI does not exist");
		} else { 
			MRSlices2 = GameObject.Find ("MRI").GetComponent<MRSlices> ();
		}


        //NEW VR Controlls
        m_InteractiveItem = GetComponent<VRInteractiveItem>();

        m_InteractiveItem.OnOver += VRPoint;
        m_InteractiveItem.OnOut += VRPointOff;
        m_InteractiveItem.OnClick += VRClick;

		//Sets the Wall indice "Click to chance wall indices" text off at start of play
		scaleText.SetActive(false);
    }
		

    //When an intereactive item is clicked on the unity game does something
    public void VRClick()
    {
        Debug.Log("trying to click");
        if (gameObject.tag == "Histology_Button")
        {
            Debug.Log("Trying to switch to Histology");
            changeHistology();

            counterHistology++;
            if (counterHistology % 2 == 1)
            {
                textHistology.text = "Map";
            }
            else
            {
                textHistology.text = "Histology";
            }
        }

        if (gameObject.tag == "Scale")
        {
            Debug.Log("Trying to switch to Differnt Scale");
            changeScale();
        }

        if (gameObject.tag == "MRI_Button")
        {
            Debug.Log("Trying to switch to MRI");
            changeMRI();
            MRIncrease(); 

            counterMRI++;
            if (counterMRI % 2 == 1)
            {
                textMRI.text = "Map";
            }
            else
            {
                textMRI.text = "Imaging";
            }
        }

		if (gameObject.tag == "Animate")
        {
			Debug.Log("Trying to start blood Animate");
			Animate();
        }

        if (gameObject.tag == "Forward")
        {
			AnimateForward();
        }
        if (gameObject.tag == "Backward")
        {
			AnimateBackward();
        }

		if (gameObject.tag == "MRI_Increase")
		{
		     Debug.Log("MRI Increase");
		     MRIncrease();
		}
		if (gameObject.tag == "MRI_Decrease")
		{
		     Debug.Log("MRI Decrease");
		     MRDecrease();
		}
    }

	//switches between map and histology - not needed if you do not have histology information
    public void changeHistology()
    {
        MasterControl2.toggleObject(MasterControl2.histology);
        MasterControl2.toggleObject(MasterControl2.map);
    }

	//Changes the scale wall indice
    public void changeScale()
    {
        MasterControl2.cycleWallTexture();
    }

	//When MRI is shown, the glyphs/streamlines are turned off
    public void changeMRI()
    {
        MasterControl2.toggleObject(MasterControl2.masterArrow);
        MasterControl2.toggleObject(MasterControl2.masterMR);
    }

	//When in MRI mode - it advances one image slice
    public void MRIncrease()
    {
        MRSlices2.incrementSlice(true);
    }

	//when in MRI mode- it goes back one image slice
    public void MRDecrease()
    {
        MRSlices2.incrementSlice(false);
    }

	//When the animate button is pressed, the animation (pulsating streamlines or glyphs) - if already is animating, it stops the animation
	public void Animate()
    {
		Debug.Log ("Animate");
		AnimateArrows2.AnimationActive = !AnimateArrows2.AnimationActive;
		if (AnimateArrows2.AnimationActive == true) StartCoroutine(AnimateArrows2.playArrowAnimation());
    }

	//move the animation (pulsating streamlines or glyphs) forward one time step
	public void AnimateForward()
    {
        AnimateArrows2.incrementArrows(true);
    }

	//move the animation (pulsating streamlines or glyphs) back one time step
	public void AnimateBackward()
    {
        AnimateArrows2.incrementArrows(false);
    }

	//when on an interactive item and the mouse it hovering over the object, an indication appears
    private void VRPoint()
    {
		//used to identify how many child objects are in the Other Mesh Game object- if there are none, the "Click to change wall indice text" never appears
		GameObject masterOtherMesh = GameObject.Find ("Other Mesh");
		Transform otherMeshTransform;
		otherMeshTransform = masterOtherMesh.transform;
		int otherMeshChildren = otherMeshTransform.childCount;


        if (gameObject.tag == "Histology_Button")
        {
            Debug.Log("Hovering on Histology Button");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "MRI_Button")
        {
            Debug.Log("Hovering on MRI Button");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "MRI_Increase")
        {
            Debug.Log("Hovering on MRI Increase");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "MRI_Decrease")
        {
            Debug.Log("Hovering on MRI Decrease");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "Forward")
        {
			Debug.Log("Hovering on Animate Forward");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "Backward")
        {
			Debug.Log("Hovering on Animate Backward");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
		if (gameObject.tag == "Animate")
        {
			Debug.Log("Hovering on Animate");
            Button buttons = GetComponent<Button>();
            buttons.colors = highlighted;
        }
        if (gameObject.tag == "Scale")
        {
			Debug.Log("Hovering on Scale");

			//if there are no child objects in Other mesh the change wall indice text never appears
			if (otherMeshChildren == 0) {
				scaleText.SetActive (false);
			} else {
				scaleText.SetActive (true);
			}
        }
    }

	//when on an interactive item and the mouse was hovering over the object and then points off, the indication dissappears
    private void VRPointOff()
    {
        if (gameObject.tag == "Histology_Button")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "MRI_Button")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "MRI_Increase")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "MRI_Decrease")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "Forward")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "Backward")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
		if (gameObject.tag == "Animate")
        {
            Button buttons = GetComponent<Button>();
            buttons.colors = norm;
        }
        if (gameObject.tag == "Scale")
        {
            scaleText.SetActive(false);
        }

    } 
}
