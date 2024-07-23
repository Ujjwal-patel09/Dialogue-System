using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum WheelType
    {
      front,
      Back
    }
    
    [Serializable]
    public struct Wheel
    {
      public GameObject WheelModel;
      public WheelCollider wheelCollider;
      public WheelType wheelType;
    }

    public float MoveSpeed;
    public float BreakForce;
    public float TrunSensitivity = 1f;
    public float MaxStearAngle;
    public Vector3 Center_Of_Mass;
    public List<Wheel> wheels;

    private float MoveVerticalInput;
    private float MoveHorizontalInput;
    private Rigidbody rb;

  
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      rb.centerOfMass = Center_Of_Mass;
    }

    void Update()
    {
      GetInput();
      AnimamteWheel();
    }

    void FixedUpdate() 
    {
      Movement();
      Break();
    }

    // Getting Player Input//
    void GetInput()
    {
      MoveVerticalInput   = Input.GetAxis("Vertical");// for getting input for "W" & "S"
      MoveHorizontalInput = Input.GetAxis("Horizontal");// for getting input for "A" & "D"
    }
    
    // For Car Movement //
    void Movement()
    {   
      // for Front and Back movement //
      foreach (var Wheel in wheels)
      {
        Wheel.wheelCollider.motorTorque = MoveVerticalInput * MoveSpeed * 100f * Time.fixedDeltaTime;
      }

      // for Left and Right Movement //
      foreach (var wheel in wheels)
      {
        if(wheel.wheelType == WheelType.front)
        {
          var StearAngle = MoveHorizontalInput * TrunSensitivity * MaxStearAngle;
          wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle,StearAngle,0.5f); 
        }
      }
    }
    
    // For Breaking in car //
    void Break()
    {
      if(Input.GetKey(KeyCode.Space))
      {
        foreach (var wheel in wheels)
        {
          wheel.wheelCollider.brakeTorque = BreakForce* 100f * Time.fixedDeltaTime;
        }
      }else{
        foreach (var wheel in wheels)
        {
          wheel.wheelCollider.brakeTorque = 0;
        }
      }
    }

    // For Roating Wheels Model with Colliders//
    void AnimamteWheel()
    {
       foreach (var wheel in wheels)
       {
        Quaternion Collider_Rotation;
        Vector3 Collider_Position;

        wheel.wheelCollider.GetWorldPose( out Collider_Position, out Collider_Rotation);
        wheel.WheelModel.transform.position = Collider_Position;
        wheel.WheelModel.transform.rotation = Collider_Rotation;
       }
    }
}
