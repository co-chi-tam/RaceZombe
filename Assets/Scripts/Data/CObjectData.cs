using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CObjectData {

		public float currentDefend;
		public float currentAttack;
		public float currentHealth;
		public float maxSpeed;

		public CObjectData ()
		{
			this.currentDefend 	= 02f;
			this.currentAttack 	= 3f;
			this.currentHealth 	= 100f;
			this.maxSpeed 		= 3.5f;
		}
		
	}
}
