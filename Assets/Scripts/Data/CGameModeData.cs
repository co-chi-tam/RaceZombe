using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CGameModeData {

		public string gameModeName;
		public string gameModeFSM;
		public string gameModeModel;
		public string gameModeDescription;
		public int killCount;
		public int goldAward;

		public CGameModeData ()
		{
			this.gameModeName 			= string.Empty;
			this.gameModeFSM 			= string.Empty;
			this.gameModeModel 			= string.Empty;
			this.gameModeDescription 	= string.Empty;
			this.killCount 				= 0;
			this.goldAward 				= 0;
		}
		
	}
}
