using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {
	//The target frame rate
	private int targetFrameRate = 22;
	

	void Update () {
		//find current frame rate
        float currentFrameRate = 1f / Time.deltaTime;
		//If current frame rate is too low, decrease time scale
        if(currentFrameRate < targetFrameRate)
        {
            Time.timeScale -= 0.1f;
        }
		//If current fram rate is too high, increase time scale
        else if(currentFrameRate > targetFrameRate)
        {
            Time.timeScale += 0.1f;
        }
		//Adjust physics updates based on timeScale
        Time.fixedDeltaTime = 0.02f / Time.timeScale;

    }
}
