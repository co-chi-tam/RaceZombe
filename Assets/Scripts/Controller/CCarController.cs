using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CObjectController {

		[Header ("Data")]
		[SerializeField]	private CCarData m_Data;

		[Header ("Components")]
		[SerializeField]	private CWheelDriver m_WheelDriver;
		[SerializeField]	private CDamageObject m_DamageObject;
		[SerializeField]	private CCarPartsComponent m_CarParts;

		protected override void Start() {
			this.m_WheelDriver.Init (this.m_Data.maxSpeed);
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
			this.m_CarParts.Init (m_Data.carParts);
			base.Start ();
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_CarParts);
		}

		public virtual void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			if (this.m_Data.gas > 0f) {
				this.m_WheelDriver.UpdateDriver (angleInput, torqueInput, brakeInput);
				var currentGas = this.GetGas ();
				var consumeGas = this.GetVelocityKMH () / this.m_Data.maxSpeed;
				currentGas -= consumeGas;
				this.SetGas (currentGas);
			} else {
				this.m_WheelDriver.UpdateDriver (0f, 0f, true);
			}
		}

		public virtual void UpdateCarPart(CCarPartsComponent.ECarPart part, GameObject contact) {
			var carPartCtrl = this.m_CarParts.GetCarPart (part);
			carPartCtrl.InteractiveOrtherObject (contact);
			carPartCtrl.SetAnimator ("Active");
		}

		public override void ApplyDamage (CObjectController attacker, float value) {
			base.ApplyDamage (attacker, value);
		}

		public override float GetDamage () {
			return this.m_Data.currentDamage;
		}

		public override float GetVelocityKMH () {
			return this.m_WheelDriver.GetVelocityKMH ();
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CCarData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CCarData;
		}

		public virtual float GetGas() {
			return this.m_Data.gas;
		}

		public virtual void SetGas(float value) {
			this.m_Data.gas = value;
		}

	}
}
