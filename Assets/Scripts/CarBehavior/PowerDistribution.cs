using System;

namespace CarBehavior
{
    public abstract class PowerDistribution: CarParams
    {
        // conventional accelaration units
        private const float MaxAccelarationPoint = 500000f;
        private const float MaxBrakePowerMultiplier = 10;
        private const float MaxBrakePowerCoef = 10;
        
        protected float CalculateMaxPower(float inputPower) => inputPower * MaxAccelarationPoint;
        
        private void ChangeMotorTorqueOnWheel(float inputPower, Wheel wheel) => wheel.collider.motorTorque = inputPower;
        private void ChangeBrakeTorqueOnWheel(double inputPower, Wheel wheel) => wheel.collider.brakeTorque = (float)inputPower;

        protected void ChangeAxelPower(float maxPower, Wheel[] accelarationWheels)
        {
            if (maxPower > 0)
            {
                Accelarate(maxPower, accelarationWheels);
            }
            else if (maxPower < 0)
            {
                Brake(maxPower, accelarationWheels);
            }
            else
            {
                // if user not press the button
                ClearPowerFromAxel(accelarationWheels);
            }
        }
        
        private void Accelarate(float maxPower, Wheel[] accelarationWheels)
        {
            foreach (Wheel wheel in accelarationWheels)
                ChangeMotorTorqueOnWheel(maxPower, wheel);
        }
        
        private void Brake(float maxPower, Wheel[] accelarationWheels)
        {
            foreach (Wheel wheel in accelarationWheels)
                ChangeBrakeTorqueOnWheel(-maxPower * Math.Pow(MaxBrakePowerCoef, MaxBrakePowerMultiplier), wheel);
        }

        private void ClearPowerFromAxel(Wheel[] accelarationWheels)
        {
            Accelarate(0, accelarationWheels);
            Brake(0, accelarationWheels);
        }
    }
}