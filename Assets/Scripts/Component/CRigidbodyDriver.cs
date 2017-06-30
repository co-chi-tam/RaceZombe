using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CRigidbodyDriver: CComponent {

		[Header ("Physic")]
		[SerializeField]	private Rigidbody m_Rigidbody;

		public virtual float GetVelocityMS() {
			return Mathf.Abs (this.m_Rigidbody.velocity.magnitude);
		}

		public virtual float GetVelocityMH() {
			return this.GetVelocityMS () * 2.237f;
		}

		public virtual float GetVelocityKMH() {
			return this.GetVelocityMS () * 3.6f;
		}

	}
}
