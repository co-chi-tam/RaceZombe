using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	class CSelectCarSceneTask : CSimpleTask {

		#region Properties

		#endregion

		#region Constructor

		public CSelectCarSceneTask () : base ()
		{
			this.taskName = "SelectCarScene";
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
