using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CObjectData {

		public string objectName;
		public string objectModelPath;
		public string objectAvatarPath;
		public float currentResistant;
		public float currentDamage;
		public float currentDurability;
		public float maxDurability;
		public float actionDelay;

		public CObjectData ()
		{
			this.objectName			= string.Empty;
			this.objectModelPath	= string.Empty;
			this.objectAvatarPath	= string.Empty;
			this.currentResistant 	= 2f;
			this.currentDamage 		= 3f;
			this.currentDurability 	= 100f;
			this.maxDurability 		= 100f;
			this.actionDelay 		= 0.1f;
		}
		
	}
}
