using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Helper{

    public static List<Generation> savedGenerations = new List<Generation>();
	public static UserPrefs userPrefs;

    private static int partition(List<Creature> A, int lo, int hi){
		int i = lo - 1;
		Creature pivot = A [hi];
		for (int j = lo; j < hi; j++) {
			if (A [j].GetFitness() < pivot.GetFitness()) {
				i += 1;
				Creature temp = A [i];
				A [i] = A [j];
				A[j] = temp;
			}
		}
		if (A [hi].GetFitness() < A [i + 1].GetFitness()) {
			Creature temp = A [i + 1];
			A [i+1] = A [hi];
			A[hi] = temp;
		}
		return i + 1;
	}
	/// <summary>
	/// Quicksort the list of Creatures into ascending order
	/// </summary>
	/// <param name="A">The List to be sorted.</param>
	/// <param name="lo">The first index to be sorted.</param>
	/// <param name="hi">The end index.</param>
	public static void quicksort(List<Creature> A, int lo, int hi){
		if (lo < hi) {
			int p = partition (A, lo, hi);
			quicksort(A, lo, p-1);
			quicksort (A, p + 1, hi);
		}
	}

    /// <summary>
    /// Returns a random number that tends to the given mean
    /// </summary>
    /// <param name="mean">The mean of the ND</param>
    /// <param name="std">The standard deviation of the ND</param>
    /// <returns>Returns a normally distributed number</returns>
    public static float GaussianSample (float mean, float std)
    {
        std *= (float)userPrefs.varianceMultiplier;
        float u1 = 1 - Random.Range(0f, 1f);
        float u2 = 1 - Random.Range(0f, 1f);
        float z = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Cos(2 * Mathf.PI * u2);
        float u = mean + (std * z);
        return u;
    }


    public static void WriteGeneration(Generation gen)
    {
        savedGenerations.Add(gen);
		WriteToFile("/savedgenerations.gd", savedGenerations);
        Debug.Log("Saved Generation " + gen.GENNUMBER  + " to " +  Application.persistentDataPath + "/savedgenerations.gd");
    }

	public static void WriteToFile(string fName, object data){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + fName);
		bf.Serialize(file, data);
		file.Close();
	}

    public static void ReadGenerations()
    {
		savedGenerations = (List<Generation>)ReadData("/savedgenerations.gd");
        Debug.Log("Read Generations From " + Application.persistentDataPath + "/savedgenerations.gd");
    }

	public static object ReadData(string fName)
	{
		if(File.Exists(Application.persistentDataPath + fName))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + fName, FileMode.Open);
			object toReturn = bf.Deserialize(file);
			file.Close();
			return toReturn;
		}
		return null;
	}

	private static float RandomFunctionValue(float mean = 50f){
		return Mathf.Clamp (GaussianSample (mean, mean * (float)userPrefs.initialFunctionCV), 0.1f, 200f);
	}

	private static float RandomBodyValue(float mean = 2f){
		return Mathf.Clamp (GaussianSample (mean,mean * (float)userPrefs.initialBodyCV), 0.2f, 5f);
	}

	private static string GetRandomName(){
		string[] names = File.ReadAllLines(Application.dataPath + "/Prefabs/names.txt");
		return names [Random.Range (0, names.Length - 1)];
	}

    public static void writeScore(float best, float mean)
    {
        var meanList = new List<string>(File.ReadAllLines(Application.dataPath + "/mean.txt"));
        var bestList = new List<string>(File.ReadAllLines(Application.dataPath + "/best.txt"));
        meanList.Add(mean.ToString());
        bestList.Add(best.ToString());

        TextWriter tw = new StreamWriter(Application.dataPath + "/mean.txt");

        foreach (string s in meanList)
            tw.WriteLine(s);

        tw.Close();
        tw = new StreamWriter(Application.dataPath + "/best.txt");

        foreach (string s in bestList)
            tw.WriteLine(s);

        tw.Close();

    }

    /// <summary>
    /// Creates a random creature
    /// </summary>
    /// <param name="gen">The gen number of the creature</param>
    /// <param name="ID">The ID num of the creature</param>
    /// <returns></returns>
    public static Creature CreateRandomCreature(int gen, int ID)
    {

		Creature c = new Creature(GetRandomName(), gen, ID);
        if(Random.Range(1,100) % 2 == 0)
        {
			c.SetRHF(new SinWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        else
        {
			c.SetRHF(new TriangleWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        if (Random.Range(1, 100) % 2 == 0)
        {
			c.SetLHF(new SinWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        else
        {
			c.SetLHF(new TriangleWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }


		c.SetBodyLength(RandomBodyValue (4));
		c.SetRUpperLegLength(RandomBodyValue (2));
		c.SetLUpperLegLength(RandomBodyValue (2));


        return c;


    }

    /// <summary>
    /// Combines two functions
    /// </summary>
    /// <param name="f1">First Function</param>
    /// <param name="f2">Second Function</param>
    /// <returns></returns>
	private static Function MateFunction(Function f1, Function f2){
        
        float a = (f1.GetAmplitude() + f2.GetAmplitude()) / 2;
        float w = (f1.GetWavelength() + f2.GetWavelength()) / 2;
        float p = (f1.GetPhase() + f2.GetPhase()) / 2;
        //Choose a random variable to mutate
        switch (Random.Range(0, 2 + 1))
        {
            case 0:
                a += Random.Range(-0.01f, 0.01f);
                a = Mathf.Clamp(a, -1f, +1f);
                break;
            case 1:
                p += Random.Range(-0.01f, 0.01f);
                p = Mathf.Clamp(p, -2f, 2f);
                break;
            case 2:
                w += Random.Range(-0.01f, 0.01f);
                w = Mathf.Clamp(w, 0.1f, 2f);
                break;

        }

        return new SinWave(a, w, p);
    }

    /// <summary>
    /// Combines two creatures and returns a child with their properties
    /// </summary>
    /// <param name="c1">First Creature</param>
    /// <param name="c2">Second Creature</param>
    /// <param name="gen">The child creature's generation num</param>
    /// <param name="ID">The child creature's ID num</param>
    /// <returns></returns>
	public static Creature MateCreatures(Creature c1, Creature c2, int gen, int ID){
		Creature child = new Creature (GetRandomName(), gen, ID);
        child.SetRHF(MateFunction(c1.GetRHF(), c2.GetRHF()));
        child.SetLHF(MateFunction (c1.GetLHF(), c2.GetLHF()));
        return child;
        
    }
}

[System.Serializable]
public struct UserPrefs{
	public double initialBodyCV;
	public double furtherBodyCV;
	public double initialFunctionCV;
	public double furtherFunctionCV;
    public double ecosystemSpacing;
    public double varianceMultiplier;
}