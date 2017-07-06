using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CInterativeCarPartController : CCarPartController {

		[Header ("Colliders")]
		[SerializeField]	protected List<CObjectController> m_HitBoxContacts;

		protected float m_CurrentActionDelay = -1f;

		protected override void Start ()
		{
			base.Start ();
			this.m_HitBoxContacts = new List<CObjectController> ();
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.m_CurrentActionDelay > 0f) {
				this.m_CurrentActionDelay -= Time.deltaTime;
			}
		}

		public override void InteractiveOrtherObject (GameObject contactObj)
		{
//			base.InteractiveOrtherObject (contactObj);
			if (this.m_CurrentActionDelay <= 0f) {
				for (int i = 0; i < this.m_HitBoxContacts.Count; i++) {
					var objCtrl = this.m_HitBoxContacts [i];
					if (objCtrl == null) {
						this.m_HitBoxContacts.Remove (objCtrl);
						continue;
					} 
					if (objCtrl.GetActive () == false) {
						continue;
					}
					objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
				}
				this.SetAnimator ("Active");
				this.m_CurrentActionDelay = this.m_Data.actionDelay;
				if (this.m_HitBoxContacts.Count > 0) {
					// Decrease Durability
					this.ApplyEngineWear (this.GetDamage () - this.m_DamageObject.maxResistant);
				}
			}
		}

		protected override void OnCollisionEnter (Collision collision)
		{
//			base.OnCollisionEnter (collision);
		}

		protected override void OnCollisionStay (Collision collision)
		{
//			base.OnCollisionStay (collision);
		}

		protected override void OnCollisionExit (Collision collision)
		{
//			base.OnCollisionExit (collision);
		}

		protected override void OnTriggerEnter (Collider collider)
		{
//			base.OnTriggerEnter (collider);
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			if (objController != null) {
				if (this.m_HitBoxContacts.Contains (objController) == false) {
					this.m_HitBoxContacts.Add (objController);
				}
			}
		}

		protected override void OnTriggerExit (Collider collider)
		{
//			base.OnTriggerExit (collider);
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			if (objController != null) {
				if (this.m_HitBoxContacts.Contains (objController)) {
					this.m_HitBoxContacts.Remove (objController);
				}
			}
		}

	}
}
