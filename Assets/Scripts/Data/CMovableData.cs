using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CMovableData: CObjectData {

		public float maxSpeed;

		public CMovableData ()
		{
			this.maxSpeed 		= 3.5f;
		}

	}
}
