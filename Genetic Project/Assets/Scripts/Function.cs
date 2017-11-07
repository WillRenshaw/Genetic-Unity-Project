using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
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
		amplitude = amp;
		wavelength = length;
		phase = pha;
    }

    public float GetValue(float t)
    {	
		
        if(waveType == WaveType.Sin)
        {
			
            return (amplitude * Mathf.Sin((2 * Mathf.PI * t - phase)/wavelength));
        }else
        {
            return ((2 * amplitude / Mathf.PI) * Mathf.Asin(Mathf.Sin((2 * Mathf.PI * t - phase) / wavelength)));
        }

    }
}
