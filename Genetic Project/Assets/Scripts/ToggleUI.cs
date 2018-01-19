using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour {

    public GameObject UI;

    public void Toggle()
    {
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
