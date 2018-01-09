using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public int targetFrameRate = 30;
	
	// Update is called once per frame
	void Update () {
        float currentFrameRate = 1f / Time.deltaTime;
        if(currentFrameRate < targetFrameRate)
        {
            Time.timeScale -= 0.1f;
        }
        else if(currentFrameRate > targetFrameRate)
        {
            Time.timeScale += 0.1f;
        }

        Time.fixedDeltaTime = 0.02f / Time.timeScale;

    }
}
