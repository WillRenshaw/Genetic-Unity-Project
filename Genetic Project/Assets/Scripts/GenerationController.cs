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
		print ("START SIMULATION");
		int i = 0;
		foreach (Creature c in currentGen.population) {
			GameObject a = Instantiate (ecosystem);
			a.name = "Ecosystem " + i;
			a.transform.parent = this.transform;
			a.transform.position = new Vector3(0,i * 20,0);
			CreatureController cc = a.GetComponentInChildren<CreatureController> ();
			cc.genes = c;
			cc.running = true;
			cc.testXAxis = testXAxis;
			cc.testYAxis = testYAxis;
			i++;
		}
	}

	void EndSimulation(){
		print ("END SIMULATION");
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponentInChildren<CreatureController> ().running = false;
			Creature c = transform.GetChild (i).GetComponentInChildren<CreatureController>().genes;
			currentGen.population [i] = c;
		}
	}

	void Start(){
		Generation g = new Generation (1);
		Creature a = new Creature ("Mark", 1, 1);
		a.RHF = new Function (1, 30, 2, 0);
		a.LHF = new Function (0, 30, 2, 9);
		g.population.Add (a);
		Creature b = new Creature ("Alan", 1, 1);
		g.population.Add (b);
		Creature c = new Creature ("Keith", 1, 1);
		g.population.Add (c);


		currentGen = g;
		StartCoroutine (RunSimulation());

	}

	IEnumerator RunSimulation(){
		
		StartSimulation ();
		yield return new WaitForSeconds (simulationLength);
		print ("END");
		EndSimulation ();
		currentGen.tested = true;
		foreach (Creature creature in currentGen.population) {
			print (creature.fitness);
		}
		Helper.quicksort (currentGen.population, 0, currentGen.population.Count - 1);
		currentGen.sorted = true;
		print ("SORTED");
		foreach (Creature creature in currentGen.population) {
			print (creature.fitness);
		}

	}
}
