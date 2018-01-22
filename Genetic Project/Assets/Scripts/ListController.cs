using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour {

    private RectTransform rect; //Transform of this gamobject
    public Transform button; //Button prefab
    public Text stats; //Reference to stats panel
    private Generation[] contents; //The contents of this list


    void Start () {
        rect = GetComponent<RectTransform>(); //Get reference to transform
        SetGeneration(Helper.savedGenerations.ToArray());
	}
	
	public void SetGeneration(Generation[] input)
    {
        contents = input;
        foreach (Generation entry in contents)
        { //Loops through all generations
            Transform but = Instantiate(button, transform); //Create button
            but.GetComponentInChildren<Text>().text = entry.GENNUMBER.ToString(); //Set text

			//Add a listner so that when button is clicked it calls SetStats
            but.GetComponent<Button>().onClick.AddListener(delegate { SetStats(entry.GENNUMBER); });
        }
        rect.sizeDelta = new Vector2(500, 100 * transform.childCount);
    }

    public void SetStats(int gen)
    {//Set stats panel bwhen button is clicked
        stats.text = "Gen: " + contents[gen - 1].GENNUMBER + "\nMean: " + (int)contents[gen - 1].MEANFITNESS + "\nStandard Deviation: " + (int)contents[gen - 1].SDFITNESS + "\nMin Fitness: " + (int)contents[gen - 1].MINFITNESS + "\nMax Fitness: " + (int)contents[gen - 1].MAXFITNESS;
    }

}
