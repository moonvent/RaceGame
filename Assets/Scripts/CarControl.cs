using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public enum Axel 
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject model;
    public WheelCollider collider;
    public Axel axel;
}

public class CarControl : MonoBehaviour {

    // [SerializeField] 
    private const float MaxAccelaration = 500000f;
    // [SerializeField]
    private const float TurnSensitivity = 1f;
    // [SerializeField]
    private const float MaxSteerAngle = 30f;
    
    [SerializeField] private List<Wheel> wheels;
    private Vector3 _centerOfMass;

    private float inputX, inputY;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
    }

    public void Update()
    {
        AnimateWheels();
        GetInputs();
    }

    private void GetInputs()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void LateUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.axel == Axel.Rear)
            {
                wheel.collider.motorTorque = inputY * MaxAccelaration * Time.deltaTime;
            }
        }
    }

    private void Turn()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                float _steerAngle = inputX * TurnSensitivity * MaxSteerAngle;
                // wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 1f);
                wheel.collider.steerAngle = _steerAngle;
            }
        }
    }

    public void AnimateWheels()
    {
        foreach (Wheel wheel in wheels)
        {
            Quaternion _rot;
            Vector3 _pos;
            wheel.collider.GetWorldPose(out _pos, out _rot);
            wheel.model.transform.position = _pos;
            wheel.model.transform.rotation = _rot;
        }    
    }
}
    