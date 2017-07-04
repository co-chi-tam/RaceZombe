using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CGunPartData: CCarPartData{

		public float activeDelay;

		public CGunPartData (): base ()
		{
			this.activeDelay = 0.1f;
		}

	}
}