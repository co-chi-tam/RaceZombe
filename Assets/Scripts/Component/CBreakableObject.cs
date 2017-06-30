using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CBreakableObject: CComponent {

		[SerializeField]	private Collider[] m_Colliders;
		[SerializeField]	private Rigidbody[] m_Rigidbodies;
		[Header ("Event")]
		[SerializeField]	public UnityEvent OnBreaked;

		public virtual void Init(bool value) {
			this.BreakObjects (value);
		}

		public virtual void BreakObjects(bool value) {
			for (int i = 0; i < this.m_Colliders.Length; i++) {
				this.m_Colliders [i].enabled = value;
				this.m_Colliders [i].isTrigger = !value;
			}
			for (int i = 0; i < this.m_Rigidbodies.Length; i++) {
				this.m_Rigidbodies [i].useGravity = value;
				this.m_Rigidbodies [i].isKinematic = !value;
				this.m_Rigidbodies [i].constraints = value ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
			}
			if (value == true && this.OnBreaked != null) {
				this.OnBreaked.Invoke ();
			}
		}

	}
}
