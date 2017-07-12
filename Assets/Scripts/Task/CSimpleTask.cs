using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RacingHuntZombie {
	public class CSimpleTask : CTask {

		#region Constructor

		public CSimpleTask () : base ()
		{
			this.taskName = string.Empty;
			this.nextTask = string.Empty;
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			this.m_IsCompleteTask = false;
			this.m_IsLoadingTask = true;
		}

		#endregion
		
	}
}
