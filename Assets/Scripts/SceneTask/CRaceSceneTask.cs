using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CRaceSceneTask : CSimpleTask {

		#region Properties

		private CGameManager m_GameManager;

		#endregion

		#region Constructor

		public CRaceSceneTask () : base ()
		{
			this.taskName = "RaceScene";
			this.nextTask = "IntroScene";
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
			this.m_IsCompleteTask = false;
			this.m_GameManager = CGameManager.GetInstance ();
			this.m_GameManager.StartRace (() => {
				this.m_IsCompleteTask = true;
			});
		}

		#endregion

	}
}
