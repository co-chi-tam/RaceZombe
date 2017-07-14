using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CSelectModeSceneTask : CSimpleTask {

		#region Properties

		private float m_Timer = 5f;

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
			this.m_Timer = 5f;
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
			if (this.m_IsCompleteTask == false) {
				this.m_Timer -= dt;
				this.m_IsCompleteTask = this.m_Timer < 0f;
			} else {
				if (this.OnCompleteTask != null) {
					this.OnCompleteTask ();
				}
			}
		}

		#endregion
	}
}
