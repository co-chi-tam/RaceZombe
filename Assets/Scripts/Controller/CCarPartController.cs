using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarPartController : CBaseController {

		[Header ("Data")]
		[SerializeField]	private CCarPartData m_Data;
		[SerializeField]	private CCarController m_Owner;

		[Header ("Component")]
		[SerializeField]	private CDamageObject m_DamageObject;

		protected override void Start() {
			base.Start ();
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_DamageObject);
		}

		protected override void OnTriggerEnter (Collider collider)
		{
			base.OnTriggerEnter (collider);
			if (collider.gameObject.layer != LayerMask.NameToLayer ("Ground")) {
				var controller = collider.gameObject.GetColliderController<CBaseController> ();
				if (controller == null)
					return;
				if (controller.GetActive() == false)
					return;
				this.m_DamageObject.CalculteDamage (1f);
			}
		}

		public override float GetDamage ()
		{
			return this.m_Data.currentDamage;
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CCarPartData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CCarPartData;
		}

		public virtual void SetOwner(CCarController value) {
			this.m_Owner = value;
		}

		public virtual CCarController GetOwner() {
			return this.m_Owner;
		}

	}
}
