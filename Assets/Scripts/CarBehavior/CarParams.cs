using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CarBehavior
{
    public class CarParams: MonoBehaviour
    {
        [FormerlySerializedAs("WheelDriveType")] [SerializeField] protected internal WheelDriveType wdWheelDriveType = WheelDriveType.Awd; // wheel drive type of current car
        [FormerlySerializedAs("FirstAxelPower")] [SerializeField] protected internal float firstAxelPower = 50;
        [FormerlySerializedAs("SecondAxelPower")] [SerializeField] protected internal float secondAxelPower = 50;
        [FormerlySerializedAs("Mass")] [SerializeField] protected internal int mass = 1500;
        [FormerlySerializedAs("SteeringCurve")] [SerializeField] protected internal AnimationCurve steeringCurve;
        [FormerlySerializedAs("WheelSpring")] [SerializeField] protected internal int wheelSpring = 30000;
        [FormerlySerializedAs("WheelDump")] [SerializeField] protected internal int wheelDump = 2000;

        protected internal Rigidbody Rb;
    }
}
