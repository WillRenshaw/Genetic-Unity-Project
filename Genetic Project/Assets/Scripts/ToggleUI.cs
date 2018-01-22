using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour {

    public GameObject UI; //The UI to toggle

    public void Toggle()
    { //toggles the given UI fbetween active and inactive
        if (UI.activeSelf)
        {
            UI.SetActive(false);
            print("set " + UI.name + " inactive");
        }
        else
        {
            UI.SetActive(true);
            print("set " + UI.name + " active");
        }
    }
}
