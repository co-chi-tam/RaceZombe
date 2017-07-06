using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CTimerComponent : CComponent {

		protected List<CTimerObject> m_TimerObjects;

		public override void Init ()
		{
			base.Init ();
			this.m_TimerObjects = new List<CTimerObject> ();
		}

		public override void UpdateComponent (float dt)
		{
			base.UpdateComponent (dt);
			for (int i = 0; i < this.m_TimerObjects.Count; i++) {
				var objTimer = this.m_TimerObjects [i];
				if (objTimer.callbackTime > 0f) {
					objTimer.callbackTime -= dt;
				} else {
					if (objTimer.eventCallback != null) {
						objTimer.eventCallback ();
					}
					this.m_TimerObjects.Remove (objTimer);
					i--;
				}
			}
		}

		public virtual void SetTimer(float time, Action eventCallback) {
			var timerObj = new CTimerObject ();
			timerObj.callbackTime = time;
			timerObj.eventCallback = eventCallback;
			this.m_TimerObjects.Add (timerObj);
		}

	}

	public class CTimerObject {
		public float callbackTime;
		public Action eventCallback;
	}
}
