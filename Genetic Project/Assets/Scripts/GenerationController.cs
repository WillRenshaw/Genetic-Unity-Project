using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerationController : MonoBehaviour {
	public Generation currentGen;
	public GameObject ecosystem;
    public Text genUI;

    [Range(0f, 100f)]
	public float simulationLength = 10f;
	public bool XAxis = false;
	public bool YAxis = false;

	void StartSimulation(){
        
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Debug.Log ("STARTED SIMULATION AT: " + Time.time);
		int i = 0;
		foreach (Creature c in currentGen.GetPopulation()) {
			GameObject eco = Instantiate (ecosystem);
			eco.name = "Ecosystem " + i;
			eco.transform.parent = this.transform;
			eco.transform.position = new Vector3(0,i * (float)Helper.userPrefs.ecosystemSpacing,0);
			CreatureController cc = eco.GetComponentInChildren<CreatureController> ();
            cc.genes = c;
			cc.running = true;
			cc.testXAxis = XAxis;
			cc.testYAxis = YAxis;
			i++;
		}
	}

	void EndSimulation(){
        List<Creature> p = currentGen.GetPopulation();
        Debug.Log("ENDED SIMULATION AT: " + Time.time);
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponentInChildren<CreatureController> ().running = false;
			Creature c = transform.GetChild (i).GetComponentInChildren<CreatureController>().genes;
			p [i] = c;
		}
        currentGen.SetPopulation(p);
	}

	void Start(){
        Helper.userPrefs = new UserPrefs() {
            initialBodyCV = 1,
            furtherBodyCV = 1,
            initialFunctionCV = 1,
            furtherFunctionCV = 1,
            ecosystemSpacing = 50,
            varianceMultiplier = 1
		};
		Helper.WriteToFile ("/userPrefs.gd", Helper.userPrefs);
		Helper.userPrefs = (UserPrefs)Helper.ReadData ("/userPrefs.gd");

        Generation g = new Generation (1);

        Creature c1 = Helper.CreateRandomCreature(1,1);
        Creature c2 = new Creature("", 1, 2);

        g.AppendCreature(c1);
		g.AppendCreature(c2);
		for (int i = 0; i < 48; i++) {
			Creature c3 = Helper.MateCreatures (c1, c2, g.GENNUMBER, i + 3);
			g.AppendCreature(c3);
		}
        

        Helper.WriteGeneration(g);
        Helper.ReadGenerations();
        currentGen = Helper.savedGenerations[0];
        StartCoroutine (RunSimulation());
	}

	IEnumerator RunSimulation(){
		StartSimulation ();
		yield return new WaitForSeconds (simulationLength);
		EndSimulation ();
        currentGen.MarkTested();
        List<Creature> p = currentGen.GetPopulation();
        Helper.quicksort (p, 0, p.Count - 1);
        currentGen.SetPopulation(p);
        currentGen.MarkSorted();
        currentGen.calculateStats();

        List<Creature> parents = new List<Creature>();
        p.Reverse();
        parents.Add(currentGen.GetPopulation()[0]);
        print(currentGen.GetPopulation()[0].GetFitness());
        parents.Add(currentGen.GetPopulation()[1]);
        print(currentGen.GetPopulation()[1].GetFitness());
        print(currentGen.GetPopulation()[2].GetFitness());

        genUI.text = ("Gen: " + (currentGen.GENNUMBER + 1) +"\nPrevious Mean Fitness: " + (int)currentGen.MEANFITNESS);
        currentGen = CreateNewGeneration(50, parents, currentGen.GENNUMBER + 1);
        StartCoroutine(RunSimulation());
    }

    private Generation CreateNewGeneration(int genSize, List<Creature> parents, int genNum)
    {

        Generation newGen = new Generation(genNum);
        if (parents.Count > Mathf.Sqrt(genSize))
        {
            Debug.LogError("Number of Parents must be less than the square root of genSize");
        }
        else
        {
            while(newGen.GetPopulation().Count < genSize)
            {
                int i = 0;
                foreach (Creature p1 in parents)
                {
                    int j = 0;
                    foreach (Creature p2 in parents)
                    {
                        newGen.AppendCreature(Helper.MateCreatures(p1, p2, genNum, i * j));
                    }
                    j++;
                }
                i++;
            }

            while(newGen.GetPopulation().Count > genSize)
            {
                List<Creature> tempList = newGen.GetPopulation();
                tempList.RemoveAt(newGen.GetPopulation().Count - 1);
                newGen.SetPopulation(tempList);
            }
        }

        return newGen;
    }
}
