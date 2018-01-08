using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerationController : MonoBehaviour {
	private Generation currentGen;
	public GameObject ecosystem;
    public Text genUI;

    
    [Range(0f, 100f)]
	public float simulationLength = 10f;

    [Range(1, 10000)]
    public int generations = 100;

    [Range(1, 1000)]
    public int genSize = 50;

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

        Creature c1 = new Creature("", 1, 1);
        Creature c2 = Helper.CreateRandomCreature(1, 2);

        g.AppendCreature(c1);
		g.AppendCreature(c2);

		for (int i = 0; i < genSize - 2; i++) {
			Creature c3 = Helper.MateCreatures (c1, c2, g.GENNUMBER, i + 3);
			g.AppendCreature(c3);
		}
        

        Helper.WriteGeneration(g);
        Helper.ReadGenerations();
        currentGen = Helper.savedGenerations[0];
        StartCoroutine (RunSimulation());
	}

    IEnumerator RunSimulation()
    {
        for (int i = 0; i < generations; i++)
        {
            StartSimulation();
            yield return new WaitForSeconds(simulationLength);
            EndSimulation();
            currentGen.MarkTested();
            List<Creature> p = currentGen.GetPopulation();
            Helper.quicksort(p, 0, p.Count - 1);
            p.Reverse();
            currentGen.SetPopulation(p);
            currentGen.MarkSorted();
            currentGen.calculateStats();

            List<Creature> parents = new List<Creature>();

            parents.Add(currentGen.GetPopulation()[0]);
            parents.Add(currentGen.GetPopulation()[1]);
            parents.Add(currentGen.GetPopulation()[2]);

            genUI.text = ("Gen: " + (currentGen.GENNUMBER + 1) + "\nPrevious Mean Fitness: " + currentGen.MEANFITNESS + "\nPrevious Best: " + currentGen.MAXFITNESS + "\nPrevious Worst: " + currentGen.MINFITNESS);
            Helper.WriteScores(currentGen.MEANFITNESS, currentGen.MAXFITNESS, currentGen.MINFITNESS);
            currentGen = CreateNewGeneration(genSize, parents, currentGen.GENNUMBER + 1);
        }
    }


    private Generation CreateNewGeneration(int genSize, List<Creature> parents, int genNum)
    {

        Generation newGen = new Generation(genNum);

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


        return newGen;
    }
}
