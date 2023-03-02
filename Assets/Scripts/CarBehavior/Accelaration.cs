using UnityEngine;


namespace CarBehavior
{
   
    public class Accelaration
    {
        // conventional accelaration units
        private const float MaxAccelarationPoint = 500000f;
        private Wheels _wheels;

        public Accelaration(Wheels wheels)
        {
            _wheels = wheels;
        }
    }   
}