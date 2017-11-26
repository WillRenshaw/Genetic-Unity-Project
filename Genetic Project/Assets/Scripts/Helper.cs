using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Helper{

    public static List<Generation> savedGenerations = new List<Generation>();
    public static TextAsset names;
    private static int partition(List<Creature> A, int lo, int hi){
		int i = lo - 1;
		Creature pivot = A [hi];
		for (int j = lo; j < hi; j++) {
			if (A [j].fitness < pivot.fitness) {
				i += 1;
				Creature temp = A [i];
				A [i] = A [j];
				A[j] = temp;
			}
		}
		if (A [hi].fitness < A [i + 1].fitness) {
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
        float u1 = 1 - Random.Range(0f, 1f);
        float u2 = 1 - Random.Range(0f, 1f);
        float z = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Cos(2 * Mathf.PI * u2);
        float u = mean + (std * z);
        return u;
    }


    public static void WriteGeneration(Generation gen)
    {
        savedGenerations.Add(gen);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedgenerations.gd");
        Debug.Log("Saved Generation " + gen.genNumber  + " to " +  Application.persistentDataPath + "/savedgenerations.gd");
        bf.Serialize(file, savedGenerations);
        file.Close();
    }

    public static void ReadGenerations()
    {
        if(File.Exists(Application.persistentDataPath + "/savedgenerations.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedgenerations.gd", FileMode.Open);
            Debug.Log("Read Generations From " + Application.persistentDataPath + "/savedgenerations.gd");
            savedGenerations = (List<Generation>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static Creature CreateRandomCreature(int gen, int ID)
    {
        string[] names = File.ReadAllLines(Application.dataPath + "/Prefabs/names.txt");
        Creature c = new Creature(names[Random.RandomRange(0, names.Length - 1)], gen, ID);
        if(Random.Range(1,100) % 2 == 0)
        {
            c.RHF = new SinWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        else
        {
            c.RHF = new TriangleWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        if (Random.Range(1, 100) % 2 == 0)
        {
            c.LHF = new SinWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        else
        {
            c.LHF = new TriangleWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        if (Random.Range(1, 100) % 2 == 0)
        {
            c.RKF = new SinWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        else
        {
            c.RKF = new TriangleWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        if (Random.Range(1, 100) % 2 == 0)
        {
            c.LKF = new SinWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }
        else
        {
            c.LKF = new TriangleWave(Random.Range(0.000001f, 180f), Random.Range(0.000001f, 100f), Random.Range(0, 360));
        }

        c.bodyLength = Mathf.Clamp(GaussianSample(5, 3), 0.00001f, 100f);
        c.RUpperLegLength = Mathf.Clamp(GaussianSample(2, 1), 0.00001f, 100f);
        c.LUpperLegLength = Mathf.Clamp(GaussianSample(2, 1), 0.00001f, 100f);
        c.RLowerLegLength = Mathf.Clamp(GaussianSample(1, 0.5f), 0.00001f, 100f);
        c.LLowerLegLength = Mathf.Clamp(GaussianSample(1, 0.5f), 0.00001f, 100f);

        return c;
    }
}
