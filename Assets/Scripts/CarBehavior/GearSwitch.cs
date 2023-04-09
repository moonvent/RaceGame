using System;
using System.Linq;
using UnityEngine;

namespace CarBehavior
{
    public class GearSwitch
    {
        private float[] _minSpeeds;
        private float[] _maxSpeeds;

        private float _maxSpeed;
        private float _amountGears;

        public float OneSpeedPeriod;      // один скоростной период

        private float _motorTorque;

        private float _speedByOneUnit;                // скорость одного км в час в процентах
        
        private float _currentGearSpeed;              // текущая скорость для передачи
        private float _minGearSpeedForCurrentGear;    // минимальная скорость для текущей передачи

        private const int ConvertToPercent = 100;
        private const int ConvertToPercentForSpeedometerBar = 1000;
        private const int ConvertToPercentForSpeedometerBarWithBraking = 100;

        public float CurrentPowerToWheels = 0;

        public GearSwitch(float maxSpeed, int amountGears, float motorTorque)
        {
            _maxSpeed = maxSpeed;
            _amountGears = amountGears;
            OneSpeedPeriod = maxSpeed / amountGears;
            _motorTorque = motorTorque;
            _speedByOneUnit = OneSpeedPeriod / ConvertToPercent;

            _minSpeeds = new float[amountGears];
            _maxSpeeds = new float[amountGears];
            
            CalculateGearsSpeeds();
        }

        private void CalculateGearsSpeeds()
        {
            for (int i = 0; i < _amountGears; i++)
            {
                SetNewGearSpeed(minSpeed: OneSpeedPeriod * i, 
                    maxSpeed: OneSpeedPeriod * i + OneSpeedPeriod,
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
            return _minSpeeds[gear - 1];
        }
        
        private int GetGearByMinSpeed(float speed)
        {
            for (int i = _minSpeeds.Length - 1; i >= 0; i--)
            {
                if (speed > _minSpeeds[i])
                    return i + 1;
            }

            return 0;
        }

        /// <summary>
        /// Рассчитываем мощь которую подавать на колёса
        /// </summary>
        /// <param name="currentGearSpeed">скорость на которую нужно ещё разогнаться, чем меньше тем меньше подаем мощности на колеса</param>
        /// <returns></returns>
        private float GetMotorTorqueByCurrentSpeed(float currentGearSpeed)
        {
            float motorPowerInPercent = currentGearSpeed * _speedByOneUnit;
            return _motorTorque 
                   / ConvertToPercent
                   * motorPowerInPercent;
        }

        private float CalculateCurrentGearSpeed(float currentSpeed, int currentGear)
        {
            _minGearSpeedForCurrentGear = GetMinSpeedByGear(currentGear);
            
            if (currentSpeed > _minGearSpeedForCurrentGear)
                _currentGearSpeed = OneSpeedPeriod - (currentSpeed - GetMinSpeedByGear(currentGear));
            else
            {
                _currentGearSpeed = (float)((GetMinSpeedByGear(currentGear) + currentSpeed) /
                                            Math.Pow(GetGearByMinSpeed(currentSpeed) - currentGear, 2));
            }

            return _currentGearSpeed;
        }

        public void ResetPowerAfterAccelerate(bool braking = false)
        {
            int percentConvertValue = braking ? ConvertToPercentForSpeedometerBarWithBraking : ConvertToPercentForSpeedometerBar;
            CurrentPowerToWheels = CurrentPowerToWheels > 0 ? CurrentPowerToWheels - OneSpeedPeriod / percentConvertValue : 0;
        }
        
        public float GetMotorTorqueByCurrentData(float currentSpeed, int currentGear)
        {
            if (currentGear > 0)
                // для всех кроме задней и нейтралки
                _currentGearSpeed = CalculateCurrentGearSpeed(currentSpeed, currentGear);
            else if (currentGear == -1)
                // для задней передачи 
                // _currentGearSpeed = -OneSpeedPeriod - (currentSpeed - GetMinSpeedByGear(1));
                _currentGearSpeed = -CalculateCurrentGearSpeed(currentSpeed, 1);
            else
            {
                // для нейтралки
                CurrentPowerToWheels = OneSpeedPeriod;
                return 0;
            }
            CurrentPowerToWheels = OneSpeedPeriod - Math.Abs(_currentGearSpeed);

            return GetMotorTorqueByCurrentSpeed(_currentGearSpeed);
        }
    }
}