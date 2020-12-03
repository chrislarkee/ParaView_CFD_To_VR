using UnityEngine;
using System.Collections;

public class MouseLook2 : MonoBehaviour {
	public enum RotationAxes
	{
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}
	public RotationAxes axes = RotationAxes.MouseXAndY;
	
	public float sensitivityHor = 9.0f;
	public float sensitivityVert = 9.0f;
	
	public float minimumVert = -45.0f;
	public float maximumVert = 45.0f;
	
	private float _rotationX = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);        
		}
		else if (axes == RotationAxes.MouseY)
		{
			float rotationX = Input.GetAxis("Mouse Y") * sensitivityVert;
			rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
			
			float rotationY = transform.localEulerAngles.y;
			
			transform.localEulerAngles = new Vector3(rotationX + transform.localEulerAngles.x, rotationY, 0);
		}
		else
		{
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
			
			float delta = Input.GetAxis("Mouse X") * sensitivityHor;
			float rotationY = transform.localEulerAngles.y + delta;
			
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
		}
	}
}