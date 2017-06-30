using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CMovableObject : CComponent {

		[SerializeField]	protected float m_Radius = 0.5f;
		[SerializeField]	protected float m_MinDistance = 1f;
		[SerializeField]	protected float m_MaxDistance = 10f;
		[SerializeField]	protected float m_Speed;

		protected Transform m_CurrentTransform;
		protected bool m_Stop = false;

		public Vector3 targetPosition;

		public new void Init(float min, float max, float speed, Transform mTransform) {
			base.Init ();
			this.m_MinDistance = min;
			this.m_MaxDistance = max;
			this.m_Speed = speed;
			this.m_CurrentTransform = mTransform;
		}

		public virtual void Move(float dt) {
			var direction = targetPosition - this.m_CurrentTransform.position;
			var angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			var position = direction.normalized * this.m_Speed * dt;
			this.m_CurrentTransform.position 
				= Vector3.Lerp (this.m_CurrentTransform.position, this.m_CurrentTransform.position + position, 0.5f);
			this.m_CurrentTransform.rotation 
				= Quaternion.Lerp (this.m_CurrentTransform.rotation, Quaternion.AngleAxis (angle, Vector3.up), 0.5f);
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
