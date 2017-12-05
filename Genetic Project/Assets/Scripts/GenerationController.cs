using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerationController : MonoBehaviour {
	public Generation currentGen;
	public GameObject ecosystem;


	[Range(0f, 100f)]
	public float simulationLength = 10f;
	public bool XAxis = false;
	public bool YAxis = false;

	void StartSimulation(){
		Debug.Log ("STARTED SIMULATION AT: " + Time.time);
		int i = 0;
		foreach (Creature c in currentGen.population) {
			GameObject eco = Instantiate (ecosystem);
			eco.name = "Ecosystem " + i;
			eco.transform.parent = this.transform;
			eco.transform.position = new Vector3(0,i * 50,0);
			CreatureController cc = eco.GetComponentInChildren<CreatureController> ();
            cc.genes = c;
			cc.running = true;
			cc.testXAxis = XAxis;
			cc.testYAxis = YAxis;
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
		Helper.userPrefs = new UserPrefs () {
			initialBodyCV = 1,
			furtherBodyCV = 1,
			initialFunctionCV = 1,
			furtherFunctionCV = 1
		};
		Helper.WriteToFile ("/userPrefs.gd", Helper.userPrefs);
		Helper.userPrefs = (UserPrefs)Helper.ReadData ("/userPrefs.gd");

        Generation g = new Generation (1);
        
		Creature c1 = new Creature("c1", 1, 1);
		Creature c2 = new Creature("c2", 1, 2);
		g.population.Add(c1);
		g.population.Add(c2);
		for (int i = 0; i < 100; i++) {
			Creature c3 = Helper.MateCreatures (c1, c2, g.genNumber, i + 3);
			g.population.Add(c3);
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
