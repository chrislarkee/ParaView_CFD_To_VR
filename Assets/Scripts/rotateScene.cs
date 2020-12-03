//using UnityEngine;
//using System.Collections;
//using MiddleVR_Unity3D;
//
//public class rotateScene : MonoBehaviour {
//
//	private float axisValueH = 0f;				//the wand's joystick axis, horizontal
//	private float axisValueV = 0f;				//the wand's joystick axis, vertical
//	private float rotationSpeed;
//
//	private GameObject origin;
//	private Quaternion initialRotation;
//
//	// Use this for initialization
//	void Start () {
//		initialRotation = transform.rotation;	
//		origin = GameObject.Find ("HeadNode");
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		axisValueV = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
//		if (Mathf.Abs(axisValueV) > 0.05f) rotateMolecule("v", axisValueV);
//		if (MiddleVR.VRDeviceMgr.IsWandButtonToggled (5))
//			resetRotation ();
//
//
//		if (MiddleVR.VRDeviceMgr.IsKeyToggled (MiddleVR.VRK_4)) resetRotation ();
//
//		rotationSpeed = 40 * (float)MiddleVR.VRKernel.GetDeltaTime();
//		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_UP)) rotateMolecule ("v", rotationSpeed);
//		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_DOWN)) rotateMolecule ("v", rotationSpeed * -1f);
//	}
//
//	void resetRotation() {
//		transform.rotation = initialRotation;
//	}
//
////	void checkRotation(){
//////		axisValueH = MiddleVR.VRDeviceMgr.GetWandHorizontalAxisValue();
//////		if (Mathf.Abs(axisValueH) > 0.05f) rotateMolecule("h", axisValueH * -1f);
////
////		axisValueV = MiddleVR.VRDeviceMgr.GetWandVerticalAxisValue();
////		if (Mathf.Abs(axisValueV) > 0.05f) rotateMolecule("v", axisValueV);
////
////		//keyboard rotation
////		rotationSpeed = 40 * (float)MiddleVR.VRKernel.GetDeltaTime();
//////		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_LEFT)) rotateMolecule ("h", rotationSpeed);
//////		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_RIGHT)) rotateMolecule ("h", rotationSpeed * -1f);
////		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_UP)) rotateMolecule ("v", rotationSpeed);
////		if (MiddleVR.VRDeviceMgr.IsKeyPressed (MiddleVR.VRK_DOWN)) rotateMolecule ("v", rotationSpeed * -1f);
////	}
//
//
//	void rotateMolecule(string dir, float amount){
//		if (dir == "h") {
//			transform.RotateAround(origin.transform.position, Vector3.up, amount);
//		}
//		if (dir == "v") {
//			transform.RotateAround(origin.transform.position, Vector3.right, amount * 3f);
//		}
//	}
//
//}
