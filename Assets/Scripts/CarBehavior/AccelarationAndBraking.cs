using UnityEngine;

namespace CarBehavior
{
    public abstract class AccelarationAndBraking: ChangeWheelTorque
    {
        protected Wheels Wheels { get; set; }
        
        protected bool IsHandBrake;
        private const float HandBrakePower = 100000f;
        
        private const float ActiveHandBrakeStiffnessOnFront = 1.5f;
        private const float InactiveHandBrakeStiffnessOnFront = 1f;
        private const float ActiveHandBrakeStiffnessOnRear = 0.75f;
        private const float InactiveHandBrakeStiffnessOnRear = 1f;
        
        protected const float NoPower = 0f;

        private bool _brakesActive = false;
        
        protected void Accelarate(float maxPower)
        {
            if (_brakesActive)
                StopBrake();
            
            foreach (Wheel wheel in Wheels.AccelatationWheels)
                ChangeMotorTorque(maxPower, wheel);
        }
        
        protected void Brake()
        {
            if (!_brakesActive) StopAccelarate();

            foreach (Wheel wheel in Wheels.AllWheelsArray)
                ChangeBrakeTorque(wheel: wheel);
        }
        
        protected void StopBrake()
        {
            _brakesActive = false;
            
            foreach (Wheel wheel in Wheels.AllWheelsArray)
                ChangeBrakeTorque(wheel: wheel, inputPower: NoPower);
        }
        
        protected void StopAccelarate()
        {
            Accelarate(NoPower);
            _brakesActive = true;
        }
        
        protected void HandBrake()
        {
            _brakesActive = true;
            if (!IsHandBrake) SetupNewSuspensionForHandBrake();
            foreach (Wheel wheel in Wheels.RearWheelsArray)
                ChangeBrakeTorque(wheel, HandBrakePower);
        }

        protected void SetupNewSuspensionForHandBrake(bool resetStiffnes = false)
        {
            float newStiffnessOnFront = resetStiffnes ? InactiveHandBrakeStiffnessOnFront : ActiveHandBrakeStiffnessOnFront;
            float newStiffnessOnRear = resetStiffnes ? InactiveHandBrakeStiffnessOnRear : ActiveHandBrakeStiffnessOnRear;

            foreach (Wheel wheel in Wheels.FrontWheelsArray)
            {
                WheelFrictionCurve wfc = wheel.collider.sidewaysFriction;
                wfc.stiffness = newStiffnessOnFront;
                wheel.collider.sidewaysFriction = wfc;
            } 
            
            foreach (Wheel wheel in Wheels.RearWheelsArray)
            {
                WheelFrictionCurve wfc = wheel.collider.forwardFriction;
                wfc.stiffness = newStiffnessOnRear;
                wheel.collider.forwardFriction = wfc;
            }

            IsHandBrake = !resetStiffnes;
        }
        
    }
}