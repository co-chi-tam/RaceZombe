using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CCarPartData: CObjectData{
		
		public CCarPartsComponent.ECarPart partType;

		public CCarPartData (): base ()
		{
			this.partType 		= CCarPartsComponent.ECarPart.NONE;
		}

	}
}