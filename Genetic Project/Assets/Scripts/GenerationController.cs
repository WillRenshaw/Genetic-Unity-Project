using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class GenerationController : MonoBehaviour {
	private Generation currentGen; //The geneartion being tested
	public GameObject ecosystem; //Ecosystem prefab
    public Text genUI; //Text to display current info

	private const int ecosystemSpacing = 50; //How far apart should ecosystems be

    

	private float simulationLength; //How long simulations last
    private int generations; //Number of generations to test
    private int genSize; //The size of generations to be used

    /// <summary>
    /// Starts the simulation
    /// </summary>
	void StartSimulation(){
        foreach (Transform child in this.transform)
		{//Destroys all of last generation
            GameObject.Destroy(child.gameObject); 
        }
        Debug.Log ("STARTED SIMULATION AT: " + Time.time);
		int i = 0;
		//Spawns in new generation
		foreach (Creature c in currentGen.GetPopulation()) {
			GameObject eco = Instantiate (ecosystem);
			eco.name = "Ecosystem " + i;
			eco.transform.parent = this.transform;
			eco.transform.position = new Vector3(0,i * ecosystemSpacing,0);
			CreatureController cc = eco.GetComponentInChildren<CreatureController> ();
            cc.genes = c;
			cc.Run(true);
			i++;
		}
	}

	/// <summary>
	/// Ends the simulation
	/// </summary>
	void EndSimulation(){
        List<Creature> p = currentGen.GetPopulation();
        Debug.Log("ENDED SIMULATION AT: " + Time.time);
		//Updates the generation so that genetation contains fitnesses
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponentInChildren<CreatureController> ().Run(false);
			Creature c = transform.GetChild (i).GetComponentInChildren<CreatureController>().genes;
			p [i] = c;
		}
        currentGen.SetPopulation(p);
	}

	void Start(){
        Generation g = new Generation (1); //Define first generation
		Helper.savedGenerations.Clear (); //Delete previous saves
        if (File.Exists(Application.persistentDataPath + "/userprefs.gd"))
        { //See if there is existing user prefs
            Debug.Log("Read in Existing User Prefs");
            Helper.userPrefs = (UserPrefs)Helper.ReadData("/userprefs.gd");
        }
        else //if not default it
        {
            Debug.Log("Deafulted User Prefs");
			Helper.userPrefs = new UserPrefs()
			{
				initialBodyCV = 0.5,
				initialFunctionCV = 0.5,
				varianceMultiplier = 1,
				simulationLength = 10,
				generationSize = 50,
				iterationCount = 100
			};
            Helper.WriteToFile("/userprefs.gd", Helper.userPrefs);
        }
		//Sets settings based on user prefs
		simulationLength = (float)Helper.userPrefs.simulationLength;
		generations = Helper.userPrefs.iterationCount;
		genSize = Helper.userPrefs.generationSize;

		//Define parents for first generation
        List<Creature> parents = new List<Creature>();
        parents.Add(new Creature("", 1, 1)); //First is default creature
        parents.Add(Helper.CreateRandomCreature(1, 2)); //Second is a random creature
        g = CreateNewGeneration(genSize, parents, 1); //Create new creature based on parents
        currentGen = g;
        StartCoroutine (RunSimulation()); //begin simulation
    }

    IEnumerator RunSimulation() 
    {
        for (int i = 0; i < generations; i++) //Main loop
        {
			//Updates the generation so that genetation contains fitnesses
            StartSimulation();
            yield return new WaitForSeconds(simulationLength);
            EndSimulation();

            currentGen.MarkTested();
            List<Creature> p = currentGen.GetPopulation();
            Helper.quicksort(p, 0, p.Count - 1); //Sort population based on fitness
            p.Reverse(); //Change from ascending to descending
            currentGen.SetPopulation(p); //Replace with sorted list
            currentGen.MarkSorted();
            currentGen.calculateStats();

			//Define parents for next generation
            List<Creature> parents = new List<Creature>();
			//Add top 3 as seeder parents for next generation
            parents.Add(currentGen.GetPopulation()[0]);
            parents.Add(currentGen.GetPopulation()[1]);
            parents.Add(currentGen.GetPopulation()[2]);
            if (Random.Range(1, 11) == 10) { //1 in 10 chance of adding random parent
                Debug.Log("Random Seeder Parent Added");
                parents.Add(Helper.CreateRandomCreature(currentGen.GENNUMBER, 1));
            }
			//Set text to display stats
            genUI.text = ("Gen: " + (currentGen.GENNUMBER + 1) + "\nPrevious Mean Fitness: " + currentGen.MEANFITNESS + "\nPrevious Best: " + currentGen.MAXFITNESS + "\nPrevious Worst: " + currentGen.MINFITNESS);
            
            Helper.WriteGeneration(currentGen);//Store current generation
			//Create next generation
            currentGen = CreateNewGeneration(genSize, parents, currentGen.GENNUMBER + 1);
        }
    }


	/// <summary>
	/// Creates a new generation
	/// </summary>
	/// <returns>The new generation</returns>
	/// <param name="genSize">Size of the generation</param>
	/// <param name="parents">The seeder parents for the next generation</param>
	/// <param name="genNum">The number of this generation</param>
    private Generation CreateNewGeneration(int genSize, List<Creature> parents, int genNum)
    {

        Generation newGen = new Generation(genNum);
		//Add the best creature to the next one
        newGen.AppendCreature(parents[0]);

		//Loops through until there are at least enough creatures in this generation
        while (newGen.GetPopulation().Count < genSize)
        {
            int i = 0;
            foreach (Creature p1 in parents)
            { //Loops through all parents
                int j = 0;
                foreach (Creature p2 in parents)
                { //Loops through all parents again
                    newGen.AppendCreature(Helper.MateCreatures(p1, p2, genNum, i * j));
					//Adds a new creature made from the two parents
                }
                j++;
            }
            i++;
        }

        while (newGen.GetPopulation().Count > genSize)
        { //trims excess creatures off
            List<Creature> tempList = newGen.GetPopulation();
            tempList.RemoveAt(newGen.GetPopulation().Count - 1);
            newGen.SetPopulation(tempList);
        }
			
        return newGen;
    }

}
