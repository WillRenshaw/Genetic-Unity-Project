using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Generation
{

    public List<Creature> population = new List<Creature>(); //List of all creatures in the generation
    public int genNumber; //The number of the generation
    private float meanFitness = 0; //The mean fitness of the generation
    private float maxFitness = 0; //The maximum fitness in the generation
    private float minFitness = 0; //The minimum fitness in the generation
    private float sdFitness = 0; //The standard deviation of the fitness in the generation
    public bool tested = false; //Has the simulation been run?
   	public bool sorted = false; //Has the list been ranked?

    public Generation(int gNum)
    {
        genNumber = gNum;
    }
}
