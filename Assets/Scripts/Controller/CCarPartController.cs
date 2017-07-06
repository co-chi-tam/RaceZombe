﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarPartController : CObjectController {

		[Header("Data")]
		[SerializeField]	protected CGunPartData m_Data;
		[Header ("Owner")]
		[SerializeField]	protected CCarController m_Owner;

		[Header ("Component")]
		[SerializeField]	protected CDamageObject m_DamageObject;

		protected override void Start() {
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
			base.Start ();
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.GetActive() && this.m_DamageObject.IsOutOfDamage ()) {
				this.SetActive (false);
				this.gameObject.SetActive (false);
			}
		}

		protected override void RegisterComponent () {
			base.RegisterComponent ();
			this.m_Components.Add (this.m_DamageObject);
		}

		public override void InteractiveOrtherObject (GameObject contactObj) {
			base.InteractiveOrtherObject (contactObj);
			if (contactObj.layer != LayerMask.NameToLayer ("Ground")) {
				var controller = contactObj.GetObjectController<CObjectController> ();
				if (controller == null)
					return;
				if (controller.GetActive() == false)
					return;
				// Decrease Durability
				this.ApplyEngineWear (controller.GetDamage () - this.m_DamageObject.maxResistant);
			}
		}

		public override void ApplyEngineWear (float wear)
		{
			base.ApplyEngineWear (wear);
			this.m_DamageObject.CalculteDamage (wear);
		}

		public override float GetDamage () {
			return this.m_Data.currentDamage;
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CGunPartData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CGunPartData;
		}

		public override float GetVelocityKMH () {
			if (this.m_Owner == null)
				return 0f;
			return this.m_Owner.GetVelocityKMH ();
		}

		public virtual void SetOwner(CCarController value) {
			this.m_Owner = value;
		}

		public virtual CCarController GetOwner() {
			return this.m_Owner;
		}

	}
}
