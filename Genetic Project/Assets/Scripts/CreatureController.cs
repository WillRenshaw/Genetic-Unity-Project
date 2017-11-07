using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour {

    public Creature genes;
    public GameObject bodyPreset;
    public GameObject rightPreset;
    public GameObject leftPreset;


    const float jointOffset = 0.2f;
    GameObject rightHip;
    GameObject leftHip;
    GameObject rightKnee;
    GameObject leftKnee;

    public void BuildCreature()
    {
        //CreateBody
        GameObject body = Instantiate(bodyPreset);
        body.name = "Body";
        body.transform.parent = this.transform;
        body.transform.localPosition = Vector3.zero;
        body.transform.localScale = new Vector3(genes.bodyLength, 1, 1);


        //Create Right Side
        rightHip = new GameObject("Right Hip"); //Make Right Hip
        rightHip.transform.parent = this.transform;
        rightHip.transform.localPosition = new Vector3(genes.bodyLength / 2 - jointOffset, 0, 0);
        GameObject rightUpperLeg = Instantiate(rightPreset);

        rightUpperLeg.name = "Right Upper Leg"; //Make Right Upper Leg
        rightUpperLeg.transform.parent = rightHip.transform;
        rightUpperLeg.transform.localScale = new Vector3(1, genes.RUpperLegLength, 1);
        rightUpperLeg.transform.localPosition = new Vector3(0, -genes.RUpperLegLength, 0);

        rightKnee = new GameObject("Right Knee"); //Make Right Knee
        rightKnee.transform.parent = rightHip.transform;
        rightKnee.transform.localPosition = new Vector3(0, -(2 * genes.RUpperLegLength - jointOffset), 0);
        GameObject rightLowerLeg = Instantiate(rightPreset);

        rightLowerLeg.name = "Right Lower Leg"; //Make Right Lower Leg
        rightLowerLeg.transform.parent = rightKnee.transform;
        rightLowerLeg.transform.localScale = new Vector3(1, genes.RLowerLegLength, 1);
        rightLowerLeg.transform.localPosition = new Vector3(0, -genes.RLowerLegLength, 0);

        //Create Left Side
        leftHip = new GameObject("Left Hip"); //Make left Hip
        leftHip.transform.parent = this.transform;
        leftHip.transform.localPosition = new Vector3(-genes.bodyLength / 2 + jointOffset, 0, 0);
        GameObject leftUpperLeg = Instantiate(leftPreset);

        leftUpperLeg.name = "Left Upper Leg"; //Make left Upper Leg
        leftUpperLeg.transform.parent = leftHip.transform;
        leftUpperLeg.transform.localScale = new Vector3(1, genes.LUpperLegLength, 1);
        leftUpperLeg.transform.localPosition = new Vector3(0, -genes.LUpperLegLength, 0);

        leftKnee = new GameObject("Left Knee"); //Make left Knee
        leftKnee.transform.parent = leftHip.transform;
        leftKnee.transform.localPosition = new Vector3(0, -(2 * genes.LUpperLegLength - jointOffset), 0);
        GameObject leftLowerLeg = Instantiate(leftPreset);

        leftLowerLeg.name = "Left Lower Leg"; //Make left Lower Leg
        leftLowerLeg.transform.parent = leftKnee.transform;
        leftLowerLeg.transform.localScale = new Vector3(1, genes.LLowerLegLength, 1);
        leftLowerLeg.transform.localPosition = new Vector3(0, -genes.LLowerLegLength, 0);
    }

    void Start()
    {
        BuildCreature();//Test
		genes = new Creature ("Subject 1", 1, 1);
    }

	void FixedUpdate(){
		rightHip.transform.localRotation = Quaternion.AngleAxis (genes.RHF.GetValue (Time.time), Vector3.forward);
		leftHip.transform.localRotation = Quaternion.AngleAxis (genes.LHF.GetValue (Time.time), Vector3.forward);
		rightKnee.transform.localRotation = Quaternion.AngleAxis (genes.RKF.GetValue (Time.time), Vector3.forward);
		leftKnee.transform.localRotation = Quaternion.AngleAxis (genes.LKF.GetValue (Time.time), Vector3.forward);
	}
}
