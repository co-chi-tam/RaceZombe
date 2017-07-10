using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CInteractiveEventCarPartController : CInteractiveCarPartController {

		[Header ("Events")]
		[SerializeField]	protected string m_CurrentEvent = "ApplyDamageTarget";

		protected Dictionary<string, Action<object>> m_InteractiveEvents;

		protected override void Start ()
		{
			base.Start ();
			this.m_InteractiveEvents = new Dictionary<string, Action<object>> ();
			this.m_InteractiveEvents.Add ("ApplyDamageTarget", ApplyDamageTarget);
			this.m_InteractiveEvents.Add ("ChangeThisToTarget", ChangeThisToTarget);
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
