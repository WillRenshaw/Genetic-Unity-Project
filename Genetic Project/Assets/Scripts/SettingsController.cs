using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsController : MonoBehaviour {

    public Slider variance, bodyCV, functionCV, iteration, size, length;
	public Text varianceVal, bodyVal, functionVal, iterationVal, sizeVal, lengthVal;

    void OnEnable()
    {
        if(File.Exists(Application.persistentDataPath + "/userprefs.gd"))
        {
			Debug.Log("Read in Existing User Prefs");
            Helper.userPrefs = (UserPrefs)Helper.ReadData("/userprefs.gd");
        }
        else
        {
            Debug.Log("Deafulted User Prefs");
            Helper.userPrefs = new UserPrefs()
            {
                initialBodyCV = 0.5,
                initialFunctionCV = 0.5,
                varianceMultiplier = 1,
				simulationLength = 10,
				generationSize = 50,
				iterationCount = 100
            };

            Helper.WriteToFile("/userprefs.gd", Helper.userPrefs);
        }
        bodyCV.value = (float)Helper.userPrefs.initialBodyCV;
        functionCV.value = (float)Helper.userPrefs.initialFunctionCV;
        variance.value = (float)Helper.userPrefs.varianceMultiplier;
		iteration.value = Helper.userPrefs.iterationCount;
		size.value = Helper.userPrefs.generationSize;
		length.value = (float)Helper.userPrefs.simulationLength;
    }

	public void updateUserPrefs()
    {
        Helper.userPrefs = new UserPrefs()
        {
            initialBodyCV = bodyCV.value,
            initialFunctionCV = functionCV.value,
			varianceMultiplier = variance.value,
			iterationCount = (int)iteration.value,
			generationSize = (int)size.value,
			simulationLength = (double)length.value

        };

        Helper.WriteToFile("/userprefs.gd", Helper.userPrefs);
    }

	public void Update(){
		varianceVal.text = variance.value.ToString();
		bodyVal.text = bodyCV.value.ToString();
		functionVal.text = functionCV.value.ToString();
		iterationVal.text = iteration.value.ToString();
		sizeVal.text = size.value.ToString();
		lengthVal.text = length.value.ToString();
	}
}
