// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using System.Collections;
using System;

public class ControllerHinge : Controller {
    public HingeJoint2D joint;

    public float motorSpeed = 0;

    public override void SetValue(float value)
    {
        motorSpeed = value;
    }

    private float min;
    private float max;

    public void Start ()
    {
        JointAngleLimits2D limits = joint.limits;
        limits.min = joint.limits.max;
        limits.max = joint.limits.min;
        joint.limits = limits;
    }
    

    void FixedUpdate ()
    {
        JointMotor2D motor = joint.motor;
        motor.motorSpeed = motorSpeed * 1;
        joint.motor = motor;
        
    }
}
