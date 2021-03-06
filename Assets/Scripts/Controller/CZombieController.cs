﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CObjectController {

		#region Properties

		[Header ("Data")]
		[SerializeField]	protected CMovableData m_Data;

		[Header ("Component")]
		[SerializeField]	protected CBreakableObject m_BreakableObject;
		[SerializeField]	protected CSteeringMovableObject m_MovableObject;
		[SerializeField]	protected CDamageObject m_DamageObject;
		[SerializeField]	protected CFSMComponent m_FSMComponent;

		protected bool m_AlreadyUpdateMission = false;

		#endregion

		#region Implementation MonoBehaviour

		public override void Init() {
			this.m_BreakableObject.Init (false);
			this.m_MovableObject.Init (this.m_Data.maxSpeed, this.m_Transform);
			this.m_DamageObject.Init (this.m_Data.maxResistant, this.m_Data.currentDurability);
			this.m_FSMComponent.Init (this);
			base.Init ();
		}

		#endregion

		#region Implementation Controller

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_BreakableObject);
			this.m_Components.Add (this.m_MovableObject);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_FSMComponent);
		}

		public override void UpdateObject (float dt)
		{
			base.UpdateObject (dt);
			this.ChasingTarget (dt);
		}

		protected virtual void ChasingTarget(float dt) {
			if (this.m_TargetController == null)
				return;
			this.m_MovableObject.SetDestination (this.m_TargetController.transform.position, this.m_TargetController.GetCollider());
			if (this.m_MovableObject.IsNearTarget () == false) {
				this.m_MovableObject.Move (dt);
			} else {
				this.m_MovableObject.LookForwardToTarget (this.m_TargetController.transform.position);
			}
		}

		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj)
		{
//			base.InteractiveOrtherObject (contactObj);
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << contactObj.gameObject.layer));
			if (isExceptionLayer == false) {
				var controller = contactObj.gameObject.GetObjectController<CObjectController> ();
				if (controller == null) {
					return;
				}
				if (controller.GetActive() == false) {
					return;
				}
				if (controller.GetVelocityKMH () > this.m_DamageObject.maxResistant) {
					var totalDamage = controller.GetVelocityKMH () - this.m_DamageObject.maxResistant + controller.GetDamage ();
					this.ApplyDamage (controller, totalDamage);
				}
			}
		}

		public override void ApplyDamage (CObjectController attacker, float value)
		{
			base.ApplyDamage (attacker, value);
			this.m_DamageObject.CalculteDamage (value);
			if (this.m_DamageObject.IsOutOfDamage () 
				&& this.m_AlreadyUpdateMission == false 
				&& attacker != null) {
				for (int i = 0; i < this.m_Data.objectTypes.Length; i++) {
					attacker.SetMissionObject (this.m_Data.objectTypes[i], 1);
				}
				this.m_AlreadyUpdateMission = true;
			}
		}

		#endregion

		#region FSM

		public override bool HaveEnemy() {
			return this.m_TargetController != null;
		}

		public override bool IsInactive() {
			return this.m_DamageObject.IsOutOfDamage ();
		}

		#endregion

		#region Getter && Setter

		public override void SetActive (bool value)
		{
			base.SetActive (value);
			this.m_BreakableObject.BreakObjects (!value);
		}

		public override void SetTarget(CObjectController target) {
			base.SetTarget (target);
		}

		public override CObjectController GetTarget ()
		{
			return base.GetTarget ();
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

		public override float GetResistant ()
		{
			base.GetResistant ();
			return this.m_Data.maxResistant;
		}

		#endregion
		
	}
}
