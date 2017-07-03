﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarPartController : CBaseController {

		[Header ("Data")]
		[SerializeField]	protected CCarPartData m_Data;
		[SerializeField]	protected CCarController m_Owner;

		[Header ("Component")]
		[SerializeField]	protected CDamageObject m_DamageObject;

		protected override void Start() {
			base.Start ();
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
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
				var controller = contactObj.GetObjectController<CBaseController> ();
				if (controller == null)
					return;
				if (controller.GetActive() == false)
					return;
				// Decrease Durability
				this.m_DamageObject.CalculteDamage (controller.GetDamage () - this.m_DamageObject.maxResistant);
			}
		}

		public override float GetDamage () {
			return this.m_Data.currentDamage;
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CCarPartData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CCarPartData;
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