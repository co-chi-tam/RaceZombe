using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CCarPartData: CObjectData{
		
		public CCarPartsComponent.ECarPart partType;
		public float engineWearValue;

		public CCarPartData (): base ()
		{
			this.partType 			= CCarPartsComponent.ECarPart.NONE;
			this.engineWearValue	= 0.1f;
		}

	}
}