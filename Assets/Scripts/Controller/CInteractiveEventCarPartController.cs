using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	public class CInteractiveEventCarPartController : CInteractiveCarPartController {

		#region Properties

		public enum EInteractiveEvent: int {
			ApplyDamageTarget = 0,
			ChangeThisToTarget = 1,
			// Car part
			AddGasToTarget = 101,
			PullTarget = 102
		}

		[Header ("Events")]
		[SerializeField]	protected EInteractiveEvent m_CurrentEvent = EInteractiveEvent.ApplyDamageTarget;
		[SerializeField]	protected float m_FloatValue = 0f;
		[SerializeField]	protected int m_IntValue = 0;
		[SerializeField]	protected string m_StringValue = string.Empty;

		public UnityEvent OnEventTriggerEnter;
		public UnityEvent OnEventStart;

		protected Dictionary<EInteractiveEvent, Action<object>> m_InteractiveEvents;

		#endregion

		#region Implementation MonoBehaviour

		protected override void Start ()
		{
			base.Start ();
			this.m_InteractiveEvents = new Dictionary<EInteractiveEvent, Action<object>> ();
			this.m_InteractiveEvents.Add (EInteractiveEvent.ApplyDamageTarget, 	this.ApplyDamageTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.ChangeThisToTarget, this.ChangeThisToTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.AddGasToTarget, 	this.AddGasToTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.PullTarget, 		this.PullTarget);
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
			// Decrease Durability
			this.ApplyEngineWear (this.m_Data.engineWearValue);
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
				var force = direction.normalized * rigidBodyCtrl.mass * this.GetDamage ();
				rigidBodyCtrl.AddForce (force, ForceMode.Impulse);
				objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
			}
		}

		#endregion

	}
}
