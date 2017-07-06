using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CMovableObject : CRigidbodyObject {

		[Header ("Info")]
		[SerializeField]	protected float m_Radius = 0.5f;
		[SerializeField]	protected float m_MinDistance = 1f;
		[SerializeField]	protected float m_MaxDistance = 10f;
		[SerializeField]	protected float m_Speed;

		[Header ("Advance")]
		[SerializeField]	protected EMoveType m_MoveType;

		[Header("Events")]
		public UnityEvent OnMoved;

		protected float m_PerlineNoiseX = 0f;
		protected float m_PerlineNoiseY = 0f;
		protected Transform m_CurrentTransform;
		protected bool m_Stop = false;

		public Vector3 targetPosition;

		public enum EMoveType: byte {
			Smooth = 0,
			PerlineNoise = 1
		}

		public new void Init(float min, float max, float speed, Transform mTransform) {
			base.Init ();
			this.m_MinDistance = min;
			this.m_MaxDistance = max;
			this.m_Speed = speed;
			this.m_CurrentTransform = mTransform;
			this.m_PerlineNoiseX = UnityEngine.Random.Range (0f, 100f);
			this.m_PerlineNoiseY = 0f;
		}

		public virtual void Move(float dt) {
			var direction = targetPosition - this.m_CurrentTransform.position;
			var angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			var position = direction.normalized * this.GetMoveSpeedSample(dt);
			this.m_Rigidbody.position 
				= Vector3.Lerp (this.m_Rigidbody.position, this.m_Rigidbody.position + position, 0.5f);
			this.m_Rigidbody.rotation 
				= Quaternion.Lerp (this.m_Rigidbody.rotation, Quaternion.AngleAxis (angle, Vector3.up), 0.5f);
			if (this.OnMoved != null) {
				this.OnMoved.Invoke ();
			}
		}

		protected virtual float GetMoveSpeedSample(float dt) {
			switch (this.m_MoveType) {
			case EMoveType.Smooth:
				return this.m_Speed * dt;
			case EMoveType.PerlineNoise:
				this.m_PerlineNoiseX += dt;
				this.m_PerlineNoiseY += dt;
				return this.m_Speed * Mathf.PerlinNoise (this.m_PerlineNoiseX, 0f) * dt;
			default:
				return this.m_Speed * dt;
			}
		}

		public virtual void Stop() {
			this.m_CurrentTransform.position = this.targetPosition;
		}

		public virtual bool IsOutOfRange() {
			return Vector3.Distance (this.m_CurrentTransform.position, this.targetPosition) > this.m_MaxDistance + this.m_Radius;
		}

		public virtual bool IsNearTarget() {
			return Vector3.Distance (this.m_CurrentTransform.position, this.targetPosition) < this.m_MinDistance - this.m_Radius;
		}

		public virtual void SetDestination(Vector3 value, Collider colliderTarget = null) {
			this.targetPosition = value;
			if (colliderTarget != null) {
				var fixSize = this.m_CurrentTransform.position;
				this.targetPosition = colliderTarget.ClosestPointOnBounds (fixSize);
			}
		}

		public virtual Vector3 GetDestination() {
			return this.targetPosition;
		}

	}
}
