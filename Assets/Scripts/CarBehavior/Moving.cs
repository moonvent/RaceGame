namespace CarBehavior
{
    /// <summary>
    /// Describe logic of moving, forward, backward, stop, and other
    /// </summary>
    public abstract class Moving: PowerDistribution
    {
        public delegate void _Move(float inputPower);

        public _Move Move { get; private set; }
        
        protected void SetupWheelDriveType()
        {
            switch (wdWheelDriveType)
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

        private void MoveRwd(float inputPower)
        {
            ChangeAxelPower(maxPower: CalculateMaxPower(inputPower: inputPower), Wheels.RearWheelsArray);
        }
        
        private void MoveFwd(float inputPower)
        {
            ChangeAxelPower(maxPower: CalculateMaxPower(inputPower: inputPower), Wheels.FrontWheelsArray);
        }
        
        private void MoveAwd(float inputPower)
        {
            float maxPower = CalculateMaxPower(inputPower: inputPower);
            float powerOnFrontAxel = firstAxelPower / 100 * maxPower;
            float powerOnRearAxel = maxPower - powerOnFrontAxel;
            
            ChangeAxelPower(maxPower: powerOnFrontAxel, Wheels.FrontWheelsArray);
            ChangeAxelPower(maxPower: powerOnRearAxel, Wheels.RearWheelsArray);
        }
    }
}