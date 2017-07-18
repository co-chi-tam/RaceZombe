using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CSelectModeSceneTask : CSimpleTask {

		#region Properties

		#endregion

		#region Constructor

		public CSelectModeSceneTask () : base ()
		{
			this.taskName = "SelectModeScene";
			this.nextTask = "RaceScene";
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
		}

		#endregion
	}
}
