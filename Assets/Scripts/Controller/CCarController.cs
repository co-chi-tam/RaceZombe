using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CObjectController {

		#region Properties

		[Header ("Data")]
		[SerializeField]	protected bool m_IsBot;
		[SerializeField]	protected CCarData m_Data;

		[Header ("Components")]
		[SerializeField]	protected CWheelDriver m_WheelDriver;
		[SerializeField]	protected CDamageObject m_DamageObject;
		[SerializeField]	protected CCarPartsComponent m_CarParts;
		[SerializeField]	protected CFSMComponent m_FSMComponent;
		[SerializeField]	protected CMissionComponent m_MissionComponent;

		public bool IsBot {
			get { return this.m_IsBot; }
			set { this.m_IsBot = value; }
		}

		#endregion

		#region Implementation MonoBehaviour

		public override void Init() {
			this.m_WheelDriver.Init (this.m_Data.maxSpeed);
			this.m_DamageObject.Init (this.m_Data.maxResistant, this.m_Data.currentDurability);
			this.m_CarParts.Init (m_Data.carParts);
			this.m_FSMComponent.Init (this);
			this.m_MissionComponent.Init ();
			base.Init ();
		}

		protected override void OnTriggerStay (Collider collider)
		{
			base.OnTriggerStay (collider);
			var isGround = collider.gameObject.layer == LayerMask.NameToLayer ("Ground");
			if (isGround == true) {
				this.ApplyDamage (null, Time.deltaTime);
			}
		}

		#endregion

		#region Main methods

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
			this.m_Components.Add (this.m_DamageObject);
			this.m_Components.Add (this.m_CarParts);
			this.m_Components.Add (this.m_FSMComponent);
			this.m_Components.Add (this.m_MissionComponent);
		}

		public override void UpdateObject (float dt)
		{
			base.UpdateObject (dt);
			this.ChaseTarget (dt);
		}

		protected virtual void ChaseTarget(float dt) {
			if (this.m_TargetController == null)
				return;	
			var targetPosition = this.m_TargetController.transform.position;
			var direction = targetPosition - this.m_Transform.position;
			var angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			var angleAxis = Quaternion.AngleAxis (angle, Vector3.up).eulerAngles;
			var currentAngle = this.m_Transform.rotation.eulerAngles.y;
			var deltaAngle = Mathf.DeltaAngle (currentAngle, angleAxis.y) / 180f;
			var speed = 1f;
			RaycastHit hitInfo;
			var forward = this.m_Transform.forward;
			forward.y = this.m_Transform.position.y;
			if (Physics.Raycast (this.m_Transform.position, forward, out hitInfo, 10f)) {
				var isExceptionLayer = this.m_ExcepLayerMask.value 
					== (this.m_ExcepLayerMask.value | (1 << hitInfo.collider.gameObject.layer));
				if (isExceptionLayer == true) {
					speed = -1f;
				}
			}
			this.UpdateDriver (deltaAngle * speed, speed, false);
		}

		public virtual void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			if (this.GetGas() > 0f) {
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
			this.SetMissionObject ("CarDurability", this.GetDurability ());
		}

		#endregion

		#region FSM 

		public override bool HaveGas () {
			return this.GetGas () > 0f;
		}

		public override bool IsInactive ()
		{
			return this.m_DamageObject.IsOutOfDamage() || this.GetGas () <= 0f;
		}

		#endregion

		#region Getter && Setter

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
			if (this.m_IsBot) {
				return this.m_Data.maxGas;
			} else {
#if TEST_MODE
				return this.m_Data.maxGas;
#else
				return this.m_Data.currentGas;
#endif
			}
		}

		public override void SetGas(float value) {
			this.m_Data.currentGas = value < 0f ? 0f : 
				value >= this.m_Data.maxGas ? this.m_Data.maxGas : value;
		}

		public override float GetGasPercent() {
			var gasPercent = this.m_Data.currentGas / this.m_Data.maxGas;
			return gasPercent <= 0f ? 0f : gasPercent;
			this.SetMissionObject ("CarGas", gasPercent);
		}

		public override float GetDurability () {
			return this.m_DamageObject.currentDamage;
//			return this.m_Data.currentDurability;
		}

		public override void SetDurability (float value) {
			this.m_Data.currentDurability = value < 0f ? 0f : 
				value >= this.m_Data.maxDurability ? this.m_Data.maxDurability : value;
		}

		public override float GetDurabilityPercent () {
			var durabilityPercent = 1f - (this.m_DamageObject.currentDamage / this.m_DamageObject.maxDamage);
			return durabilityPercent <= 0f ? 0f : durabilityPercent;
		}

		public override float GetCarPartPercent(CCarPartsComponent.ECarPart value) {
			var carPartCtrl = this.m_CarParts.GetCarPart (value);
			return carPartCtrl.GetDurabilityPercent();
		}

		public override void SetMissionObject (string key, float value) {
			this.m_MissionComponent.SetMissionObject (key, value);
		}

		public virtual bool GetMissionObject(Dictionary<string, float> mission) {
			return this.m_MissionComponent.IsMissionComplete (mission);
		}

		public override float GetMissionPercent () {
			return this.m_MissionComponent.GetMissionPercent ();
		}

		#endregion

	}
}
