using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CObjectController {

		[Header ("Data")]
		[SerializeField]	private CCarData m_Data;

		[Header ("Components")]
		[SerializeField]	protected CWheelDriver m_WheelDriver;
		[SerializeField]	protected CDamageObject m_DamageObject;
		[SerializeField]	protected CCarPartsComponent m_CarParts;
		[SerializeField]	protected CFSMComponent m_FSMComponent;

		protected override void Start() {
			this.m_WheelDriver.Init (this.m_Data.maxSpeed);
			this.m_DamageObject.Init (this.m_Data.maxResistant, this.m_Data.currentDurability);
			this.m_CarParts.Init (m_Data.carParts);
			this.m_FSMComponent.Init (this);
			base.Start ();
		}

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_CarParts);
			this.m_Components.Add (this.m_FSMComponent);
		}

		public virtual void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			if (this.m_Data.currentGas > 0f) {
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
			carPartCtrl.InteractiveOrtherObject (this.gameObject, contact);
			carPartCtrl.SetAnimator ("Active");
		}

		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj) {
//			base.InteractiveOrtherObject (thisContantObj, contactObj);
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << contactObj.gameObject.layer));
			if (isExceptionLayer == false) {
				var contactCtrl = contactObj.GetObjectController<CObjectController> ();
				if (contactCtrl == null) {
					return;
				}
				if (contactCtrl.GetActive() == false) {
					return;
				}
				var thisContactCtrl = thisContantObj.GetObjectController<CObjectController> ();
				if (thisContactCtrl != null) {
					thisContactCtrl.ApplyDamage (contactCtrl, contactCtrl.GetDamage () - thisContactCtrl.GetResistant());
				} else {
					this.ApplyDamage (contactCtrl, contactCtrl.GetDamage () - this.GetResistant());
				}
			}
		}

		public override void ApplyDamage (CObjectController attacker, float value) {
			base.ApplyDamage (attacker, value);
			this.m_DamageObject.CalculteDamage (value - this.m_DamageObject.maxResistant);
		}

		protected override void OnTriggerStay (Collider collider)
		{
			base.OnTriggerStay (collider);
			var isGround = collider.gameObject.layer == LayerMask.NameToLayer ("Ground");
			if (isGround == true) {
				this.ApplyDamage (null, Time.deltaTime);
			}
		}

		public override bool HaveGas () {
			return this.GetGas () > 0f;
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

		public override float GetResistant ()
		{
			base.GetResistant ();
			return this.m_Data.maxResistant;
		}

		public override float GetGas() {
			return this.m_Data.currentGas;
		}

		public override void SetGas(float value) {
			this.m_Data.currentGas = value;
		}

	}
}
