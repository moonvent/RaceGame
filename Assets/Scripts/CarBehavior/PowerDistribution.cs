using System;
using UnityEngine;

namespace CarBehavior
{
    public abstract class PowerDistribution: CarParams
    {
        protected Wheels Wheels { get; set; }
        
        // conventional accelaration units
        private const float MaxAccelarationPoint = 300f;
        
        private const float BrakePowerMultiplier = 10;
        private const float BrakePowerCoef = 10;
        private readonly double _brakeCoef = Math.Pow(BrakePowerCoef, BrakePowerMultiplier);

        private const float SpeedLevelForChangeDirection = 0.1f;

        private bool _carDirectionForward = true;
        
        protected float CalculateMaxPower(float inputPower) => inputPower * MaxAccelarationPoint;
        
        private void ChangeMotorTorqueOnWheel(float inputPower, Wheel wheel) => wheel.collider.motorTorque = inputPower;
        private void ChangeBrakeTorqueOnWheel(double inputPower, Wheel wheel) => wheel.collider.brakeTorque = (float)inputPower;

        protected void ChangeAxelPower(float maxPower, Wheel[] accelarationWheels)
        {
            if (maxPower > 0)
            {
                // if user press accelaration button
                if (!_carDirectionForward)
                {
                    // if move backward and just use a brake
                    _carDirectionForward = ChangeMoveDirection(maxPower, accelarationWheels);
                }
                else
                {
                    Brake(0, Wheels.WheelsArray);
                    Accelarate(maxPower, accelarationWheels);
                }
            }
            else if (maxPower < 0)
            {
                // if user press brake / back button
                if (_carDirectionForward)
                    // if move forward and just use a brake
                    _carDirectionForward = !ChangeMoveDirection(maxPower, accelarationWheels);
                else
                {
                    Brake(0, Wheels.WheelsArray);
                    Accelarate(maxPower, accelarationWheels);
                }
            }
            else
            {
                // if user not press the button
                ClearPowerFromAxel(accelarationWheels);
            }
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
                Accelarate(0, accelarationWheels);
                return false;
            }
            else
            {
                Brake(0, Wheels.WheelsArray);
                return true;
            }
        }
        
        private void Accelarate(float maxPower, Wheel[] accelarationWheels)
        {
            foreach (Wheel wheel in accelarationWheels)
                ChangeMotorTorqueOnWheel(maxPower, wheel);
        }
        
        private void Brake(double maxPower, Wheel[] accelarationWheels)
        {
            maxPower = maxPower > 0 ? maxPower : -maxPower;
            foreach (Wheel wheel in accelarationWheels)
                ChangeBrakeTorqueOnWheel(maxPower * _brakeCoef, wheel);
        }

        private void ClearPowerFromAxel(Wheel[] accelarationWheels)
        {
            Accelarate(0, accelarationWheels);
            Brake(0, Wheels.WheelsArray);
        }
    }
}