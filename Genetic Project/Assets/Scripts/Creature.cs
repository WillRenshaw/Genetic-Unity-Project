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


    private Function RHF = new SinWave(1f, 1f, 0);
    private Function LHF = new SinWave(1f, 1f, 0);


    private float bodyLength = 5f;
    private float RUpperLegLength = 2f;
    private float LUpperLegLength = 2f;

    private float fitness = 0f;

    public Creature(string n, int gen, int IDnum)
    {
        name = n;
        generation = gen;
        id = IDnum;
    }

    public float GetBodyLength()
    {
        return bodyLength;
    }
    public void SetBodyLength(float l)
    {
        bodyLength = l;
    }

    public float GetRUpperLegLength()
    {
        return RUpperLegLength;
    }
    public void SetRUpperLegLength(float l)
    {
        RUpperLegLength = l;
    }

    public float GetLUpperLegLength()
    {
        return LUpperLegLength;
    }
    public void SetLUpperLegLength(float l)
    {
        LUpperLegLength = l;
    }
   

    public Function GetRHF()
    {
        return (RHF);
    }
    public void SetRHF(Function f)
    {
        RHF = f;
    }

    public Function GetLHF()
    {
        return (LHF);
    }
    public void SetLHF(Function f)
    {
        LHF = f;
    }

   

    public float GetFitness()
    {
        return fitness;
    }

    public void SetFitness(float f)
    {
        fitness = f;
    }
}

