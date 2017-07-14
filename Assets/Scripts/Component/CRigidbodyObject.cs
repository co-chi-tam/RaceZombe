using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CRigidbodyObject: CComponent {

		#region Properties

		[Header ("Physic")]
		[SerializeField]	protected Rigidbody m_Rigidbody;

		#endregion

		#region Main methods

		public virtual float GetVelocityMS() {
			if (this.m_Rigidbody == null)
				return 0f;
			var velocity = Mathf.Abs (this.m_Rigidbody.velocity.magnitude);
			return velocity < 0.01f ? 0f : velocity ;
		}

		public virtual float GetVelocityMH() {
			return this.GetVelocityMS () * 2.237f;
		}

		public virtual float GetVelocityKMH() {
			return this.GetVelocityMS () * 3.6f;
		}

		#endregion

	}
}
