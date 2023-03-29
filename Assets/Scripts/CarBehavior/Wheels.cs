using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarBehavior
{
    public enum Axel 
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public MeshRenderer model;
        public WheelCollider collider;
        public Axel axel;
    }
    
    public class Wheels
    {
        private Transform _transform;

        private Wheel _frontLeftWheel;
        private Wheel _frontRightWheel;
        private Wheel _rearLeftWheel;
        private Wheel _rearRightWheel;

        private const string WheelsCollidersObjectTitle = "WheelsColliders";
        private const string WheelsModelsObjectTitle = "WheelsModels";
        
        private Transform WheelsColliders { get; set; }
        private Transform WheelsModels { get; set; }
        
        private const int AmountWheels = 4;
        private const int OneAxelAmountWheels = 2;

        public Wheel[] WheelsArray = new Wheel[AmountWheels];
        public Wheel[] AllWheelsArray = new Wheel[AmountWheels];
        public Wheel[] RearWheelsArray = new Wheel[OneAxelAmountWheels];
        public Wheel[] FrontWheelsArray = new Wheel[OneAxelAmountWheels];

        public Wheel[] AccelatationWheels;

        private void InitializeWheelsColliders(GameObject carObject)
        {
            WheelsColliders = carObject.transform.Find(WheelsCollidersObjectTitle);
                            
            _frontLeftWheel.collider = WheelsColliders.Find("FrontLeftWheel").GetComponent<WheelCollider>();
            _frontRightWheel.collider = WheelsColliders.Find("FrontRightWheel").GetComponent<WheelCollider>();
            _rearLeftWheel.collider = WheelsColliders.Find("RearLeftWheel").GetComponent<WheelCollider>();
            _rearRightWheel.collider = WheelsColliders.Find("RearRightWheel").GetComponent<WheelCollider>();
        }

        private void InitializeAxels()
        {
            _frontLeftWheel.axel = _frontRightWheel.axel = Axel.Front;
            _rearLeftWheel.axel = _rearRightWheel.axel = Axel.Rear;
        }
        
        private void InitializeWheelsModels(GameObject carObject)
        {
            WheelsModels = carObject.transform.Find(WheelsModelsObjectTitle);
                                
            _frontLeftWheel.model = WheelsModels.Find("FrontLeftWheel").GetComponent<MeshRenderer>();
            _frontRightWheel.model = WheelsModels.Find("FrontRightWheel").GetComponent<MeshRenderer>();
            _rearLeftWheel.model = WheelsModels.Find("RearLeftWheel").GetComponent<MeshRenderer>();
            _rearRightWheel.model = WheelsModels.Find("RearRightWheel").GetComponent<MeshRenderer>();
        }

        private void AddWheelsToColletion()
        {
            WheelsArray[0] = _frontLeftWheel;
            WheelsArray[1] = _frontRightWheel;
            WheelsArray[2] = _rearLeftWheel;
            WheelsArray[3] = _rearRightWheel;

            AllWheelsArray = WheelsArray;
            
            FrontWheelsArray[0] = _frontLeftWheel;
            FrontWheelsArray[1] = _frontRightWheel;
            
            RearWheelsArray[0] = _rearLeftWheel;
            RearWheelsArray[1] = _rearRightWheel;
        }

        /// <summary>
        /// Initialize wheels
        /// </summary>
        /// <param name="carObject">prefab/cars/anyCar</param>
        /// <exception cref="MissingReferenceException">Thrown when didn't have a wheels.</exception>
        private void InitializeWheels(GameObject carObject)
        {
            try
            {
                InitializeWheelsColliders(carObject);
                InitializeWheelsModels(carObject);
                InitializeAxels();
                AddWheelsToColletion();
            }
            catch (MissingReferenceException e)
            {
                Debug.LogError($"Can't find wheels objects in Car Object");
                Console.WriteLine(e);
                throw e;
            }
        }

        private void SetSuspencion(int wheelSpring, int wheelDump)
        {
            JointSpring jointSpring = new JointSpring();
            jointSpring.spring = wheelSpring;
            jointSpring.damper = wheelDump;
            jointSpring.targetPosition = 0.5f;
            
            foreach (Wheel wheel in WheelsArray)
                wheel.collider.suspensionSpring = jointSpring;
        }

        private void InitAddAccelarationWheels(WheelDriveType wheelDriveType)
        {
            switch (wheelDriveType)
            {
                case WheelDriveType.Awd:
                    AccelatationWheels = WheelsArray;
                    return;
                case WheelDriveType.Rwd:
                    AccelatationWheels = RearWheelsArray;
                    return;
                case WheelDriveType.Fwd:
                    AccelatationWheels = FrontWheelsArray;
                    return;
            }
        }
        
        public Wheels (GameObject carObject, int wheelSpring, int wheelDump, WheelDriveType wheelDriveType)
        {
            InitializeWheels(carObject);
            SetSuspencion(wheelSpring, wheelDump);
            InitAddAccelarationWheels(wheelDriveType);
        }
        
    }
}