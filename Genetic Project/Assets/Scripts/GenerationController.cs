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
    [Range(0f, 1000f)]
    public int genSize = 20;



    Creature best;

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
			eco.transform.position = new Vector3(0,i * 50,0);
			CreatureController cc = eco.GetComponentInChildren<CreatureController> ();
            cc.genes = c;
			cc.running = true;
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
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        currentGen.SetPopulation(p);
	}

	void Start(){ //Currently used to start the simulation
		best = new Creature ("",1,1);
        best.SetFitness(-999999f);


        Generation g = new Generation (1);

        Creature parent = new Creature("", 1, 1); 

        g.AppendCreature(parent); //Create initial generation
		for (int i = 0; i < genSize - 1; i++) {
			Creature c3 = Helper.MateCreatures (parent, parent, g.GENNUMBER, i + 2);
			g.AppendCreature(c3);
		}

        currentGen = g;
        StartCoroutine (RunSimulation());
	}

	IEnumerator RunSimulation(){
		StartSimulation ();
		yield return new WaitForSeconds (simulationLength);
		EndSimulation ();

        currentGen.MarkTested();
        List<Creature> p = currentGen.GetPopulation();
        Helper.quicksort (p, 0, p.Count - 1);
        p.Reverse();
        currentGen.SetPopulation(p);
        currentGen.MarkSorted();
        currentGen.calculateStats();


        
        foreach (Creature c in p) {
            if (c.GetFitness() > best.GetFitness())
            {
                best = c;
                print("Best replaced with " + c.NAME + " with fintess: " + c.GetFitness());
            }

        }


        genUI.text = ("Gen: " + (currentGen.GENNUMBER + 1) +"\nPrevious Mean Fitness: " + currentGen.MEANFITNESS + "\nCurrent Best Fitness: " + best.GetFitness());
        Helper.writeScore(best.GetFitness(), currentGen.MEANFITNESS); //Write Scores to file so they can be graphed
        print("Mean Fitness: " + currentGen.MEANFITNESS);
        currentGen = CreateNewGeneration(genSize, best, currentGen.GENNUMBER + 1);
        StartCoroutine(RunSimulation());
    }

    private Generation CreateNewGeneration(int genSize, Creature parents, int genNum)
    {
        Generation newGen = new Generation(genNum);

        {
            while(newGen.GetPopulation().Count < genSize)
            {
                newGen.AppendCreature(Helper.MateCreatures(best, best, genNum, 1));
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
