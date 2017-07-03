using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CObjectData {

		public string objectName;
		public float currentResistant;
		public float currentDamage;
		public float currentDurability;

		public CObjectData ()
		{
			this.objectName			= string.Empty;
			this.currentResistant 	= 2f;
			this.currentDamage 		= 3f;
			this.currentDurability 	= 100f;
		}
		
	}
}
