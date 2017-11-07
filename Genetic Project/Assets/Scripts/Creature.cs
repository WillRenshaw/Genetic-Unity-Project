using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
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


    public Function RHF = new Function(0, 50, 1, 0);
    public Function LHF = new Function(0, 20, 3, 0);
    public Function RKF = new Function(0, 30, 0.000000000000001f, 0);
    public Function LKF = new Function(0, 100, 5, 0);

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
