using System;
using UnityEngine;

namespace CarBehavior
{
    public abstract class PowerDistribution: CarParams
    {
        protected Wheels Wheels { get; set; }
        
        // conventional accelaration units
        private const float MaxAccelarationPoint = 300f;
        private const float BrakeCoefMultiplier = 8f;
        private const float HandBrakePower = 100000f;

        private const float SpeedLevelForChangeDirection = 0.1f;

        private const float NoPower = 0;

        private bool _carDirectionForward = true;
        
        protected float CalculateMaxPower(float inputPower) => inputPower * MaxAccelarationPoint;
        
        private void ChangeMotorTorqueOnWheel(float inputPower, Wheel wheel) => wheel.collider.motorTorque = inputPower;
        private void ChangeBrakeTorqueOnWheel(double inputPower, Wheel wheel) => wheel.collider.brakeTorque = (float)inputPower;

        protected void ChangeAxelPower(float maxPower, Wheel[] accelarationWheels)
        {
            if (maxPower > NoPower)
            {
                // if user press accelaration button
                if (!_carDirectionForward)
                    // if move backward and just use a brake
                    _carDirectionForward = ChangeMoveDirection(maxPower, accelarationWheels);
                else
                    AccelarateAfterBrakes(maxPower, accelarationWheels);
            }
            else if (maxPower < NoPower)
            {
                // if user press brake / back button
                if (_carDirectionForward)
                    // if move forward and just use a brake
                    _carDirectionForward = !ChangeMoveDirection(maxPower, accelarationWheels);
                else
                    AccelarateAfterBrakes(maxPower, accelarationWheels);
            }
            else
            {
                // if user not press the button
                ClearPowerFromAxel(accelarationWheels);
            }
        }

        private void AccelarateAfterBrakes(float maxPower, Wheel[] accelarationWheels)
        {
            Brake(NoPower, Wheels.WheelsArray);
            Accelarate(maxPower, accelarationWheels);
        }

        /// <summary>
        /// Make brakes when change the direction, when speed is slow, return True which signalize about
        /// available of ready to change direction
        /// </summary>
        /// <param name="maxPower"></param>
        /// <param name="accelarationWheels"></param>
        /// <returns></returns>
        private bool ChangeMoveDirection(float maxPower, Wheel[] accelarationWheels)
        {
            if (Rb.velocity.magnitude > SpeedLevelForChangeDirection)
            {
                Brake(maxPower, Wheels.WheelsArray);
                Accelarate(NoPower, accelarationWheels);
                return false;
            }
            else
            {
                Brake(NoPower, Wheels.WheelsArray);
                return true;
            }
        }
        
        private void Accelarate(float maxPower, Wheel[] accelarationWheels)
        {
            foreach (Wheel wheel in accelarationWheels)
                ChangeMotorTorqueOnWheel(maxPower, wheel);
        }
        
        /// <summary>
        /// Brakes system, work on all wheels
        /// </summary>
        /// <param name="maxPower"></param>
        /// <param name="accelarationWheels"></param>
        private void Brake(double maxPower, Wheel[] accelarationWheels)
        {
            maxPower = maxPower > NoPower ? maxPower : -maxPower;
            foreach (Wheel wheel in accelarationWheels)
                ChangeBrakeTorqueOnWheel(maxPower * BrakeCoefMultiplier, wheel);
        }

        protected void HandBrake()
        {
            Accelarate(NoPower, Wheels.WheelsArray);
            foreach (Wheel wheel in Wheels.RearWheelsArray)
                ChangeBrakeTorqueOnWheel(HandBrakePower, wheel);
        }

        private void ClearPowerFromAxel(Wheel[] accelarationWheels)
        {
            Accelarate(NoPower, accelarationWheels);
            Brake(NoPower, Wheels.WheelsArray);
        }
    }
}