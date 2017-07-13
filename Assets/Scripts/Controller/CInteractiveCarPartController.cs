using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	public class CInteractiveCarPartController : CCarPartController {

		[Header ("Auto interactive")]
		[SerializeField]	protected bool m_AutoInteractive = false;
		[SerializeField]	protected bool m_RepeatAction = false;
		[SerializeField]	protected bool m_StartWithAnimation = false;
		[SerializeField]	protected float m_DestroyAfter = 1f;
		public UnityEvent OnEventTriggerEnter;

		[Header ("Colliders")]
		[SerializeField]	protected List<CObjectController> m_HitBoxContacts;

		protected float m_CurrentActionDelay = -1f;

		protected override void Start ()
		{
			base.Start ();
			this.m_HitBoxContacts = new List<CObjectController> ();
			this.m_CurrentActionDelay = this.m_Data.actionDelay;
			if (this.m_StartWithAnimation) {
				this.SetAnimator ("Active");
			}
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.m_CurrentActionDelay > 0f) {
				this.m_CurrentActionDelay -= Time.deltaTime;
			}
			if (this.m_AutoInteractive) {
				if (this.m_CurrentActionDelay <= 0f) {
					this.InteractiveOrtherObject (this.gameObject, null);
					if (this.m_StartWithAnimation == false) {
						this.SetAnimator ("Active");
					}
					if (this.m_RepeatAction == false) {
						this.DestroyObject ();
					}
				}
			}
		}

		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj)
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
					this.InteractiveTarget (objCtrl);
				}
				this.m_CurrentActionDelay = this.m_DestroyAfter;
				if (this.m_HitBoxContacts.Count > 0) {
					// Decrease Durability
					this.ApplyEngineWear (this.m_Data.engineWearValue);
				}
			}
		}

		public virtual void AutoInteractive() {
			this.InteractiveOrtherObject (null, null);
		}

		protected virtual void InteractiveTarget(CObjectController target) {
			target.ApplyDamage (this.m_Owner, this.GetDamage ());
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
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << collider.gameObject.layer));
			if (isExceptionLayer == true)
				return;
			if (collider.isTrigger == true)
				return;
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			if (objController != null) {
				if (this.m_HitBoxContacts.Contains (objController) == false) {
					this.m_HitBoxContacts.Add (objController);
				}
			}
			if (this.m_AutoInteractive == false) {
				if (this.OnEventTriggerEnter != null) {
					this.OnEventTriggerEnter.Invoke ();
				}
			}
		}

		protected override void OnTriggerExit (Collider collider)
		{
//			base.OnTriggerExit (collider);
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << collider.gameObject.layer));
			if (isExceptionLayer == true)
				return;
			if (collider.isTrigger == true)
				return;
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			if (objController != null) {
				if (this.m_HitBoxContacts.Contains (objController)) {
					this.m_HitBoxContacts.Remove (objController);
				}
			}
		}

	}
}
