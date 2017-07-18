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
		public int goldAward;
		public string[] zombiePrefabs;
		public Dictionary<string, int> gameMissionInfo;

		public CGameModeData ()
		{
			this.gameModeName 			= string.Empty;
			this.gameModeFSM 			= string.Empty;
			this.gameModeModel 			= string.Empty;
			this.gameModeDescription 	= string.Empty;
			this.goldAward 				= 0;
		}
		
	}
}
