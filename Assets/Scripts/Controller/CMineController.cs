using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CMineController : CObjectController {

		[Header("Data")]
		[SerializeField]	protected CBombData m_Data;

		[Header("Component")]
		[SerializeField]	protected CExplosionObject m_ExplosionObject;

		private float m_Countdown = -1f;

		protected override void Start() {
			this.m_ExplosionObject.Init (this.m_Transform, this.m_Data.currentDamage);
			base.Start ();
			// TEST
			this.SetTimer (this.m_Data.actionDelay, () => {
				this.m_Active = true;
			});
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (m_Countdown > 0f) {
				this.m_Countdown -= Time.deltaTime;
				if (this.m_Countdown <= 0f) {
					this.SetActive (false);
					this.DestroyObject ();
				}
			}
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_ExplosionObject);
		}

		protected override void OnTriggerEnter (Collider collider)
		{
			if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground") 
				|| collider.gameObject.layer == LayerMask.NameToLayer ("CarPart"))
				return;
			if (this.GetActive () == false)
				return;
			base.OnTriggerEnter (collider);
			this.m_ExplosionObject.Explosion ((contactRigidbody) => {
				var controller = contactRigidbody.GetComponent<CObjectController>();
				if (controller != null) {
					controller.ApplyDamage (this, this.m_ExplosionObject.GetDamage());
				}
				this.m_Countdown = this.m_Countdown < 0f ? 1f : this.m_Countdown; 
				return controller != null;
			});
		}
		
	}
}
