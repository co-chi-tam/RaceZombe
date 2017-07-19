using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CGameModeData {

		public string gameModeName;
		public string gameModeFSM;
		public string gameModeModel;
		public string gameModeDescription;
		public string gameModeHardPoint;
		public CMovableData[] zombieDatas;
		public CCarData[] enemyDatas;
		public Dictionary<string, float> gameMissionReware;
		public Dictionary<string, float> gameMissionInfo;

		public CGameModeData ()
		{
			this.gameModeName 			= string.Empty;
			this.gameModeFSM 			= string.Empty;
			this.gameModeModel 			= string.Empty;
			this.gameModeDescription 	= string.Empty;
			this.gameModeHardPoint 		= string.Empty;
		}
		
	}
}
