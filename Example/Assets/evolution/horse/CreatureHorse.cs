// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;

public class CreatureHorse : Creature
{

    public GameObject head;
    public GameObject body;

    public void Start()
    {
        express = Gene.Evaluate4At;
        initialX = head.transform.position.x;
    }

    private float initialX;
    
    public override float GetScore ()
    {
        return
            + (head.transform.position.x - initialX) / Evolution.S.simulationTime
        ;
    }
}
