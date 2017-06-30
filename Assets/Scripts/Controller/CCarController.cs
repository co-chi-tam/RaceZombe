using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CCarController: CBaseController {

		[Header ("Components")]
		[SerializeField]	private CWheelDriver m_WheelDriver;

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_WheelDriver);
		}

		protected override void UpdateComponents(float dt) {
			base.UpdateComponents (dt);
		}

		public void UpdateDriver(float angleInput, float torqueInput, bool brakeInput) {
			this.m_WheelDriver.UpdateDriver (angleInput, torqueInput, brakeInput);
		}

	}
}
