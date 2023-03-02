using System;
using UnityEngine;

namespace CarBehavior
{
    public class Car
    {
        
        private Wheels Wheels { get; set; }
        public Riding Riding { get; private set; }

        public Car(GameObject carObject)
        {
            // params for current only one car
            // carObject.GetComponents<CarParams>();
            Wheels = new Wheels(carObject);
            Riding = new Riding(Wheels, carObject.GetComponent<CarParams>());
        }

        public void AnimateWheels()
        {
            foreach (Wheel wheel in Wheels.WheelsArray)
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