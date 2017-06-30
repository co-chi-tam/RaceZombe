using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CBaseController {

		[Header ("Control")]
		[SerializeField]	private Transform m_TargetTransform;

		[Header ("Component")]
		[SerializeField]	private CBreakableObject m_BreakableObject;
		[SerializeField]	private CNavigateMovableObject m_NavigateObject;
		[SerializeField]	private CDamageObject m_DamageObject;

		private float m_Countdown = 5f;

		protected override void Start() {
			base.Start ();
			this.m_BreakableObject.Init (false);
			this.m_NavigateObject.Init (1f, 10f, this.m_Transform);
			this.m_DamageObject.Init (0f, 10f);
		}

		protected override void Update() {
			base.Update();
			if (this.m_DamageObject.IsOutOfDamage ()) {
				m_Countdown -= Time.deltaTime;
				if (m_Countdown <= 0f) {
					Destroy (this.gameObject);
				}
			} else {
				if (this.m_NavigateObject.IsNearTarget () == false) {
					this.m_NavigateObject.SetDestination (this.m_TargetTransform.position);
				}
			}
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_BreakableObject);
			this.m_Components.Add (this.m_NavigateObject);
			this.m_Components.Add (this.m_DamageObject);
		}

		private void InteractiveOrtherObject(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i];
				if (contactObj.otherCollider.gameObject.name == "BlueBird") {
					var controller = collision.gameObject.GetComponent<CBaseController> ();
					var driver = controller.GetCustomComponent<CWheelDriver> ();
					if (driver != null && driver.GetVelocityKMH() > 10f) {
						this.m_DamageObject.CalculteDamge (5f);
						if (this.m_DamageObject.IsOutOfDamage ()) {
							this.m_BreakableObject.BreakObjects (true);
						}
					}
				}
			}
		}

		void OnCollisionEnter(Collision collision) {
			this.InteractiveOrtherObject (collision);
		}
		
	}
}
