using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float moveSpeed; //The speed the camera should moce at
	private float originalSize; //The size the camera is at the start
	Camera c; //Reference to the camera

	void Start () {
		c = GetComponent<Camera> (); //Gets the reference to the camera
		originalSize = c.orthographicSize; //Gets the size of the camera at startup
	}

	void Update () {
		moveSpeed =  c.orthographicSize / originalSize; //Movespeed is proportional to the size compared to the original

		transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, 0)); //Move the camera based on user inputs
		if(Input.GetAxisRaw("Mouse ScrollWheel") !=0){ //Checks is mouse scroll wheel is being used
			c.orthographicSize -= 20 * Input.GetAxisRaw("Mouse ScrollWheel"); //Adjusts size of camera based on mouse wheel
		}
	}
}
