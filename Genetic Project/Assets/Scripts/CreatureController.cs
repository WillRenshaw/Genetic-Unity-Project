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

    private float startTime;

    public Text tagUI;


    private void Start()
    {
        startTime = Time.time;
        print("START TIME IS: " + startTime);
    }


    void FixedUpdate(){
        if (running) {
            rightJoint.distance = Mathf.Clamp(2.5f + genes.GetRHF().GetAmplitude() + genes.GetRHF().GetValue(Time.time - startTime), 2.5f, 6f);
            leftJoint.distance = Mathf.Clamp(2.5f + genes.GetLHF().GetAmplitude() + genes.GetLHF().GetValue(Time.time - startTime), 2.5f, 6f);

            //calculate fitness
            genes.SetFitness(0);

            //is creature upright?
           bool headUp = body.transform.eulerAngles.z < 0 + 30 || body.transform.eulerAngles.z > 360 - 30;
           bool headDown = body.transform.eulerAngles.z > 180 - 30 && body.transform.eulerAngles.z < 180 + 30;

            //Set fitness
            genes.SetFitness(Mathf.Pow(body.transform.position.x, 1));

          
            //optionally give incentive for creature to remain upright
           // genes.SetFitness(genes.GetFitness() * (headDown ? 0.25f : 1f));
           
            if (headDown)
            {
                genes.SetFitness(genes.GetFitness() * 0.1f);

            }
            if (headUp)
            {
                //genes.SetFitness(genes.GetFitness() + 2f);
            }
        }

	}
    private void Update()
    {
        {
            tagUI.text = genes.NAME + "\nFitness: " + genes.GetFitness();
        }
    }

}
