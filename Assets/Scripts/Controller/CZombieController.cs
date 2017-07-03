using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CBaseController {

		[Header ("Data")]
		[SerializeField]	private CMovableData m_Data;

		[Header ("Control")]
		[SerializeField]	private Collider m_TargetCollider;

		[Header ("Component")]
		[SerializeField]	private CBreakableObject m_BreakableObject;
		[SerializeField]	private CMovableObject m_MovableObject;
		[SerializeField]	private CDamageObject m_DamageObject;

		private float m_Countdown = 3f;

		protected override void Start() {
			base.Start ();
			this.m_BreakableObject.Init (false);
			this.m_MovableObject.Init (1f, 10f, this.m_Data.maxSpeed, this.m_Transform);
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
		}

		protected override void LateUpdate() {
			base.LateUpdate();
			if (this.m_DamageObject.IsOutOfDamage ()) {
				m_Countdown -= Time.deltaTime;
				if (m_Countdown <= 0f) {
					Destroy (this.gameObject);
				}
				this.m_BreakableObject.BreakObjects (true);
				this.SetActive (false);
			} else {
				this.ChasingTarget (Time.deltaTime);
			}
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_BreakableObject);
			this.m_Components.Add (this.m_MovableObject);
			this.m_Components.Add (this.m_DamageObject);
		}

		protected virtual void ChasingTarget(float dt) {
			if (this.m_TargetCollider == null)
				return;
			this.m_MovableObject.SetDestination (this.m_TargetCollider.transform.position, this.m_TargetCollider);
			if (this.m_MovableObject.IsNearTarget () == false) {
				this.m_MovableObject.Move (dt);
			}
		}

		public override void ApplyDamage (CBaseController attacker, float value)
		{
			base.ApplyDamage (attacker, value);
			if (attacker.GetVelocityKMH () > this.m_DamageObject.maxResistant) {
				this.m_DamageObject.CalculteDamage (value);
			}
		}

		public virtual void SetTargetCollider(Collider target) {
			this.m_TargetCollider = target;
		}

		public override float GetDamage ()
		{
			return this.m_Data.currentDamage;
		}

		public override float GetVelocityKMH ()
		{
			return this.m_MovableObject.GetVelocityKMH ();
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CMovableData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CMovableData;
		}
		
	}
}
