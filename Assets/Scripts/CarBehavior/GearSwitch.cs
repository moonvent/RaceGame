namespace CarBehavior
{
    public class GearSwitch
    {
        private float[] _minSpeeds;
        private float[] _maxSpeeds;

        private float _maxSpeed;
        private float _amountGears;

        private float _oneSpeedPeriod;

        private float _motorTorque;

        private float _speedByOneUnit;      // скорость одного км в час в процентах

        private const int ConvertToPercent = 100;

        public GearSwitch(float maxSpeed, int amountGears, float motorTorque)
        {
            _maxSpeed = maxSpeed;
            _amountGears = amountGears;
            _oneSpeedPeriod = maxSpeed / amountGears;
            _motorTorque = motorTorque;
            _speedByOneUnit = _oneSpeedPeriod / ConvertToPercent;

            _minSpeeds = new float[amountGears];
            _maxSpeeds = new float[amountGears];
            
            CalculateGearsSpeeds();
        }

        private void CalculateGearsSpeeds()
        {
            for (int i = 0; i < _amountGears; i++)
            {
                SetNewGearSpeed(minSpeed: _oneSpeedPeriod * i, 
                    maxSpeed: _oneSpeedPeriod * i + _oneSpeedPeriod,
                    gear: i);
            }
        }

        private void SetNewGearSpeed(float maxSpeed, float minSpeed, int gear)
        {
            _minSpeeds[gear] = minSpeed;
            _maxSpeeds[gear] = maxSpeed;
        }
        
        private float GetMinSpeedByGear(int gear)
        {
            return _minSpeeds[gear];
        }

        private float GetMotorTorqueByCurrentSpeed(float currentGearSpeed)
        {
            float motorPowerInPercent = currentGearSpeed * _speedByOneUnit;
            return _motorTorque 
                   / ConvertToPercent
                   * motorPowerInPercent;
        }

        public float GetMotorTorqueByCurrentData(float currentSpeed, int currentGear)
        {
            float currentGearSpeed;
            if (currentGear > -1)
                currentGearSpeed = _oneSpeedPeriod - (currentSpeed - GetMinSpeedByGear(currentGear));
            else 
                currentGearSpeed = -_oneSpeedPeriod - (currentSpeed - GetMinSpeedByGear(0));
            return GetMotorTorqueByCurrentSpeed(currentGearSpeed);
        }
    }
}