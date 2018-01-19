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
		foreach (Generation gen in gens) { //Convert all gens to coordinates
			coords.Add(new Coordinate(){genNum = gen.GENNUMBER, mean = gen.MEANFITNESS, max = gen.MAXFITNESS, min = gen.MINFITNESS});
		}

		float xToWorldSpace = maxWidth / coords.Count; //Constant to convert coordinate x vals to unity world space
		float maxY = 0;
		foreach (Coordinate c in coords) { //Find the greatest max value
			if (c.max > maxY) {
				maxY = c.max;
			}
		}
		float yToWorldSpace = maxHeight / maxY; //Constant to convert coordinate y vals to unity world space
		meanLine.positionCount = coords.Count; //Set the size of the position arrays appropriately
		maxLine.positionCount = coords.Count;
		minLine.positionCount = coords.Count;
		foreach (Coordinate c in coords) {
			if (c.genNum % 5 == 0 || c.genNum == 1) { //Place marker tags for every 5th generation
				Transform tag = Instantiate (lineMarker,transform);
				tag.transform.position = new Vector3 ((c.genNum - 1) * xToWorldSpace, 0, 0);
				tag.GetComponentInChildren<Text> ().text = c.genNum.ToString();
			}
			meanLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.mean * yToWorldSpace, 0)); //Set mean Position
			maxLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.max * yToWorldSpace, 0)); //Set max Position
			minLine.SetPosition (c.genNum - 1, new Vector3 ((c.genNum - 1) * xToWorldSpace, c.min * yToWorldSpace, 0)); //Set min Position

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
