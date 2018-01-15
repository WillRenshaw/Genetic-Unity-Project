using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour {

    float maxHeight = 50; //Height up / down from 0
    float maxWidth = 160; //Width from 0 onwards
	public Transform linePrefab;
	public Transform lineMarker;
    private LineRenderer meanLine;
	private LineRenderer maxLine;
	private LineRenderer minLine;


	void Start()
	{
		meanLine = Instantiate (linePrefab, transform).GetComponent<LineRenderer> ();
		meanLine.gameObject.name = "Mean";
		meanLine.startColor = Color.red;
		meanLine.endColor = Color.red;
		maxLine = Instantiate (linePrefab, transform).GetComponent<LineRenderer> ();
		maxLine.gameObject.name = "Max";
		maxLine.startColor = Color.blue;
		maxLine.endColor = Color.blue;
		minLine = Instantiate (linePrefab, transform).GetComponent<LineRenderer> ();
		minLine.gameObject.name = "Min";
		minLine.startColor = Color.green;
		minLine.endColor = Color.green;
		DrawLines (Helper.savedGenerations.ToArray ());
	}


	public void DrawLines(Generation[] gens){
		List<Coordinate> coords = new List<Coordinate> ();
		foreach (Generation gen in gens) {
			coords.Add(new Coordinate(){genNum = gen.GENNUMBER, mean = gen.MEANFITNESS, max = gen.MAXFITNESS, min = gen.MINFITNESS});
		}

		float xToWorldSpace = maxWidth / coords.Count;
		float maxY = 0;
		foreach (Coordinate c in coords) {
			if (c.max > maxY) {
				maxY = c.max;
			}
		}
		float yToWorldSpace = maxHeight / maxY;
		meanLine.positionCount = coords.Count;
		maxLine.positionCount = coords.Count;
		minLine.positionCount = coords.Count;
		foreach (Coordinate c in coords) {
			if (c.genNum % 5 == 0 || c.genNum == 1) {
				Transform tag = Instantiate (lineMarker,transform);
				tag.transform.position = new Vector3 ((c.genNum - 1) * xToWorldSpace, 0, 0);
				tag.GetComponentInChildren<Text> ().text = c.genNum.ToString();
			}
			meanLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.mean * yToWorldSpace, 0));
			maxLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.max * yToWorldSpace, 0));
			minLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.min * yToWorldSpace, 0));

		}
	}

  
}
public struct Coordinate
{
	public int genNum;
    public float mean;
	public float max;
	public float min;
}
