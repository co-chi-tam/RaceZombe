using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CExplosionObject : CRigidbodyObject {

		#region Properties

		[Header("Info")]
		[SerializeField]	protected LayerMask m_TargetLayerMask;
		[SerializeField] 	protected float m_DetectRadius = 3f;
		[SerializeField]	protected float m_ExplosionForceMass = 15f;
		[SerializeField] 	protected float m_ExplosionRadius = 3f;
		[SerializeField]	protected float m_ExplosionUpward = 1f;

		[Header("Events")]
		public UnityEvent OnExplosion;

		protected Transform m_Transform;
		protected float m_ExplosionDamage = 10f;

		#endregion

		#region Implementation Component

		public new void Init (Transform transform, float damage)
		{
			base.Init ();
			this.m_Transform = transform;
			this.m_ExplosionDamage = damage;
		}

		#endregion

		#region Main methods

		public virtual void WorldExplosion(Func<Rigidbody, bool> contact = null) {
			var contacts = Physics.OverlapSphere (this.m_Transform.position, this.m_DetectRadius, this.m_TargetLayerMask);
			for (int i = 0; i < contacts.Length; i++) {
				var contactRigidbody = contacts[i].gameObject.GetComponent<Rigidbody> ();
				if (contactRigidbody != null) {
					if (contact != null) {
						if (contact (contactRigidbody)) {
							this.ExplosionObject (contactRigidbody);
						}
					} else {
						this.ExplosionObject (contactRigidbody);
					}
				}
			}
			if (this.OnExplosion != null) {
				this.OnExplosion.Invoke ();
			}
		}

		public virtual void ObjectExplosion(Rigidbody body, Func<bool> contact = null) {
			if (contact != null) {
				if (contact ()) {
					this.ExplosionObject (body);
				}
			}
			if (this.OnExplosion != null) {
				this.OnExplosion.Invoke ();
			}
		}

		protected virtual void ExplosionObject(Rigidbody contactRigidbody) {
			contactRigidbody.AddExplosionForce (
				contactRigidbody.mass * 2 + this.m_ExplosionForceMass,   // explosionForce
				this.m_Transform.position, // position
				this.m_ExplosionRadius, // radius
				this.m_ExplosionUpward, // upwardForce
				ForceMode.Impulse);  // force mode
		}

		public virtual float GetDamage() {
			return this.m_ExplosionDamage;
		}

		public virtual float GetRadius() {
			return this.m_DetectRadius;
		}

		#endregion
		
	}
}
