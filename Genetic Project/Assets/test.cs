using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	DistanceJoint2D joint;
	// Use this for initialization
	void Start () {
		joint = GetComponent<DistanceJoint2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		joint.distance = 4* Mathf.Sin (Time.time) + 4;
	}
}
