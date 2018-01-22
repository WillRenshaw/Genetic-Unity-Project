using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Generation
{

    private List<Creature> population = new List<Creature>(); //List of all creatures in the generation
    private int genNumber; //The number of the generation
    public int GENNUMBER
    {
        get
        {
            return genNumber;
        }
    }
    private float meanFitness = 0; //The mean fitness of the generation
    public float MEANFITNESS
    {
        get
        {
            return meanFitness;
        }
    }
    public float MAXFITNESS
    {
        get
        {
            return maxFitness;
        }
    }
    private float maxFitness = 0; //The maximum fitness in the generation
    public float MINFITNESS
    {
        get
        {
            return minFitness;
        }
    }
    private float minFitness = 9999; //The minimum fitness in the generation
    public float SDFITNESS
    {
        get
        {
            return sdFitness;
        }
    }
    private float sdFitness = 0; //The standard deviation of the fitness in the generation
    private bool tested = false; //Has the simulation been run?
    public bool TESTED
    {
        get
        {
            return tested;
        }
    }
   	private bool sorted = false; //Has the list been ranked?
    public bool SORTED
    {
        get
        {
            return sorted;
        }
    }

    private static List<float> meanScores = new List<float>(); //Lists of mean scores, best scores and worst scores
    private static List<float> bestScores = new List<float>();
    private static List<float> worstScores = new List<float>();

    public Generation(int gNum) //Constructor
    {
        genNumber = gNum;
    }

	/// <summary>
	/// Marks the generation as tested.
	/// </summary>
    public void MarkTested()
    {
        tested = true;
    }

	/// <summary>
	/// Marks the generation as sorted
	/// </summary>
    public void MarkSorted()
    {
        sorted = true;
    }

	/// <summary>
	/// Add a creature to the generation
	/// </summary>
	/// <param name="c">The creature to add</param>
    public void AppendCreature(Creature c)
    {
        population.Add(c);
    }

	/// <summary>
	/// Retrives the population of the generation
	/// </summary>
	/// <returns>List of Creatures</returns>
    public List<Creature> GetPopulation()
    {
        return population;
    }
    
	/// <summary>
	/// Sets the population of the generation
	/// </summary>
	/// <param name="p">Population</param>
    public void SetPopulation(List<Creature> p)
    {
       population = p;
    }

	/// <summary>
	/// Calculates the mean, min and max fitnesses & standard deviation of fitnesses
	/// </summary>
    public void calculateStats()
    {
        int n = 0; //The number of creatures
        float sumFitness = 0; //Sum of the fitnesses
        float sumFitnessSquared = 0; //Sum of the fitnesses^2
        foreach (Creature c in population)
        {
            n++; //Increment n
			if (c.GetFitness () == float.NaN) {
				c.SetFitness (0); //Validates for NaN fitnesses
			}
            sumFitness += c.GetFitness(); //Add to sum of fitnesses and sum of fitnesses^2
            sumFitnessSquared += Mathf.Pow(c.GetFitness(), 2);
            //Checks if this fitnesss usurps min or max fitness
			if (c.GetFitness() < minFitness)
            {
                minFitness = c.GetFitness();
            }
            if(c.GetFitness() > maxFitness)
            {
                maxFitness = c.GetFitness();
            }
        }
		//calculate mean and standard deviation
        meanFitness = sumFitness / n;
        sdFitness = Mathf.Sqrt((sumFitnessSquared / n) - Mathf.Pow(meanFitness, 2));

		//add to lists
        meanScores.Add(meanFitness);
        bestScores.Add(maxFitness);
        worstScores.Add(minFitness);
    }
}
