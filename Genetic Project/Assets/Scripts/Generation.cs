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
    private float maxFitness = 0; //The maximum fitness in the generation
    private float minFitness = 0; //The minimum fitness in the generation
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
}
