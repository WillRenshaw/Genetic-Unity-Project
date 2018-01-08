﻿using System.Collections;
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

	public static void WriteToFile(string fName, object data)
    {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + fName);
		bf.Serialize(file, data);
		file.Close();
	}

    public static void WriteScores(float mean, float best, float worst)
    {
        WriteLine(mean.ToString(), Application.dataPath + "/mean.txt");
        WriteLine(best.ToString(), Application.dataPath + "/best.txt");
        WriteLine(worst.ToString(), Application.dataPath + "/worst.txt");
    }


    private static void WriteLine(string value, string fName)
    {
        StreamWriter writer = new StreamWriter(fName, true);
        writer.WriteLine(value);
        writer.Close();
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
        if (Random.Range(1, 100) % 2 == 0)
        {
			c.SetRKF(new SinWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        else
        {
			c.SetRKF(new TriangleWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        if (Random.Range(1, 100) % 2 == 0)
        {
			c.SetLKF(new SinWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }
        else
        {
			c.SetLKF(new TriangleWave(RandomFunctionValue(), RandomFunctionValue(), Random.Range(0, 360)));
        }

		c.SetBodyLength(RandomBodyValue (4));
		c.SetRUpperLegLength(RandomBodyValue (2));
		c.SetLUpperLegLength(RandomBodyValue (2));
		c.SetRLowerLegLength(RandomBodyValue (1));
		c.SetLLowerLegLength(RandomBodyValue (1));

        return c;


    }

    /// <summary>
    /// Combines two functions
    /// </summary>
    /// <param name="f1">First Function</param>
    /// <param name="f2">Second Function</param>
    /// <returns></returns>
	private static Function MateFunction(Function f1, Function f2){
		Function f;
		float typeMean = 50;
		if (f1.GetType () == typeof(TriangleWave)) {
			typeMean -= 20;
		} else {
			typeMean += 20;
		}
		if (f2.GetType () == typeof(TriangleWave)) {
			typeMean -= 20;
		} else {
			typeMean += 20;
		}
		if (GaussianSample (typeMean, 30f) <= 50) {
			f = new TriangleWave (Mathf.Clamp(GaussianSample ((f1.GetAmplitude() + f2.GetAmplitude()) / 2, Mathf.Abs (f1.GetAmplitude() - f2.GetAmplitude())), 0, 90), Mathf.Clamp(GaussianSample ((f1.GetWavelength() + f2.GetWavelength()) / 2, Mathf.Abs (f1.GetWavelength() - f2.GetWavelength())), 2f, 5f), GaussianSample ((f1.GetPhase() + f2.GetPhase()) / 2, Mathf.Abs (f1.GetPhase() - f2.GetPhase())));
		} else {
			f = new SinWave(Mathf.Clamp(GaussianSample((f1.GetAmplitude() + f2.GetAmplitude()) / 2, Mathf.Abs(f1.GetAmplitude() - f2.GetAmplitude())), 0, 90), Mathf.Clamp(GaussianSample((f1.GetWavelength() + f2.GetWavelength()) / 2, Mathf.Abs(f1.GetWavelength() - f2.GetWavelength())), 2f, 5f), GaussianSample((f1.GetPhase() + f2.GetPhase()) / 2, Mathf.Abs(f1.GetPhase() - f2.GetPhase())));
        }

		return f;
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
		child.SetRKF(MateFunction (c1.GetRKF(), c2.GetRKF()));
		child.SetLKF(MateFunction (c1.GetLKF(), c2.GetLKF()));
        child.SetBodyLength(Mathf.Clamp(GaussianSample((c1.GetBodyLength() + c2.GetBodyLength()) / 2, Mathf.Abs(c1.GetBodyLength() - c2.GetBodyLength())), 0.1f, 6f));
		child.SetRUpperLegLength(Mathf.Clamp(GaussianSample ((c1.GetRUpperLegLength() + c2.GetRUpperLegLength()) / 2, Mathf.Abs (c1.GetRUpperLegLength() - c2.GetRUpperLegLength())), 0.1f, 3f));
		child.SetLUpperLegLength(Mathf.Clamp(GaussianSample ((c1.GetLUpperLegLength() + c2.GetLUpperLegLength()) / 2, Mathf.Abs (c1.GetLUpperLegLength() - c2.GetLUpperLegLength())), 0.1f, 3f));
		child.SetRLowerLegLength(Mathf.Clamp(GaussianSample ((c1.GetRLowerLegLength() + c2.GetRLowerLegLength()) / 2, Mathf.Abs (c1.GetRLowerLegLength() - c2.GetRLowerLegLength())), 0.1f, 3f));
		child.SetLLowerLegLength(Mathf.Clamp(GaussianSample ((c1.GetLLowerLegLength() + c2.GetLLowerLegLength()) / 2, Mathf.Abs (c1.GetLLowerLegLength() - c2.GetLLowerLegLength())), 0.1f, 3f));
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