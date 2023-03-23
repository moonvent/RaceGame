using CarBehavior;
using UnityEngine;

namespace UserControl
{

    public class CarControl : CarParams
    {
        private Car _car;
        private float _inputTurn, _inputAcceleration;
        private bool _inputHandBrake = false, _gearUp = false, _gearDown = false;
        private void Awake()
        {
            gameObject.AddComponent<Rigidbody>();
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
            if (Input.GetKeyDown(KeyCode.A) && !_gearUp) _gearUp = true;
            if (Input.GetKeyDown(KeyCode.Z) && !_gearDown) _gearDown = true;
        }
        
        private void FixedUpdate()
        {
            _car.Move(_inputAcceleration);
            _car.Turn(_inputTurn);
            _car.HandBrake(_inputHandBrake);
            if (_gearUp)
            {
                _car.GearUp();
                _gearUp = false;
            }
            else if (_gearDown)
            {
                _car.GearDown();
                _gearDown = false;
            }
        }

    }
}