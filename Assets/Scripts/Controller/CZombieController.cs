using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieController : CObjectController {

		[Header ("Data")]
		[SerializeField]	protected CMovableData m_Data;

		[Header ("Component")]
		[SerializeField]	protected CBreakableObject m_BreakableObject;
		[SerializeField]	protected CMovableObject m_MovableObject;
		[SerializeField]	protected CDamageObject m_DamageObject;
		[SerializeField]	protected CFSMComponent m_FSMComponent;


		protected override void Start() {
			this.m_BreakableObject.Init (false);
			this.m_MovableObject.Init (this.m_Data.maxSpeed, this.m_Transform);
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
			this.m_FSMComponent.Init (this);
			base.Start ();
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_BreakableObject);
			this.m_Components.Add (this.m_MovableObject);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_FSMComponent);
		}

		public override void ChasingTarget(float dt) {
			base.ChasingTarget (dt);
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
				var controller = contactObj.gameObject.GetObjectController<CObjectController> ();
				if (controller == null) {
					return;
				}
				if (controller.GetActive() == false) {
					return;
				}
				this.ApplyDamage (controller, controller.GetDamage () - this.m_DamageObject.maxResistant);
			}
		}

		public override void ApplyDamage (CObjectController attacker, float value)
		{
			base.ApplyDamage (attacker, value);
			this.m_DamageObject.CalculteDamage (value);
		}

		public override bool HaveEnemy() {
			base.HaveEnemy ();
			return this.m_TargetController != null;
		}

		public override bool IsDeath() {
			base.IsDeath ();
			return this.m_DamageObject.IsOutOfDamage ();
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
		
	}
}
