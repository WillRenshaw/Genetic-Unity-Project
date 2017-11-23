using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SinWave : Function {

    public SinWave(float amp, float length, float pha) :base(amp, length, pha){ }

    public override float GetValue(float t)
    {
        return (amplitude * Mathf.Sin((2 * Mathf.PI * t - phase) / wavelength));
    }
}
