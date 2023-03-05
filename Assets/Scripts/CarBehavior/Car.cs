using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CarBehavior
{
    public class Car
    {
        
        private Wheels Wheels { get; set; }
        public Riding Riding { get; private set; }
        private Rigidbody _rb;
        private CarParams _carParams;

        public Car(GameObject carObject)
        {
            _rb = carObject.GetComponent<Rigidbody>();
            _rb.mass = carObject.GetComponent<CarParams>().mass;
            
            _carParams = carObject.GetComponent<CarParams>();
            _carParams.Rb = _rb;
            
            Wheels = new Wheels(carObject);
            Riding = new Riding(Wheels, _carParams);
        }

        public void AnimateWheels()
        {
            foreach (Wheel wheel in Wheels.WheelsArray)
            {
                Quaternion _rot;
                Vector3 _pos;
                
                wheel.collider.GetWorldPose(out _pos, out _rot);

                Transform wheelModelTransform = wheel.model.transform;

                wheelModelTransform.rotation = _rot;
                
                // Debug.Log(_carParams.Rb.velocity.magnitude);
                // wheelModelTransform.position = _pos;
            }    
        }
    }
}