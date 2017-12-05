using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float moveSpeed;
	private float originalSize;
	Camera c;

	// Use this for initialization
	void Start () {
		c = GetComponent<Camera> ();
		originalSize = c.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		moveSpeed =  c.orthographicSize / originalSize;

		transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed, 0));
		if(Input.GetAxisRaw("Mouse ScrollWheel") !=0){
			c.orthographicSize -= 20 * Input.GetAxisRaw("Mouse ScrollWheel");
		}

	}
}
