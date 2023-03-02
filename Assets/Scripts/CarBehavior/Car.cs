using UnityEngine;

namespace CarBehavior
{
    
    public class Car
    {
        // conventional accelaration units
        private const float MaxAccelarationPoint = 500000f;
        private const float TurnSensitivity = 1f;
        private const float MaxSteerAngle = 45f; // degrees
        
        private Wheels _wheels;
        
        public Car(GameObject carObject)
        {
            _wheels = new Wheels(carObject);
            // _accelaration = new Accelaration(_wheels);
        }
        
        public void Move(float inputPower)
        {
            foreach (Wheel wheel in _wheels.WheelsArray)
            {
                if (wheel.axel == Axel.Rear) // rwd
                {
                    wheel.collider.motorTorque = inputPower * MaxAccelarationPoint * Time.deltaTime;
                }
            }
        }
        
        public void Turn(float inputPower)
        {
            foreach (Wheel wheel in _wheels.WheelsArray)
            {
                if (wheel.axel == Axel.Front)
                {
                    float steerAngle = inputPower * TurnSensitivity * MaxSteerAngle;
                    // wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 1f);
                    wheel.collider.steerAngle = steerAngle;
                }
            }
        }
        
        public void AnimateWheels()
        {
            foreach (Wheel wheel in _wheels.WheelsArray)
            {
                Quaternion _rot;
                Vector3 _pos;
                
                wheel.collider.GetWorldPose(out _pos, out _rot);

                Transform wheelModelTransform = wheel.model.transform;
                
                wheelModelTransform.position = _pos;
                wheelModelTransform.rotation = _rot;
            }    
        }
    }
}