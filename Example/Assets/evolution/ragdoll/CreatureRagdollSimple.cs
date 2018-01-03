using UnityEngine;
using System.Collections;

public class CreatureRagdollSimple : Creature
{

    public void Start()
    {
        express = Gene.Evaluate4At;
    }

    public GameObject head;
    //public GameObject body;

    //public GameObject footL;
    //public GameObject footR;


    //public GameObject legL;
    //public GameObject legR;

    public float headUpTime = 0;
    //public float bodyUpTime = 0;

    //public float feetUpTime = 0;

    //public float feetLegsYDistance = 0;

    //public float feetAboveLegs = 0;
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Body and head UP!
        if (IsUp(head, 20))
            headUpTime += Time.fixedDeltaTime;
        /*
        if (IsUp(body, 20))
            bodyUpTime += Time.fixedDeltaTime;

        // Feet UP!
        if (IsUp(footL, 20) || IsUp(footR, 20))
            feetUpTime += Time.fixedDeltaTime;

        // Feet above leg? BAD!
        if (footL.transform.position.y > legL.transform.position.y)
            feetAboveLegs += Time.fixedDeltaTime / 2;
        if (footR.transform.position.y > legR.transform.position.y)
            feetAboveLegs += Time.fixedDeltaTime / 2;
            */

    }

    public override float GetScore ()
    {
        //return head.transform.position.x;
        float position = head.transform.position.x;
        //float position = head.transform.localPosition.y + body.transform.localPosition.y;
        // Orientation
        float upScore =
            + (IsUp(head, 20) ? 1 : 0)
            //+ (IsUp(body, 20) ? 1 : 0)
            ;
        // Flipped
        //float downScore =
        //    head.transform.position.y < body.transform.position.y ? 0.5f : 1f;



        return
            
                headUpTime / Evolution.S.simulationTime
                /*
                + bodyUpTime / Evolution.S.simulationTime

                + feetUpTime*0.5f / Evolution.S.simulationTime

                - feetAboveLegs / Evolution.S.simulationTime
                */
                + position * (IsUp(head, 20) ? 1 : 0.25f)

                  + upScore  
            
            
            ;
    }
}
