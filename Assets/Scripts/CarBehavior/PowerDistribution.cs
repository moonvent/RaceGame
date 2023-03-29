using System;
using UnityEngine;

namespace CarBehavior
{
    public abstract class PowerDistribution: AccelarationAndBraking
    {

        private const float MaxSpeedFullPercantage = 100f;

        private const float BackPowerInPercentOfMax = 20f;

        private const int BackGear = -1;
        private const int NeutralGear = 0;
        
       private float GetGearRatio()
       {
           return gearsPower[CurrentGear - 1];
           switch (CurrentGear)
           {
               case BackGear:
                   return BackPowerInPercentOfMax;
               case NeutralGear:
                   return NoPower;
               default:
                   return gearsPower[CurrentGear - 1];
           }
       }
       
       protected float CalculateAccelerationPower(float inputPower)
       {
        
            // float availablePower = maxSpeed / MaxSpeedFullPercantage * inputPower * GetGearPower();
            // float availablePower = torque / GetGearRatio();
            float availablePower = 1;
            
            // Debug.Log($"{Rb.velocity.sqrMagnitude} // {availablePower}");
            return inputPower * availablePower;   
       }

        public void Move(float maxPower)
        {
            // Debug.Log($"{Rb.velocity.sqrMagnitude} // ");
            if (maxPower > NoPower)
                Accelarate(maxPower);
            else if (maxPower == 0f)
                Accelarate(0);
            else
                Brake();   
        }

    }
}