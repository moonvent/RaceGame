using System;
using UnityEngine;

namespace CarBehavior
{
    public abstract class ChangeWheelTorque: CarParams
    {
        public int CurrentGear = 1;

        private const float UsualBrakesPower = 2500f;
        protected void ChangeMotorTorque(float inputPower, Wheel wheel)
        {
            if (inputPower != 0)
            {
                wheel.collider.motorTorque = GearsSwitch.GetMotorTorqueByCurrentData(Rb.velocity.sqrMagnitude, CurrentGear);
            }
            else
            {
                wheel.collider.motorTorque = 0f;
            }
        }

        protected void ChangeBrakeTorque(Wheel wheel, float inputPower = UsualBrakesPower) => wheel.collider.brakeTorque = inputPower;
    }
}