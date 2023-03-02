using UnityEngine;


namespace CarBehavior
{
    public enum WheelDriveType
    {
        Fwd, 
        Rwd,
        Awd,
    }

    public class Riding: CarParams
    {
        // conventional accelaration units
        private const float MaxAccelarationPoint = 500000f;
        private const float TurnSensitivity = 1f;
        private const float MaxSteerAngle = 45f;                           // degrees
        
        private Wheels Wheels { get; set; }
        public delegate void _Move(float inputPower);

        public _Move Move { get; private set; }
        
        public Riding(Wheels wheels, CarParams carParams)
        {
            Wheels = wheels;

            FirstAxelPower = carParams.FirstAxelPower;
            SecondAxelPower = carParams.SecondAxelPower;

            switch (carParams.WdWheelDriveType)
            {
                case WheelDriveType.Rwd:
                    Move = MoveRwd;
                    break;
                case WheelDriveType.Fwd:
                    Move = MoveFwd;
                    break;
                case WheelDriveType.Awd:
                    Move = MoveAwd;
                    break;
            }
        }
        
        public void MoveRwd(float inputPower)
        {
            float newPower = inputPower * MaxAccelarationPoint * Time.deltaTime;
            Wheels.RearWheelsArray[1].collider.motorTorque = Wheels.RearWheelsArray[0].collider.motorTorque = newPower;
            Debug.Log("rear");
        }
        
        public void MoveFwd(float inputPower)
        {
            float newPower = inputPower * MaxAccelarationPoint * Time.deltaTime;
            Wheels.FrontWheelsArray[1].collider.motorTorque = Wheels.FrontWheelsArray[0].collider.motorTorque = newPower;
        }
        
        public void MoveAwd(float inputPower)
        {
            float powerOnFrontAxel = FirstAxelPower / 100 * inputPower * MaxAccelarationPoint * Time.deltaTime;
            float powerOnRearAxel = inputPower * MaxAccelarationPoint * Time.deltaTime - powerOnFrontAxel;
            Wheels.FrontWheelsArray[1].collider.motorTorque = Wheels.FrontWheelsArray[0].collider.motorTorque = powerOnFrontAxel;
            Wheels.RearWheelsArray[1].collider.motorTorque = Wheels.RearWheelsArray[0].collider.motorTorque = powerOnRearAxel;
        }
        
        public void Turn(float inputPower)
        {
            foreach (Wheel wheel in Wheels.WheelsArray)
            {
                if (wheel.axel == Axel.Front)
                {
                    float steerAngle = inputPower * TurnSensitivity * MaxSteerAngle;
                    // wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 1f);
                    wheel.collider.steerAngle = steerAngle;
                }
            }
        }
    }   
}