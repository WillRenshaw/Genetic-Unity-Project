using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper{
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


    public static float GaussianSample (float mean, float std)
    {
        float u1 = 1 - Random.Range(0f, 1f);
        float u2 = 1 - Random.Range(0f, 1f);
        float z = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Cos(2 * Mathf.PI * u2);
        float u = mean + (std * z);
        return u;
    }
}
