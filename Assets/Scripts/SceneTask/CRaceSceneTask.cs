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
			this.nextTask = "SelectModeScene";
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
			this.m_IsCompleteTask = false;
			this.m_IsLoadingTask = false;
			this.m_GameManager = CGameManager.GetInstance ();
			this.m_GameManager.StartRace (() => {
				this.m_IsCompleteTask = true;
				this.m_IsLoadingTask = true;
			});
		}

		#endregion

	}
}
