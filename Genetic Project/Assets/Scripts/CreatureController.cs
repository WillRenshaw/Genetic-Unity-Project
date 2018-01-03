using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour {

    public Creature genes; //The underlying variables of the creature
    public DistanceJoint2D rightJoint;
    public DistanceJoint2D leftJoint;
    public GameObject body;

    public bool running = true;

	public Text tagUI;


	void FixedUpdate(){
		if (running) {
            rightJoint.distance = Mathf.Clamp(0.55f + genes.GetRHF().GetAmplitude() + genes.GetRHF().GetValue(Time.time), 0.55f, 1.05f);
            leftJoint.distance = Mathf.Clamp(0.55f + genes.GetLHF().GetAmplitude() + genes.GetLHF().GetValue(Time.time), 0.55f, 1.05f);

            //calcxulate fitness
            genes.SetFitness(0);

            //is creature upright?
            bool headUp = body.transform.eulerAngles.z < 0 + 30 || this.transform.eulerAngles.z > 360 - 30;
            bool headDown = body.transform.eulerAngles.z > 180 - 45 && this.transform.eulerAngles.z < 180 + 45;

            //Set fitness
            genes.SetFitness(body.transform.position.x);

       
            //optionally give incentive for creature to remain upright
            //genes.SetFitness(genes.GetFitness() * (headDown ? 0.5f : 1f) * (headUp ? 2f : 1f));

            tagUI.text = genes.NAME + "\nFitness: " +  genes.GetFitness();
		}
	}

}
