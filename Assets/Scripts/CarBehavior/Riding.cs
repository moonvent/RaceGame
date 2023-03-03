using System;
using UnityEngine;


namespace CarBehavior
{
    public enum WheelDriveType
    {
        Fwd, 
        Rwd,
        Awd,
    }

    /// <summary>
    /// All riding logic, accelaration/brakes/turning/other
    /// </summary>
    public class Riding: Moving
    {
        private const float MaxSteerAngle = 45f;                           // degrees
        
        public Riding(Wheels wheels, CarParams carParams)
        {
            Wheels = wheels;

            firstAxelPower = carParams.firstAxelPower;
            secondAxelPower = carParams.secondAxelPower;
            mass = carParams.mass;
            Rb = carParams.Rb;
            steeringCurve = carParams.steeringCurve;
            wdWheelDriveType = carParams.wdWheelDriveType;
            
            SetupWheelDriveType();
        }

        public void Turn(float inputPower)
        {
            foreach (Wheel wheel in Wheels.FrontWheelsArray)
            {
                wheel.collider.steerAngle = inputPower * MaxSteerAngle * steeringCurve.Evaluate(Rb.velocity.magnitude);
            }
        }
    }   
}