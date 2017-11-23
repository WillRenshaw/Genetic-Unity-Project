using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Function {

    protected float amplitude;
    protected float wavelength;
    protected float phase;

    /// <summary>
    /// Define A New Function
    /// </summary>
    /// <param name="amp">The Amplitude of the Wave</param>
    /// <param name="length">The Wavelength of the Wave</param>
    /// <param name="pha">The Phase Difference of the Wave</param>
    public Function(float amp, float length, float pha)
    {
		amplitude = amp;
		wavelength = length;
		phase = pha;
    }

    public virtual float GetValue(float t)
    {
        return 0;
    }
}
