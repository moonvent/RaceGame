using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

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
        
        [FormerlySerializedAs("WheelDriveType")] [SerializeField] protected internal WheelDriveType wdWheelDriveType = WheelDriveType.Awd; // wheel drive type of current car
        [FormerlySerializedAs("FirstAxelPower")] [SerializeField] protected internal float firstAxelPower = 50;
        [FormerlySerializedAs("SecondAxelPower")] [SerializeField] protected internal float secondAxelPower = 50;
        [FormerlySerializedAs("Mass")] [SerializeField] protected internal int mass = 1500;
        [FormerlySerializedAs("SteeringCurve")] [SerializeField] protected internal AnimationCurve steeringCurve = new AnimationCurve(_steeringKeyframes);
        [FormerlySerializedAs("WheelSpring")] [SerializeField] protected internal int wheelSpring = 500000;
        [FormerlySerializedAs("WheelDump")] [SerializeField] protected internal int wheelDump = 9000;

        protected internal Rigidbody Rb;
    }
}
