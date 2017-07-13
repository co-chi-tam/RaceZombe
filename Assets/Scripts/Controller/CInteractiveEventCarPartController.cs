using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CInteractiveEventCarPartController : CInteractiveCarPartController {

		public enum EInteractiveEvent: int {
			ApplyDamageTarget = 0,
			ChangeThisToTarget = 1,
			// Car part
			AddGasToTarget = 101
		}

		[Header ("Events")]
		[SerializeField]	protected EInteractiveEvent m_CurrentEvent = EInteractiveEvent.ApplyDamageTarget;
		[SerializeField]	protected float m_FloatValue = 0f;
		[SerializeField]	protected int m_IntValue = 0;
		[SerializeField]	protected string m_StringValue = string.Empty;

		protected Dictionary<EInteractiveEvent, Action<object>> m_InteractiveEvents;

		protected override void Start ()
		{
			base.Start ();
			this.m_InteractiveEvents = new Dictionary<EInteractiveEvent, Action<object>> ();
			this.m_InteractiveEvents.Add (EInteractiveEvent.ApplyDamageTarget, 	this.ApplyDamageTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.ChangeThisToTarget, this.ChangeThisToTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.AddGasToTarget, 	this.AddGasToTarget);
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

	}
}
