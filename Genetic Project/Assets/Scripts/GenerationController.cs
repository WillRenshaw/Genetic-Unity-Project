﻿using System.Collections;
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
		foreach (Creature c in currentGen.GetPopulation()) {
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
		g.AppendCreature(c1);
		g.AppendCreature(c2);
		for (int i = 0; i < 10; i++) {
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
        //Get Best 3
        //Store Current Gen
        //Create New Gen
        //Call Self
	}
}
