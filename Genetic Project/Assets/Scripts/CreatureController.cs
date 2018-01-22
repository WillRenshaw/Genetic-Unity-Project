using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour {

    public Creature genes; //The Creature that is to be tested
    public GameObject bodyPreset; //Presets that are assigned in inspector
    public GameObject rightPreset;
    public GameObject leftPreset;
	public Text tagUI; //The text used to display name and fitness

    const float jointOffset = 0.2f; //Offset used for joints
    
	private GameObject rightHip; //The joint gameobjects that get rotated
    private GameObject leftHip;
    private GameObject rightKnee;
    private GameObject leftKnee;

	private bool running = false; //Is simulation running?
    private float startTime; //The start time of the simulation
    private bool inverted = false; //Has the creature been inverted?

	/// <summary>
	/// Builds the creature.
	/// </summary>
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

        rightKnee = new GameObject("Right Knee"); //Make Right Knee
        rightKnee.transform.parent = rightHip.transform;
        rightKnee.transform.localPosition = new Vector3(0, -(2 * genes.GetRUpperLegLength() - jointOffset), 0);
        GameObject rightLowerLeg = Instantiate(rightPreset);

        rightLowerLeg.name = "Right Lower Leg"; //Make Right Lower Leg
        rightLowerLeg.transform.parent = rightKnee.transform;
        rightLowerLeg.transform.localScale = new Vector3(1, genes.GetRLowerLegLength(), 1);
        rightLowerLeg.transform.localPosition = new Vector3(0, -genes.GetRLowerLegLength(), 0);

        //Create Left Side
        leftHip = new GameObject("Left Hip"); //Make left Hip
        leftHip.transform.parent = this.transform;
        leftHip.transform.localPosition = new Vector3(-genes.GetBodyLength() / 2 + jointOffset, 0, 0);
        GameObject leftUpperLeg = Instantiate(leftPreset);

        leftUpperLeg.name = "Left Upper Leg"; //Make left Upper Leg
        leftUpperLeg.transform.parent = leftHip.transform;
        leftUpperLeg.transform.localScale = new Vector3(1, genes.GetLUpperLegLength(), 1);
        leftUpperLeg.transform.localPosition = new Vector3(0, -genes.GetLUpperLegLength(), 0);

        leftKnee = new GameObject("Left Knee"); //Make left Knee
        leftKnee.transform.parent = leftHip.transform;
        leftKnee.transform.localPosition = new Vector3(0, -(2 * genes.GetLUpperLegLength() - jointOffset), 0);
        GameObject leftLowerLeg = Instantiate(leftPreset);

        leftLowerLeg.name = "Left Lower Leg"; //Make left Lower Leg
        leftLowerLeg.transform.parent = leftKnee.transform;
        leftLowerLeg.transform.localScale = new Vector3(1, genes.GetLLowerLegLength(), 1);
        leftLowerLeg.transform.localPosition = new Vector3(0, -genes.GetLLowerLegLength(), 0);
    }
		

	void FixedUpdate(){
		if (running) { //Checks if running
			//Rotates joints
			rightHip.transform.localRotation = Quaternion.AngleAxis (genes.GetRHF().GetValue (Time.time - startTime), Vector3.forward);
			leftHip.transform.localRotation = Quaternion.AngleAxis (genes.GetLHF().GetValue (Time.time - startTime), Vector3.forward);
			rightKnee.transform.localRotation = Quaternion.AngleAxis (genes.GetRKF().GetValue (Time.time - startTime), Vector3.forward);
			leftKnee.transform.localRotation = Quaternion.AngleAxis (genes.GetLKF().GetValue (Time.time - startTime), Vector3.forward);



			//Checks is body is between 120 and 240 degrees, if it is, sets inverted to true
            if(this.transform.rotation.eulerAngles.z > 120 && this.transform.rotation.eulerAngles.z < 240)
            {
                inverted = true;
            }

			genes.SetFitness(0); //Resets fitness
			//Sets fitness to x position
			genes.SetFitness( genes.GetFitness() + transform.position.x);


			//If creature is inverted halves fitness
            if (inverted)
            {
                genes.SetFitness(genes.GetFitness() * 0.5f);
            }

			//Sets tag to display name and fitness
			tagUI.text = genes.NAME + "\nFitness: " + (int)genes.GetFitness();
		}
	}

	void Start(){
		BuildCreature (); //Creature is built when creature controller is created
        startTime = Time.time; //Gets start time
	}

	public void Run(bool r){
		running = r;
	}

}
