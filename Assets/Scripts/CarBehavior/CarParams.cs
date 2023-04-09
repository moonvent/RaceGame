using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace CarBehavior
{
    public class CarParams: MonoBehaviour
    {
        static Keyframe[] _steeringKeyframes = new Keyframe[]
        {
            new Keyframe(0f, 1f),
            new Keyframe(10.66025f, 0.4599973f),
            new Keyframe(36.94558f, 0.1586899f),
        };
        
        [FormerlySerializedAs("WheelDriveType"), SerializeField]
        protected internal WheelDriveType wdWheelDriveType = WheelDriveType.Awd; // wheel drive type of current car
        [FormerlySerializedAs("FirstAxelPower"), SerializeField]
        // protected internal float firstAxelPower = 50;
        // [FormerlySerializedAs("SecondAxelPower"), SerializeField]
        // protected internal float secondAxelPower = 50;
        // [FormerlySerializedAs("Mass"), SerializeField, Tooltip("Car mass in kilogramms")]
        protected internal int mass = 1500;
        [FormerlySerializedAs("SteeringCurve"), SerializeField, Tooltip("Curve of dependence speed and availability to turn wheels")]
        protected internal AnimationCurve steeringCurve = new AnimationCurve(_steeringKeyframes);
        [FormerlySerializedAs("WheelSpring"), SerializeField, Tooltip("Suspension power to ground:\n" +
                                                                      "more: more stiffer suspension, less bouncing\n" +
                                                                      "less: more softer suspension")]
        protected internal int wheelSpring = 500000;
        [FormerlySerializedAs("WheelDump"), SerializeField, Tooltip("Wheel resistance to ground:\n" +
                                                                    "more: more control, and more resistance to ground, and more controlled suspension\n" +
                                                                    "less: less controll and more bouncy")]
        protected internal int wheelDump = 9000;
        [FormerlySerializedAs("MaxSpeed"), SerializeField, Tooltip("Max speed in kilometrs")] 
        protected internal float maxSpeed = 300f;
        
        // [FormerlySerializedAs("Power"), SerializeField, Tooltip("Max engine power in kiloWatts")] 
        // protected internal float power = 400;
        //
        [FormerlySerializedAs("Engine Power (Kilowatts)"), SerializeField, Tooltip("Max engine torque")] 
        protected internal float enginePower = 400f;
        
        [FormerlySerializedAs("Engine Rpm"), SerializeField, Tooltip("Max engine torque")] 
        protected internal float engineRpm = 6000;

        [FormerlySerializedAs("GearsPower"), SerializeField, Tooltip("Amount gears and gear power (in percent), maximum in sum of elements 100")]
        protected internal List<float> gearsPower = new List<float>();
        
        protected internal GearSwitch GearsSwitch;
        protected internal Rigidbody Rb;
        protected internal CarUI CarUI;

    }
}
