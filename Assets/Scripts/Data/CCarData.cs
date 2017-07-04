using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CCarData: CMovableData {

		public float gas;
		public CGunPartData[] carParts;

		public CCarData ()
		{
			this.gas = 1000f;
		}

	}
}