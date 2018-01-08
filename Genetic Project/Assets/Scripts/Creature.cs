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


    private Function RHF = new SinWave(30, 1, 0);
    private Function LHF = new SinWave(30, 1, 0);
    private Function RKF = new SinWave(10, 1, 0);
    private Function LKF = new SinWave(10, 1, 0);

    private float bodyLength = 5f;
    private float RUpperLegLength = 2f;
    private float LUpperLegLength = 2f;
    private float RLowerLegLength = 1f;
    private float LLowerLegLength = 1f;

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
    public float GetRLowerLegLength()
    {
        return RLowerLegLength;
    }
    public void SetRLowerLegLength(float l)
    {
        RLowerLegLength = l;
    }

    public float GetLLowerLegLength()
    {
        return LLowerLegLength;
    }
    public void SetLLowerLegLength(float l)
    {
        LLowerLegLength = l;
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

    public Function GetRKF()
    {
        return (RKF);
    }
    public void SetRKF(Function f)
    {
        RKF = f;
    }

    public Function GetLKF()
    {
        return (LKF);
    }
    public void SetLKF(Function f)
    {
        LKF = f;
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

