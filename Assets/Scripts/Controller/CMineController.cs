using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CMineController : CBaseController {

		[Header("Data")]
		[SerializeField]	protected CBombData m_Data;

		[Header("Component")]
		[SerializeField]	protected CExplosionObject m_ExplosionObject;

		private float m_Countdown = 2f;

		protected override void Start() {
			base.Start ();
			this.m_ExplosionObject.Init (this.m_Transform, this.m_Data.currentDamage);
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.GetActive () == false) {
				this.m_Countdown -= Time.deltaTime;
				if (this.m_Countdown <= 0f) {
					DestroyObject ();
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
			if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground"))
				return;
			base.OnTriggerEnter (collider);
			this.m_ExplosionObject.Explosion ((contactRigidbody) => {
				if (this.GetActive () == false)
					return;
				var controller = contactRigidbody.GetComponent<CBaseController>();
				if (controller != null) {
					controller.ApplyDamage (this, this.m_ExplosionObject.GetDamage());
				}
				this.SetActive (false);
			});
		}
		
	}
}
