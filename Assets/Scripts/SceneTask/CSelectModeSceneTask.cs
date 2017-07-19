using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CSelectModeSceneTask : CSimpleTask {

		#region Properties

		private CUISelectMissionManager m_UISelectMissionManager;

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
			this.m_UISelectMissionManager = CUISelectMissionManager.GetInstance ();
			this.m_UISelectMissionManager.Init ();
			this.m_UISelectMissionManager.LoadMission (new CGameModeData () { gameModeName = "Mission A - Killing free", gameModeHardPoint = "A", gameModeDescription = "Kill normal zombie x5" }
				, new CGameModeData () { gameModeName = "Mission B - Killing free", gameModeHardPoint = "B", gameModeDescription = "Kill normal zombie x5" }
				, new CGameModeData () { gameModeName = "Mission C - Killing free", gameModeHardPoint = "C", gameModeDescription = "Kill normal zombie x5" }
				, new CGameModeData () { gameModeName = "Mission S - Killing free", gameModeHardPoint = "S", gameModeDescription = "Kill normal zombie x5" }
				, new CGameModeData () { gameModeName = "Mission SSS - Killing free", gameModeHardPoint = "SSS", gameModeDescription = "Kill normal zombie x5" });
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
		}

		#endregion
	}
}
