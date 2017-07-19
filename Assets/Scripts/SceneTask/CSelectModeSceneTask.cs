using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CSelectModeSceneTask : CSimpleTask {

		#region Properties

		private CUISelectMissionManager m_UISelectMissionManager;
		private CGameModeData[] m_GameModeDatas;

		#endregion

		#region Constructor

		public CSelectModeSceneTask () : base ()
		{
			this.taskName = "SelectModeScene";
			this.nextTask = "SelectCarScene";
			var missionA = TinyJSON.JSON.Load (Resources.Load<TextAsset>("Data/GameMission/AMission").text).Make<CGameModeData> ();
			var missionB = TinyJSON.JSON.Load (Resources.Load<TextAsset>("Data/GameMission/BMission").text).Make<CGameModeData> ();
			var missionC = TinyJSON.JSON.Load (Resources.Load<TextAsset>("Data/GameMission/CMission").text).Make<CGameModeData> ();
			var missionS = TinyJSON.JSON.Load (Resources.Load<TextAsset>("Data/GameMission/SMission").text).Make<CGameModeData> ();
			var missionSSS = TinyJSON.JSON.Load (Resources.Load<TextAsset>("Data/GameMission/SSSMission").text).Make<CGameModeData> ();
			this.m_GameModeDatas = new CGameModeData[] { 
				missionA,
				missionB,
				missionC, 
				missionS, 
				missionSSS
			};
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
			this.m_UISelectMissionManager = CUISelectMissionManager.GetInstance ();
			this.m_UISelectMissionManager.Init ();
			this.m_UISelectMissionManager.LoadMission (this.m_GameModeDatas);
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
		}

		#endregion
	}
}
