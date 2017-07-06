using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CCarData: CMovableData {

		public float gas;
		public float engineWear;
		public CCarPartData[] carParts;

		public CCarData ()
		{
			this.gas 		= 1000f;
			this.engineWear	= 1f;
		}

	}
}