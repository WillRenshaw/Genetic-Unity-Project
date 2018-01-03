using UnityEngine;
using System.Collections;

public class CreatureRagdoll : Creature
{

    public void Start()
    {
        express = Gene.Evaluate4At;
        position_prev = head.transform.position;
        initialX = head.transform.position.x;
    }

    public GameObject head;
    public GameObject body;

    public float headUpTime = 0;
    public float bodyUpTime = 0;


    private float initialX;

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Body and head UP!
        if (IsUp(head, 20))
            headUpTime += Time.fixedDeltaTime;
        if (IsUp(body, 20))
            bodyUpTime += Time.fixedDeltaTime;

        // Distance is measured only when head up
        if (IsUp(head, 20))
            //distance += Mathf.Max(head.transform.position.x - position_prev.x, 0);
            distance += head.transform.position.x - position_prev.x;
        position_prev = head.transform.position;
    }

    public float distance = 0;
    private Vector3 position_prev;
    public override float GetScore ()
    {
        //return head.transform.position.x;
        float position = head.transform.position.x;
        //float position = head.transform.localPosition.y + body.transform.localPosition.y;
        // Orientation
        float upScore =
            + (IsUp(head, 20) ? 1 : 0)
            + (IsUp(body, 20) ? 1 : 0)
            ;

        return
            
            headUpTime
            + bodyUpTime

             + distance * 2

            // Average speed
             + (head.transform.position.x - initialX) / Evolution.S.simulationTime
            ;
    }
}
