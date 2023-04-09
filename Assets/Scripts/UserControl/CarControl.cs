using CarBehavior;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserControl
{

    public class CarControl : CarParams
    {
        private Car _car;
        private float _inputTurn, _inputAcceleration;
        private bool _inputHandBrake = false, _gearUp = false, _gearDown = false, _inputClutch = false;

        private void Awake()
        {
            gameObject.AddComponent<Rigidbody>();
            
            _car = new Car(gameObject);
            CarUI = new CarUI(gameObject, _car.GearsSwitch.OneSpeedPeriod);
        }
        
        public void Update()
        {
            _car.AnimateWheels();
            GetInputs();
        }
        
        private void GetInputs()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                _inputTurn = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                _inputTurn = 1;
            else
                _inputTurn = 0;
            
            if (Input.GetKey(KeyCode.UpArrow))
                _inputAcceleration = 1;
            else if (Input.GetKey(KeyCode.DownArrow))
                _inputAcceleration = -1;
            else
                _inputAcceleration = 0;
            
            _inputHandBrake = Input.GetKey(KeyCode.Space);
            _inputClutch = Input.GetKey(KeyCode.LeftControl);
            
            if (Input.GetKeyDown(KeyCode.A) && !_gearUp) _gearUp = true;
            if (Input.GetKeyDown(KeyCode.Z) && !_gearDown) _gearDown = true;
        }
        
        private void FixedUpdate()
        {
            CarUI.UpdateSpeedometer(speedValue: Rb.velocity.sqrMagnitude, powerValue: _car.GearsSwitch.CurrentPowerToWheels, currentGear: _car.CurrentGear);
            _car.Move(_inputAcceleration);
            _car.Turn(_inputTurn);
            _car.HandBrake(_inputHandBrake);
            
            if (_inputClutch)
            {
                if ( _gearUp)
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
}