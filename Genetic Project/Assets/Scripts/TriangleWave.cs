using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriangleWave : Function {

	public TriangleWave(float amp, float length, float pha) :base(amp, length, pha){}

    public override float GetValue(float t)
    {
        return ((2 * amplitude / Mathf.PI) * Mathf.Asin(Mathf.Sin((2 * Mathf.PI * t - phase) / wavelength)));
    }

}
