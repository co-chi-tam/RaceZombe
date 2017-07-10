using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CInteractiveEventCarPartController : CInteractiveCarPartController {

		public enum EInteractiveEvent: int {
			ApplyDamageTarget = 0,
			ChangeThisToTarget = 1
		}

		[Header ("Events")]
		[SerializeField]	protected EInteractiveEvent m_CurrentEvent = EInteractiveEvent.ApplyDamageTarget;

		protected Dictionary<EInteractiveEvent, Action<object>> m_InteractiveEvents;

		protected override void Start ()
		{
			base.Start ();
			this.m_InteractiveEvents = new Dictionary<EInteractiveEvent, Action<object>> ();
			this.m_InteractiveEvents.Add (EInteractiveEvent.ApplyDamageTarget, ApplyDamageTarget);
			this.m_InteractiveEvents.Add (EInteractiveEvent.ChangeThisToTarget, ChangeThisToTarget);
		}

		protected override void InteractiveTarget(CObjectController target) {
//			base.InteractiveTarget (target);
			this.m_InteractiveEvents[this.m_CurrentEvent](target);
		}

		protected virtual void ApplyDamageTarget(object target) {
			var objCtrl = target as CObjectController;
			objCtrl.ApplyDamage (this.m_Owner, this.GetDamage ());
		}

		protected virtual void ChangeThisToTarget(object target) {
			var objCtrl = target as CObjectController;
			objCtrl.SetTarget (this);
		}

	}
}
