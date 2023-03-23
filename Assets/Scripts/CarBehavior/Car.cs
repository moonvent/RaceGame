using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CarBehavior
{
    public class Car
    {
        
        private Wheels Wheels { get; set; }
        public Riding Riding { get; private set; }
        public Rigidbody _rb;
        private CarParams _carParams;

        public Car(GameObject carObject)
        {
            _rb = carObject.GetComponent<Rigidbody>();
            _rb.mass = carObject.GetComponent<CarParams>().mass;

            _carParams = carObject.GetComponent<CarParams>();
            _carParams.Rb = _rb;
            var rbCenterOfMass = _rb.centerOfMass;
            rbCenterOfMass.y -= 0.1f;
            _rb.centerOfMass = rbCenterOfMass;
            
            Wheels = new Wheels(carObject, _carParams.wheelSpring, _carParams.wheelDump);
            Riding = new Riding(Wheels, _carParams);
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