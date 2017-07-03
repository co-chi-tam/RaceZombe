using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CBaseController {

		[Header ("Data")]
		[SerializeField]	private CMovableData m_Data;

		[Header ("Components")]
		[SerializeField]	private CWheelDriver m_WheelDriver;
		[SerializeField]	private CDamageObject m_DamageObject;
		[SerializeField]	private CCarPartsComponent m_CarParts;

		protected override void Start() {
			base.Start ();
			this.m_WheelDriver.Init (this.m_Data.maxSpeed);
			this.m_DamageObject.Init (this.m_Data.currentResistant, this.m_Data.currentDurability);
			this.m_CarParts.Init (null, 
									new CCarPartData() {
										objectName = "FrontBlade",
										partType = CCarPartsComponent.ECarPart.FRONT,
										currentDamage = 5,
										currentResistant = 2,
										currentDurability = 150f
									},
				                  null);
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_CarParts);
		}

		protected override void UpdateComponents(float dt) {
			base.UpdateComponents (dt);
		}

		public void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			this.m_WheelDriver.UpdateDriver (angleInput, torqueInput, brakeInput);
		}

		protected override void ApplyDamage (CBaseController attacker, float value) {
			base.ApplyDamage (attacker, value);
		}

		public override float GetDamage () {
			return this.m_Data.currentDamage;
		}

		public override float GetVelocityKMH () {
			return this.m_WheelDriver.GetVelocityKMH ();
		}

		public override void SetData(CObjectData value) {
			this.m_Data = value as CMovableData;
		}

		public override CObjectData GetData() {
			return this.m_Data as CMovableData;
		}

	}
}
