using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CBaseController {

		[Header ("Data")]
		[SerializeField]	private CObjectData m_Data;

		[Header ("Control")]
		[SerializeField]	private Collider m_TargetCollider;

		[Header ("Component")]
		[SerializeField]	private CBreakableObject m_BreakableObject;
		[SerializeField]	private CMovableObject m_MovableObject;
		[SerializeField]	private CDamageObject m_DamageObject;

		private float m_Countdown = 5f;

		protected override void Start() {
			base.Start ();
			this.m_BreakableObject.Init (false);
			this.m_MovableObject.Init (1f, 10f, this.m_Data.maxSpeed, this.m_Transform);
			this.m_DamageObject.Init (this.m_Data.currentDefend, this.m_Data.currentHealth);
		}

		protected override void Update() {
			base.Update();
			if (this.m_DamageObject.IsOutOfDamage ()) {
				m_Countdown -= Time.deltaTime;
				if (m_Countdown <= 0f) {
					Destroy (this.gameObject);
				}
				this.m_BreakableObject.BreakObjects (true);
			} else {
				if (this.m_TargetCollider == null)
					return;
				this.m_MovableObject.SetDestination (this.m_TargetCollider.transform.position, this.m_TargetCollider);
				if (this.m_MovableObject.IsNearTarget () == false) {
					this.m_MovableObject.Move (Time.deltaTime);
				}
			}
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_BreakableObject);
			this.m_Components.Add (this.m_MovableObject);
			this.m_Components.Add (this.m_DamageObject);
		}

		public virtual void SetTargetCollider(Collider target) {
			this.m_TargetCollider = target;
		}
		
	}
}
