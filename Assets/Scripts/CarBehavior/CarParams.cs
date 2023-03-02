using UnityEngine;

namespace CarBehavior
{
    public class CarParams: MonoBehaviour
    {
        [SerializeField] protected internal WheelDriveType WdWheelDriveType; // wheel drive type of current car
        [SerializeField] protected internal float FirstAxelPower = 0;
        [SerializeField] protected internal float SecondAxelPower = 0;
    }
}