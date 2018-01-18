using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


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
        Generation g = new Generation (1);
        if (File.Exists(Application.persistentDataPath + "/userprefs.gd"))
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
                ecosystemSpacing = 50,
                varianceMultiplier = 1
            };

            Helper.WriteToFile("/userprefs.gd", Helper.userPrefs);
        }
        List<Creature> parents = new List<Creature>();
        parents.Add(new Creature("", 1, 1));
        parents.Add(Helper.CreateRandomCreature(1, 2));
        g = CreateNewGeneration(genSize, parents, 1);
        currentGen = g;
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
            if (Random.Range(1, 11) == 10) {
                Debug.Log("Random Seeder Parent Added");
                parents.Add(Helper.CreateRandomCreature(currentGen.GENNUMBER, 1));
            }
            genUI.text = ("Gen: " + (currentGen.GENNUMBER + 1) + "\nPrevious Mean Fitness: " + currentGen.MEANFITNESS + "\nPrevious Best: " + currentGen.MAXFITNESS + "\nPrevious Worst: " + currentGen.MINFITNESS);
            
            Helper.WriteGeneration(currentGen);
            currentGen = CreateNewGeneration(genSize, parents, currentGen.GENNUMBER + 1);
        }
    }


    private Generation CreateNewGeneration(int genSize, List<Creature> parents, int genNum)
    {

        Generation newGen = new Generation(genNum);
        newGen.AppendCreature(parents[0]);
        while (newGen.GetPopulation().Count < genSize)
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

        while (newGen.GetPopulation().Count > genSize)
        {
            List<Creature> tempList = newGen.GetPopulation();
            tempList.RemoveAt(newGen.GetPopulation().Count - 1);
            newGen.SetPopulation(tempList);
        }


        return newGen;
    }

}
