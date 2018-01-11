using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadSceneOnClick : MonoBehaviour {

	public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
