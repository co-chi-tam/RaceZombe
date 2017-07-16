using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CGameModeData {

		public string gameModeName;
		public string gameModeFSM;
		public string gameModeModel;
		public string gameModeDescription;
		public int goldAward;

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
