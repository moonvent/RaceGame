using System;
using UnityEngine;

namespace CarBehavior
{
    public abstract class PowerDistribution: AccelarationAndBraking
    {
        public void Move(float maxPower)
        {
            // Debug.Log($"{Rb.velocity.sqrMagnitude} // ");
            if (maxPower > NoPower)
                Accelarate(maxPower);
            else if (maxPower == 0f)
            {
                GearsSwitch.ResetPowerAfterAccelerate();
                Accelarate(0);
            }
            else
            {
                GearsSwitch.ResetPowerAfterAccelerate(braking: true);
                Brake();   
                
            }
        }

    }
}