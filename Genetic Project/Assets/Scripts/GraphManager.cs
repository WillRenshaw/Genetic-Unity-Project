using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour {

	public Transform linePrefab; //Prefabs for the lineRenderer and marker
	public Transform lineMarker;
    private LineRenderer meanLine;
	private LineRenderer maxLine;
	private LineRenderer minLine;


	void Start()
	{
		//Create the three lines and assign to appropriate variables
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
		//Calls drawLines with the most recent test results
		DrawLines (Helper.savedGenerations.ToArray ());
	}

	/// <summary>
	/// Draws the graph lines
	/// </summary>
	/// <param name="gens">The results to draw</param>
	public void DrawLines(Generation[] gens){
		List<Coordinate> coords = new List<Coordinate> ();
		foreach (Generation gen in gens) { //Convert all gens to coordinates
			coords.Add(new Coordinate(){genNum = gen.GENNUMBER, mean = gen.MEANFITNESS, max = gen.MAXFITNESS, min = gen.MINFITNESS});
		}
		meanLine.positionCount = coords.Count; //Set the size of the position arrays appropriately
		maxLine.positionCount = coords.Count;
		minLine.positionCount = coords.Count;
		foreach (Coordinate c in coords) {
			if (c.genNum % 10 == 0 || c.genNum == 1) { //Place marker tags for every 5th generation
				Transform tag = Instantiate (lineMarker,transform);
				tag.transform.position = new Vector3 ((c.genNum - 1), -2.5f, 0);
				tag.GetComponentInChildren<Text> ().text = c.genNum.ToString();
			}
			meanLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1), c.mean, 0)); //Set mean Position
			maxLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1), c.max, 0)); //Set max Position
			minLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1), c.min, 0)); //Set min Position
		}
		for (int i = 0; i <= 100; i++) { //Set tags on y axis every 5 units
			Transform tag = Instantiate (lineMarker,transform);
			tag.transform.position = new Vector3 (-5, 5 * i, 0);
			tag.GetComponentInChildren<Text> ().text = (5 * i).ToString();
			tag = Instantiate (lineMarker,transform);
			tag.transform.position = new Vector3 (-5, -5 * i, 0);
			tag.GetComponentInChildren<Text> ().text = (-5 * i).ToString ();
		}
	}

  
}
/// <summary>
/// Coordinate
/// </summary>
public struct Coordinate
{
	public int genNum;
    public float mean;
	public float max;
	public float min;
}
