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

    private static List<float> meanScores = new List<float>();
    private static List<float> bestScores = new List<float>();
    private static List<float> worstScores = new List<float>();

    public Generation(int gNum)
    {
        genNumber = gNum;
    }

    public void MarkTested()
    {
        tested = true;
    }

    public void MarkSorted()
    {
        sorted = true;
    }

    public void AppendCreature(Creature c)
    {
        population.Add(c);
    }

    public List<Creature> GetPopulation()
    {
        return population;
    }
    
    public void SetPopulation(List<Creature> p)
    {
       population = p;
    }

    public void calculateStats()
    {
        int n = 0;
        float sumFitness = 0;
        float sumFitnessSquared = 0;
        foreach (Creature c in population)
        {
            n++;
            sumFitness += c.GetFitness();
            sumFitnessSquared += Mathf.Pow(c.GetFitness(), 2);
            if (c.GetFitness() < minFitness)
            {
                minFitness = c.GetFitness();
            }
            if(c.GetFitness() > maxFitness)
            {
                maxFitness = c.GetFitness();
            }
        }
        meanFitness = sumFitness / n;
        sdFitness = Mathf.Sqrt((sumFitnessSquared / n) - Mathf.Pow(meanFitness, 2));

        meanScores.Add(meanFitness);
        bestScores.Add(maxFitness);
        worstScores.Add(minFitness);
    }
}
