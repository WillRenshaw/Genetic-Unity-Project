using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Function {

    protected float amplitude; //Amplitude of function
    protected float wavelength; //Wavelength of function
    protected float phase; //Phase of function

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

	/// <summary>
	/// Fetch the value of the function at time t
	/// </summary>
	/// <returns>Value of the function at time t</returns>
	/// <param name="t">Time</param>
    public virtual float GetValue(float t)
    {
        return 0;
    }

	//Getters for amplitude, wavelength and phase
	public float GetAmplitude(){
		return amplitude;
	}
	public float GetWavelength(){
		return wavelength;
	}
	public float GetPhase(){
		return phase;
	}
}
