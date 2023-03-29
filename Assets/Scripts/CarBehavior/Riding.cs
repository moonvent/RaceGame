using System;
using System.Linq;
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
    public class Riding: PowerDistribution
    {
        private const float MaxSteerAngle = 45f;                           // degrees
        
        public Riding(Wheels wheels, CarParams carParams)
        {
            InitializeRidingObject(wheels, carParams);
        }
        
        /// <summary>
        /// need for inheritance
        /// </summary>
        protected Riding()
        {
        }

        protected void InitializeRidingObject(Wheels wheels, CarParams carParams)
        {
            Wheels = wheels;

            // firstAxelPower = carParams.firstAxelPower;
            // secondAxelPower = carParams.secondAxelPower;
            mass = carParams.mass;
            Rb = carParams.Rb;
            steeringCurve = carParams.steeringCurve;
            wdWheelDriveType = carParams.wdWheelDriveType;
            gearsPower = carParams.gearsPower;
            maxSpeed = carParams.maxSpeed;
            enginePower = carParams.enginePower;
            engineRpm = carParams.engineRpm;
            CalculateMaxWheelTorqueForGear();

            // SetupWheelDriveType();
        }

        private void CalculateMaxWheelTorqueForGear()
        {
            // gearsPower.
            float oneSpeedPeriod = maxSpeed / gearsPower.Count;
            
            GearsSpeeds = new GearSpeeds(maxSpeed, gearsPower.Count, enginePower);
            
            for (int i = 0; i < gearsPower.Count; i++)
            {
                GearsSpeeds.SetNewGearSpeed(minSpeed: oneSpeedPeriod * i, 
                    maxSpeed: oneSpeedPeriod * i + oneSpeedPeriod,
                    gear: i);
            }
        }

        public void Turn(float inputPower)
        {
            // Debug.Log($"{Rb.velocity.magnitude} // {steeringCurve.Evaluate(Rb.velocity.magnitude)}");
            float turnAngle = inputPower * MaxSteerAngle * steeringCurve.Evaluate(Rb.velocity.magnitude);
            foreach (Wheel wheel in Wheels.FrontWheelsArray)
            {
                // wheel.collider.steerAngle = turnAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, turnAngle, 0.5f);
            }
        }
        
        public void HandBrake(bool activeHandBrake)
        {
            if (activeHandBrake)
                HandBrake();
            else if (IsHandBrake)
                SetupNewSuspensionForHandBrake(true);
        }
        
        public void GearUp()
        {
            if (CurrentGear < gearsPower.Count - 1)
            {
                CurrentGear += 1;
                Debug.Log($"Gear up, current gear {CurrentGear}");
            }
        }
        public void GearDown()
        {
            if (CurrentGear > -1)
            {
                CurrentGear -= 1;
                Debug.Log($"Gear down, current gear {CurrentGear}");
            }
        }
    }   
}