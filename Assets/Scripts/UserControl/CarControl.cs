using CarBehavior;
using UnityEngine;

namespace UserControl
{

    public class CarControl : MonoBehaviour
    {
        private Car _car;
        private float _inputTurn, _inputAcceleration;
        private bool _inputHandBrake = false;
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
            _inputHandBrake = Input.GetKey(KeyCode.Space);
        }
        
        private void LateUpdate()
        {
            _car.Riding.Move(_inputAcceleration);
            _car.Riding.Turn(_inputTurn);
            _car.Riding.HandBrake(_inputHandBrake);
        }

    }
}