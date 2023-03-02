using CarBehavior;
using UnityEngine;

namespace UserControl
{

    public class CarControl : MonoBehaviour
    {
        private Car _car;
        private float _inputTurn, _inputAcceleration;
        private void Awake()
        {
            _car = new Car(gameObject);
        }
        
        public void Update()
        {
            _car.AnimateWheels();
            GetInputs();
        }
        
        private void GetInputs()
        {
            _inputTurn = Input.GetAxis("Horizontal");
            _inputAcceleration = Input.GetAxis("Vertical");
        }
        
        private void LateUpdate()
        {
            _car.Move(_inputAcceleration);
            _car.Turn(_inputTurn);
        }

    }
}