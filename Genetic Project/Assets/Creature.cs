using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature{
    private string name;
    public string NAME
    {
        get { return name; }
    }

    private int id;
    public int ID
    {
        get { return id; }
    }

    private int generation;
    public int GENERATION
    {
        get { return generation; }
    }


    public Function RHF = new Function();
    public Function LHF = new Function();
    public Function RKF = new Function();
    public Function LKF = new Function();

    public float bodyLength = 5f;
    public float RUpperLegLength = 2f;
    public float LUpperLegLength = 2f;
    public float RLowerLegLength = 1f;
    public float LLowerLegLength = 1f;

    public float fitness = 0f;

    public Creature(string n, int gen, int IDnum)
    {
        name = n;
        generation = gen;
        id = IDnum;
    }

}
