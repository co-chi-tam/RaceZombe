using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	public class CInteractiveEventCarPartController : CInteractiveCarPartController {

		#region Properties

		public enum EInteractiveEvent: int {
			None 				= 0,
			UpdateMission		= 1,
			ApplyDamageTarget 	= 2,
			ChangeThisToTarget 	= 3,
			// Car part
			AddGasToTarget 		= 101,
			PullTarget 			= 102,
			PushTarget 			= 103
		}

		[Header ("Events")]
		[SerializeField]	protected EInteractiveEvent m_CurrentEvent = EInteractiveEvent.ApplyDamageTarget;
		[SerializeField]	protected float m_FloatValue = 1f;
		[SerializeField]	protected int m_IntValue = 1;
		[SerializeField]	protected string m_StringValue = string.Empty;

		public UnityEvent OnEventTriggerEnter;
		public UnityEvent OnEventStart;

		protected Dictionary<EInteractiveEvent, Action<object>> m_InteractiveEvents;

		#endregion

		#region Implementation MonoBehaviour

		protected override void Awake ()
		{
			base.Awake ();
			this.m_InteractiveEvents = new Dictionary<EInteractiveEvent, Action<object>> ();
			this.m_InteractiveEvents.Add (EInteractiveEvent.None, 				this.None);
			this.m_InteractiveEvents.Add (EInteractiveEvent.UpdateMission,		this.UpdateMission);
			this.m_InteractiveEvents.Add (EInteractiveEvent.ApplyDamageTarget, 	this.ApplyDamageTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.ChangeThisToTarget, this.ChangeThisToTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.AddGasToTarget, 	this.AddGasToTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.PullTarget, 		this.PullTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.PushTarget, 		this.PushTarget);
		}

		protected override void OnTriggerEnter (Collider collider)
		{
			base.OnTriggerEnter (collider);
			if (this.m_AutoInteractive == false) {
				if (this.OnEventTriggerEnter != null) {
					this.OnEventTriggerEnter.Invoke ();
				}
			}
		}

		#endregion

		#region Implementation Controller

		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj)
		{
			base.InteractiveOrtherObject (thisContantObj, contactObj);
			if (this.m_HitBoxContacts.Count > 0) {
				if (this.OnEventStart != null) {
					this.OnEventStart.Invoke ();
				}
			}
			// Decrease Durability
			this.ApplyEngineWear (this.m_Data.engineWearValue);
		}

		protected override void InteractiveTarget(CObjectController target) {
//			base.InteractiveTarget (target);
			if (this.m_RepeatAction == true) {
				this.m_InteractiveEvents [this.m_CurrentEvent] (target);
			} else {
				this.m_InteractiveEvents [this.m_CurrentEvent] (target);
				this.m_CurrentActionDelay = this.m_DestroyAfter;
				this.DestroyObject ();
			}
		}

		protected virtual void None(object target) {
			
		}

		protected virtual void UpdateMission(object target) {
			var objCtrl = target as CObjectController;
			for (int i = 0; i < this.m_Data.objectTypes.Length; i++) {
				objCtrl.SetMissionObject (this.m_Data.objectTypes[i], 1);
			}
		}

		protected virtual void ApplyDamageTarget(object target) {
			var objCtrl = target as CObjectController;
			objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
		}

		protected virtual void ChangeThisToTarget(object target) {
			var objCtrl = target as CObjectController;
			objCtrl.SetTarget (this);
		}

		protected virtual void AddGasToTarget(object target) {
			var objCtrl = target as CObjectController;
			var currentGas = objCtrl.GetGas () + this.m_FloatValue;
			objCtrl.SetGas (currentGas);
		}

		protected virtual void PullTarget(object target) {
			var objCtrl = target as CObjectController;
			var rigidBodyCtrl = objCtrl.GetComponent<Rigidbody> ();
			if (rigidBodyCtrl != null) {
				var direction = this.m_Transform.position - rigidBodyCtrl.position;
				var force = direction.normalized * rigidBodyCtrl.mass * this.GetDamage () * this.m_FloatValue;
				rigidBodyCtrl.AddForce (force, ForceMode.Impulse);
				objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
			}
		}

		protected virtual void PushTarget(object target) {
			var objCtrl = target as CObjectController;
			var rigidBodyCtrl = objCtrl.GetComponent<Rigidbody> ();
			if (rigidBodyCtrl != null) {
				var direction = rigidBodyCtrl.position - this.m_Transform.position;
				var force = direction.normalized * rigidBodyCtrl.mass * this.GetDamage () * this.m_FloatValue;
				rigidBodyCtrl.AddForce (force, ForceMode.Impulse);
				objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
			}
		}

		#endregion

	}
}
