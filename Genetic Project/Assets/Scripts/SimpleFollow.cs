using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour {
	public Transform target;

	void Update () {
		transform.position = target.transform.position;
	}
}
