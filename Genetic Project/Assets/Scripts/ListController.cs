using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListController : MonoBehaviour {

    private RectTransform rect;
    public Transform button;
    public Text Stats;
    Generation[] contents;
    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        SetGeneration(Helper.savedGenerations.ToArray());
	}
	
	public void SetGeneration(Generation[] input)
    {
        contents = input;
        foreach (Generation entry in contents)
        {
            Transform b = Instantiate(button, transform);
            b.GetComponentInChildren<Text>().text = entry.GENNUMBER.ToString();

            b.GetComponent<Button>().onClick.AddListener(delegate { SetStats(entry.GENNUMBER); });
        }
        rect.sizeDelta = new Vector2(500, 100 * transform.childCount);
    }

    public void SetStats(int gen)
    {
        Stats.text = "Gen: " + contents[gen - 1].GENNUMBER + "\nMean: " + (int)contents[gen - 1].MEANFITNESS + "\nStandard Deviation: " + (int)contents[gen - 1].SDFITNESS + "\nMin Fitness: " + (int)contents[gen - 1].MINFITNESS + "\nMax Fitness: " + (int)contents[gen - 1].MAXFITNESS;
    }

}
