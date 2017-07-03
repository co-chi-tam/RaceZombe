using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CGunPartController : CCarPartController {

		public override void InteractiveOrtherObject (GameObject contactObj)
		{
			base.InteractiveOrtherObject (contactObj);
			var controller = contactObj.GetObjectController<CBaseController> ();
			controller.ApplyDamage (this.m_Owner, this.GetDamage ());
		}
		
	}
}
