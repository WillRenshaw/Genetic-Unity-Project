using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function {
    enum WaveType
    {
        Sin,
        Triangle
    }

    WaveType waveType = new WaveType();
    float amplitude;
    float wavelength;
    float phase;

    public Function(int type, float amp, float length, float pha)
    {
        waveType = (WaveType)type;
        
    }

    public float GetValue(float t)
    {
        if(waveType == WaveType.Sin)
        {
            return (amplitude * Mathf.Sin((2 * Mathf.PI * t - phase)/amplitude));
        }else
        {
            return ((2 * amplitude / Mathf.PI) * Mathf.Asin(Mathf.Sin((2 * Mathf.PI * t - phase) / amplitude)));
        }
    }
}
