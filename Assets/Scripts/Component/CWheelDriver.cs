using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CWheelDriver : CRigidbodyObject {

		[Header ("Wheel configs")]
		[Tooltip("Maximum steering angle of the wheels")]
		public float maxAngle = 30f;
		[Tooltip("Maximum torque applied to the driving wheels")]
		public float maxTorque = 300f;
		[Tooltip("Maximum brake torque applied to the driving wheels")]
		public float brakeTorque = 30000f;

		[Header ("Wheel Colliders")]
		[SerializeField]	private WheelCollider[] m_ForwardWheels;
		[SerializeField]	private WheelCollider[] m_BackwardWheels;

		[Header ("Wheel advance config")]
		[Tooltip("The motor torque's threahold when the physics engine motor torque.")]
		public float motorTorqueThreahold = 0.75f;
		[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
		public float criticalSpeed = 5f;
		[Tooltip("Simulation sub-steps when the speed is above critical.")]
		public int stepsBelow = 5;
		[Tooltip("Simulation sub-steps when the speed is below critical.")]
		public int stepsAbove = 1;
		[Header("Events")]
		public UnityEvent OnMoved;
		public UnityEvent OnStopped;

		public new void Init(float maxSpeed) {
			base.Init ();
			this.maxTorque = maxSpeed;
		}

		public virtual void UpdateDriver(float angleInput, float torqueInput, bool handBrakeInput)
		{
			this.m_ForwardWheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);
			float angle = maxAngle * angleInput;
			float torque = maxTorque * torqueInput;
			float handBrake = handBrakeInput ? brakeTorque : 0f;

			// Forward wheels
			for (int i = 0; i < this.m_ForwardWheels.Length; i++) {
				var wheel = this.m_ForwardWheels [i];
				// A simple car where front wheels steer while rear ones drive.
				if (wheel.transform.localPosition.z > 0) {
					wheel.steerAngle = angle;
				}
				if (wheel.transform.localPosition.z < 0) {
					wheel.brakeTorque = handBrake;
				}
				if (wheel.transform.localPosition.z < 0) {
					wheel.motorTorque = Mathf.Lerp (wheel.motorTorque, torque, motorTorqueThreahold);
				}
			}
			// Backward wheels
			for (int i = 0; i < this.m_BackwardWheels.Length; i++) {
				// A simple car where back wheels steer while rear ones drive.
				var wheel = this.m_BackwardWheels [i];
				if (wheel.transform.localPosition.z < 0) {
					wheel.brakeTorque = handBrake;
				}
				if (wheel.transform.localPosition.z < 0) {
					wheel.motorTorque = Mathf.Lerp (wheel.motorTorque, torque, motorTorqueThreahold);
				}
			}
			// Event
			if (handBrakeInput == false) {
				if (this.OnMoved != null) {
					this.OnMoved.Invoke ();
				} 
			} else {
				if (this.OnStopped != null) {
					this.OnStopped.Invoke ();
				}
			}
		}

	}
}
