using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CCarPartData: CObjectData{

		#region Properties

		public CCarPartsComponent.ECarPart partType;
		public float engineWearValue;

		#endregion

		#region Contructor

		public CCarPartData (): base ()
		{
			this.partType 			= CCarPartsComponent.ECarPart.NONE;
			this.engineWearValue	= 0.1f;
		}

		#endregion

		#region Main thods

		public override string ToJSON ()
		{
			base.ToJSON ();
			this.m_DictJSON.Add ("partType", (int) this.partType);
			this.m_DictJSON.Add ("engineWearValue", this.engineWearValue);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			return json;
		}

		#endregion

	}
}