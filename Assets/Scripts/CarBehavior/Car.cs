using UnityEngine;

namespace CarBehavior
{
    public class Car: Riding
    {

        public Car(GameObject carObject) : base()
        {
            CarParams carParams = carObject.GetComponent<CarParams>();
            SetupRigidBody(carObject, carParams);
            SetupWheels(carObject, carParams);

            InitializeRidingObject(Wheels, carParams);
        }

        private void SetupRigidBody(GameObject carObject, CarParams carParams)
        {
            Rigidbody rb = carObject.GetComponent<Rigidbody>();
            rb.mass = carParams.mass;
            carParams.Rb = rb;
            Vector3 rbCenterOfMass = rb.centerOfMass;
            rbCenterOfMass.y -= 0.1f;
            rb.centerOfMass = rbCenterOfMass;
        }
        
        private void SetupWheels(GameObject carObject, CarParams carParams)
        {
            Wheels = new Wheels(carObject, carParams.wheelSpring, carParams.wheelDump, carParams.wdWheelDriveType);
        }

        public void AnimateWheels()
        {
            foreach (Wheel wheel in Wheels.WheelsArray)
            {
                Quaternion _rot;
                Vector3 _pos;
                
                wheel.collider.GetWorldPose(out _pos, out _rot);

                wheel.model.transform.rotation = _rot;
                // wheelModelTransform.position = _pos;
            }    
        }
    }
}