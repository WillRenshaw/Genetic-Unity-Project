using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadSceneOnClick : MonoBehaviour {

	//Load scene ith specified index
	public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
