using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public int targetFrameRate = 30;
    public int tolerance = 5;

    private float timeScale;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    
    void Update () {
        float framerate = 2f / Time.deltaTime;
        //print("FRAMERATAE IS " + framerate);
        if (framerate < targetFrameRate - tolerance)
        {
            Time.timeScale = Mathf.Clamp(Time.timeScale - 0.1f, 0.1f, 100);
            Time.fixedDeltaTime = 0.02f / Time.timeScale;
        }
        else if(framerate > targetFrameRate + tolerance)
        {
           // print("TIMESCALE INCREASED");
            Time.timeScale = Mathf.Clamp(Time.timeScale + 0.5f, 0.1f, 100);
            Time.fixedDeltaTime = 0.02f / Time.timeScale;
        }
	}
}
