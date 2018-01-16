using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsController : MonoBehaviour {

    public Slider variance, bodyCV, functionCV, spacing;

    void Start()
    {
        if(File.Exists(Application.persistentDataPath + "userprefs.gd"))
        {
            Debug.Log("Read in Existing User Prefs");
            Helper.userPrefs = (UserPrefs)Helper.ReadData("userprefs.gd");
        }
        else
        {
            Debug.Log("Deafulted User Prefs");
            Helper.userPrefs = new UserPrefs()
            {
                initialBodyCV = 0.5,
                initialFunctionCV = 0.5,
                ecosystemSpacing = 50,
                varianceMultiplier = 1
            };

            Helper.WriteToFile("userprefs.gd", Helper.userPrefs);
        }
        bodyCV.value = (float)Helper.userPrefs.initialBodyCV;
        functionCV.value = (float)Helper.userPrefs.initialFunctionCV;
        spacing.value = (float)Helper.userPrefs.ecosystemSpacing;
        variance.value = (float)Helper.userPrefs.varianceMultiplier;
    }

	public void updateUserPrefs()
    {
        Helper.userPrefs = new UserPrefs()
        {
            initialBodyCV = bodyCV.value,
            initialFunctionCV = functionCV.value,
            ecosystemSpacing = spacing.value,
            varianceMultiplier = variance.value
        };

        Helper.WriteToFile("userprefs.gd", Helper.userPrefs);
    }
}
