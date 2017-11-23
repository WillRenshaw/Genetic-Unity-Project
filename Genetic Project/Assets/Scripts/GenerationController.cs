using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerationController : MonoBehaviour {
	public Generation currentGen;
	public GameObject ecosystem;

	public float simulationLength = 10f;
	public bool testXAxis = false;
	public bool testYAxis = false;

	void StartSimulation(){
		Debug.Log ("STARTED SIMULATION AT: " + Time.time);
		int i = 0;
		foreach (Creature c in currentGen.population) {
			GameObject eco = Instantiate (ecosystem);
			eco.name = "Ecosystem " + i;
			eco.transform.parent = this.transform;
			eco.transform.position = new Vector3(0,i * 20,0);
			CreatureController cc = eco.GetComponentInChildren<CreatureController> ();
            cc.genes = c;
			cc.running = true;
			cc.testXAxis = testXAxis;
			cc.testYAxis = testYAxis;
			i++;
		}
	}

	void EndSimulation(){
        Debug.Log("ENDED SIMULATION AT: " + Time.time);
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponentInChildren<CreatureController> ().running = false;
			Creature c = transform.GetChild (i).GetComponentInChildren<CreatureController>().genes;
			currentGen.population [i] = c;
		}
	}

	void Start(){
        Generation g = new Generation (1);
        //Creature a = new Creature ("Test", g.genNumber, 1);
        //a.RHF = new SinWave(10, 1f, 4);
        // a.LKF = new TriangleWave(90, 2f, 0);
        for (int i = 0; i < 100; i++)
        {
            Creature a = Helper.CreateRandomCreature(1, 1);
            g.population.Add(a);
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
		currentGen.tested = true;
		Helper.quicksort (currentGen.population, 0, currentGen.population.Count - 1);
		currentGen.sorted = true;
        //Get Best 3
        //Store Current Gen
        //Create New Gen
        //Call Self
	}
}
