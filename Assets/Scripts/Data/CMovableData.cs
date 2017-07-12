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

		public override string ToJSON ()
		{
			base.ToJSON ();
			this.m_DictJSON.Add ("maxSpeed", this.maxSpeed);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			return json;
		}

	}
}
