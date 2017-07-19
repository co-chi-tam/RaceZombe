using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CMineController : CInteractiveCarPartController {

		#region Properties

		[Header("Component")]
		[SerializeField]	protected CExplosionObject m_ExplosionObject;

		private float m_Countdown = -1f;

		#endregion

		#region Implementation MonoBehavour

		protected override void Start() {
			this.m_ExplosionObject.Init (this.m_Transform, this.m_Data.currentDamage);
			base.Start ();
			this.SetTimer (this.m_Data.actionDelay, () => {
				this.m_Active = true;
			});
		}

		protected override void LateUpdate ()
		{
//			base.LateUpdate ();
			if (m_Countdown > 0f) {
				this.m_Countdown -= Time.deltaTime;
				if (this.m_Countdown <= 0f) {
					this.SetActive (false);
					this.DestroyObject ();
				}
			} else {
				if (this.m_HitBoxContacts.Count > 0) {
					this.ExplosionObject ();
				}
			}
		}

		protected override void OnTriggerEnter (Collider collider)
		{
//			base.OnTriggerEnter (collider);
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << collider.gameObject.layer));
			if (objController != null && isExceptionLayer == false) {
				if (this.m_HitBoxContacts.Contains (objController) == false) {
					this.m_HitBoxContacts.Add (objController);
				}
			}
		}

		protected override void OnTriggerExit (Collider collider)
		{
//			base.OnTriggerExit (collider);
			var objController = collider.gameObject.GetObjectController<CObjectController> ();
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << collider.gameObject.layer));
			if (objController != null && isExceptionLayer == false) {
				if (this.m_HitBoxContacts.Contains (objController)) {
					this.m_HitBoxContacts.Remove (objController);
				}
			}
		}

		#endregion

		#region Implementation Controller

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_ExplosionObject);
		}

		public virtual void ExplosionObject() {
			this.m_ExplosionObject.WorldExplosion ((contactRigidbody) => {
				var controller = contactRigidbody.GetComponent<CObjectController>();
				if (controller != null && controller != this) {
					controller.ApplyDamage (this, this.m_ExplosionObject.GetDamage());
				}
				this.m_Countdown = this.m_Countdown < 0f ? 1f : this.m_Countdown; 
				return controller != null;
			});
		}

		#endregion
		
	}
}
