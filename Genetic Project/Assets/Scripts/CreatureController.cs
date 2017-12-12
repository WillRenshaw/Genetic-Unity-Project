using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour {

    public Creature genes;
    public GameObject bodyPreset;
    public GameObject rightPreset;
    public GameObject leftPreset;


    const float jointOffset = 0.2f;
    GameObject rightHip;
    GameObject leftHip;


	public bool running = false;
	public bool testXAxis = false;
	public bool testYAxis = false;

	float maxY = 0f;

	public Text tagUI;


    private bool inverted = false;
    public void BuildCreature()
    {
        //CreateBody
        GameObject body = Instantiate(bodyPreset);
        body.name = "Body";
        body.transform.parent = this.transform;
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(genes.GetBodyLength(), 1, 1);


        //Create Right Side
        rightHip = new GameObject("Right Hip"); //Make Right Hip
        rightHip.transform.parent = this.transform;
        rightHip.transform.localPosition = new Vector3(genes.GetBodyLength() / 2 - jointOffset, 0, 0);
        GameObject rightUpperLeg = Instantiate(rightPreset);

        rightUpperLeg.name = "Right Upper Leg"; //Make Right Upper Leg
        rightUpperLeg.transform.parent = rightHip.transform;
        rightUpperLeg.transform.localScale = new Vector3(1, genes.GetRUpperLegLength(), 1);
        rightUpperLeg.transform.localPosition = new Vector3(0, -genes.GetRUpperLegLength(), 0);

        

        //Create Left Side
        leftHip = new GameObject("Left Hip"); //Make left Hip
        leftHip.transform.parent = this.transform;
        leftHip.transform.localPosition = new Vector3(-genes.GetBodyLength() / 2 + jointOffset, 0, 0);
        GameObject leftUpperLeg = Instantiate(leftPreset);

        leftUpperLeg.name = "Left Upper Leg"; //Make left Upper Leg
        leftUpperLeg.transform.parent = leftHip.transform;
        leftUpperLeg.transform.localScale = new Vector3(1, genes.GetLUpperLegLength(), 1);
        leftUpperLeg.transform.localPosition = new Vector3(0, -genes.GetLUpperLegLength(), 0);

    }
		

	void FixedUpdate(){
		if (running) {
			rightHip.transform.localRotation = Quaternion.AngleAxis (genes.GetRHF().GetValue (Time.time), Vector3.forward);
			leftHip.transform.localRotation = Quaternion.AngleAxis (genes.GetLHF().GetValue (Time.time), Vector3.forward);
			
            if(transform.localPosition.y < 0)
            {
                transform.Translate(new Vector3(0, -(transform.localPosition.y - 3), 0));
                Debug.Log("Prevented Boundary Movement");
            }
            else if(transform.localPosition.y > Helper.userPrefs.ecosystemSpacing)
            {
                transform.Translate(new Vector3(0, (float)Helper.userPrefs.ecosystemSpacing - transform.localPosition.y - 3, 0));
                Debug.Log("Prevented Boundary Movement");
            }
            if(GetComponent<Rigidbody>().velocity.magnitude > 50)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0,0, 0);
                Debug.Log("Velocity Set To 0");
            }
            if(transform.localPosition.magnitude > 9999)
            {
                transform.localPosition = new Vector3(0, 0, 0);
                Debug.Log("Set a position to 0");
            }


			genes.SetFitness(0);
            if (Mathf.Abs(this.transform.rotation.eulerAngles.z) > 100f)
            {
                
                inverted = true;
            }
           //else
           //{
            //   inverted = false;
           // }
            if (testXAxis){
				genes.SetFitness( genes.GetFitness() + Mathf.Pow(transform.position.x, 2));
                if(transform.position.x < 0)
                {
                    genes.SetFitness(genes.GetFitness() * -1);
                }
			}
			if (testYAxis && transform.localPosition.y > maxY) {
				maxY = transform.localPosition.y;
			}
            

            
            genes.SetFitness(genes.GetFitness() + maxY);
            if (inverted)
            {
                genes.SetFitness(genes.GetFitness() * 0.5f);
            }
            tagUI.text = genes.NAME + "\nFitness: " + (int)genes.GetFitness();
		}
	}

	void Start(){
		BuildCreature ();
	}
}
