using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CSteeringMovableObject : CMovableObject {

		#region Properties

		[SerializeField]	protected LayerMask m_Obstacles;

		[SerializeField]	protected float[] m_AngleCheckings = new float[] { 0, -15, 15, -45, 45, -90, 90 }; 
		[SerializeField]	protected float[] m_AngleAvoidances = new float[] { 10, 40, -40, 60, -60, 80, -80 }; 
		[SerializeField]	protected float[] m_LengthAvoidances = new float[] { 3f, 3f, 3f, 3f, 3f, 1.5f, 1.5f };

		protected float m_Angle;
		protected float m_SpeedThreshold;
		protected Vector3 m_Direction;
		protected float radiusBase = 0f;

		#endregion

		#region Implementation Component

		public new void Init (float speed, Transform mTransform)
		{
			base.Init(speed, mTransform);
			this.m_Angle = 0f;
			this.m_SpeedThreshold = 1f;
		}

		#endregion

		#region Main methods

		public override void Move (float dt) {
			if (this.IsNearTarget() == false) {
				m_Direction = targetPosition - this.m_Rigidbody.position;
				var forward = this.m_Rigidbody.transform.forward;
				m_Angle = Mathf.Atan2 (m_Direction.x, m_Direction.z) * Mathf.Rad2Deg;
				DrawRayCast ();
				var position = forward * this.m_Speed * dt * m_SpeedThreshold;
				if (position != Vector3.zero) {
					this.m_Rigidbody.position = Vector3.Lerp (this.m_Rigidbody.position, this.m_Rigidbody.position + position, 0.5f);
				}
				this.m_Rigidbody.rotation = Quaternion.Lerp (this.m_Rigidbody.rotation, Quaternion.AngleAxis (m_Angle, Vector3.up), 0.5f);
#if UNITY_EDITOR
				Debug.DrawRay (this.m_Rigidbody.position, m_Direction, Color.green);	
#endif
			}
			Reset ();
		}

		public virtual void LookForwardToTarget(Vector3 value) {
			if (this.m_Rigidbody != null) {
				m_Direction = value - this.m_Rigidbody.position;
				var forward = this.m_Rigidbody.transform.forward;
				m_Angle = Mathf.Atan2 (m_Direction.x, m_Direction.z) * Mathf.Rad2Deg;
				this.m_Rigidbody.rotation = Quaternion.Lerp (this.m_Rigidbody.rotation, Quaternion.AngleAxis (m_Angle, Vector3.up), 0.1f);
			}
		}

		protected virtual void DrawRayCast() {
			var forward = this.m_Rigidbody.transform.forward;
			var tmpAngle = m_Angle;
			var tmpSpeedThreshold = m_SpeedThreshold;
			for (int i = 0; i < m_AngleCheckings.Length; i++) {
				var rayCast = Quaternion.AngleAxis(m_AngleCheckings[i], this.m_Rigidbody.transform.up) * forward * m_LengthAvoidances[i];
				RaycastHit rayCastHit;
				if (Physics.Raycast (this.m_Rigidbody.position + (rayCast.normalized * radiusBase), rayCast, out rayCastHit, m_LengthAvoidances[i], this.m_Obstacles)) {
					tmpAngle += m_AngleAvoidances [i] * (1f - (rayCastHit.distance / m_LengthAvoidances[i]));
					tmpSpeedThreshold -= 1f / ((float)m_AngleCheckings.Length / 1.15f);
				} 
#if UNITY_EDITOR
				Debug.DrawRay (this.m_Rigidbody.position + (rayCast.normalized * radiusBase), rayCast, Color.white);
#endif
			}
			m_Angle = tmpAngle;
			m_SpeedThreshold = tmpSpeedThreshold;
		}

		protected virtual void Reset() {
			m_SpeedThreshold = 1f;
		}

		#endregion

	}
}
