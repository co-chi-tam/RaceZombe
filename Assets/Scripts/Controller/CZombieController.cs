﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CBaseController {

		[Header ("Data")]
		[SerializeField]	protected CMovableData m_Data;

		[Header ("Control")]
		[SerializeField]	protected CBaseController m_TargetController;

		[Header ("Component")]
		[SerializeField]	protected CBreakableObject m_BreakableObject;
		[SerializeField]	protected CMovableObject m_MovableObject;
		[SerializeField]	protected CDamageObject m_DamageObject;

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
					DestroyObject (this.gameObject);
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
			if (this.m_TargetController == null)
				return;
			this.m_MovableObject.SetDestination (this.m_TargetController.transform.position, this.m_TargetController.GetCollider());
			if (this.m_MovableObject.IsNearTarget () == false) {
				this.m_MovableObject.Move (dt);
			}
		}

		public override void InteractiveOrtherObject (GameObject contactObj)
		{
//			base.InteractiveOrtherObject (contactObj);
			if (contactObj.gameObject.layer != LayerMask.NameToLayer ("Ground")) {
				var controller = contactObj.gameObject.GetObjectController<CBaseController> ();
				if (controller == null) {
					return;
				}
				if (controller.GetActive() == false) {
					return;
				}
				if (controller.GetVelocityKMH () > this.m_DamageObject.maxResistant) {
					this.ApplyDamage (controller, controller.GetDamage ());
				}
			}
		}

		public override void ApplyDamage (CBaseController attacker, float value)
		{
			base.ApplyDamage (attacker, value);
			this.m_DamageObject.CalculteDamage (value);
		}

		public virtual void SetTarget(CBaseController target) {
			this.m_TargetController = target;
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
