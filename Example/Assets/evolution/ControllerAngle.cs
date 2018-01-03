// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;
using System;

public class ControllerAngle : Controller {
    [Range(-1,+1)]
    public float value = 0;

    public float min = 0;
    public float max = 0;

    public override void SetValue(float value)
    {
        this.value = value;
    }


    void FixedUpdate ()
    {
        Vector3 angles = transform.localEulerAngles;
        angles.z = Controller.linearInterpolation
            (
                -1, +1,
                min, max,
                value
            );
        transform.localEulerAngles = angles;
    }
}