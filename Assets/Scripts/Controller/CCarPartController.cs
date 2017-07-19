using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarPartController : CObjectController {

		#region Properties

		[Header("Data")]
		[SerializeField]	protected CCarPartData m_Data;
		[Header ("Owner")]
		[SerializeField]	protected CCarController m_Owner;

		[Header ("Component")]
		[SerializeField]	protected CDamageObject m_DamageObject;

		#endregion

		#region Implementation MonoBehaviour

		public override void Init() {
			this.m_DamageObject.Init (this.m_Data.maxResistant, this.m_Data.currentDurability);
			base.Init ();
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.m_DamageObject.IsOutOfDamage ()) {
				this.DestroyObject ();
			}
		}

		#endregion

		#region Implementation Controller

		protected override void RegisterComponent () {
			base.RegisterComponent ();
			this.m_Components.Add (this.m_DamageObject);
		}

		public override void ApplyEngineWear (float wear)
		{
			base.ApplyEngineWear (wear);
			this.m_DamageObject.CalculteDamage (wear);
		}

		public override void ApplyDamage (CObjectController attacker, float damage)
		{
			base.ApplyDamage (attacker, damage);
			var totalDamage = damage - this.m_DamageObject.maxResistant; 
			this.m_DamageObject.CalculteDamage (totalDamage);
		}

		#endregion

		#region Getter && Setter

		public override float GetDamage () {
			return this.m_Data.currentDamage;
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CCarPartData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CCarPartData;
		}

		public override float GetDurability () {
			return this.m_DamageObject.currentDamage;
		}

		public override float GetDurabilityPercent () {
			return 1f - (this.m_DamageObject.currentDamage / this.m_DamageObject.maxDamage);
		}

		public override float GetVelocityKMH () {
			if (this.m_Owner == null)
				return 0f;
			return this.m_Owner.GetVelocityKMH ();
		}

		public override float GetResistant ()
		{
			base.GetResistant ();
			return this.m_Data.maxResistant;
		}

		public virtual void SetOwner(CCarController value) {
			this.m_Owner = value;
		}

		public virtual CCarController GetOwner() {
			return this.m_Owner;
		}

		public override void SetMissionObject (string key, float value)
		{
			if (this.m_Owner == null)
				return;
			this.m_Owner.SetMissionObject (key, value);
		}

		#endregion

	}
}
