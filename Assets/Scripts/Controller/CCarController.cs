using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CBaseController {

		[Header ("Data")]
		[SerializeField]	private CObjectData m_Data;

		[Header ("Components")]
		[SerializeField]	private CWheelDriver m_WheelDriver;
		[SerializeField]	private CDamageObject m_DamageObject;

		protected override void Start() {
			base.Start ();
			this.m_WheelDriver.Init (this.m_Data.maxSpeed);
			this.m_DamageObject.Init (this.m_Data.currentDefend, this.m_Data.currentHealth);
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
			this.m_Components.Add (this.m_DamageObject);
		}

		protected override void UpdateComponents(float dt) {
			base.UpdateComponents (dt);
		}

		public void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			this.m_WheelDriver.UpdateDriver (angleInput, torqueInput, brakeInput);
		}

		private void InteractiveOrtherObject(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i];
				if (contactObj.otherCollider.gameObject.layer == LayerMask.NameToLayer ("Zombie")) {
					var controller = collision.gameObject.GetComponent<CBaseController> ();
					if (controller == null)
						continue;
					var damageObject = controller.GetCustomComponent<CDamageObject> ();
					if (damageObject != null && this.m_WheelDriver.GetVelocityKMH() > 20f) {
						damageObject.CalculteDamage (this.m_Data.currentAttack);
					}
				}
			}
		}

		protected override void OnCollisionEnter(Collision collision) {
			base.OnCollisionEnter (collision);
			this.InteractiveOrtherObject (collision);
		}

	}
}
