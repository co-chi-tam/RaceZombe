using System;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CMovableData: CObjectData {

		#region Properties
		
		public float maxSpeed;

		#endregion

		#region Contructor

		public CMovableData ()
		{
			this.maxSpeed 		= 3.5f;
		}

		#endregion

		#region Main methods

		public override string ToJSON ()
		{
			base.ToJSON ();
			this.m_DictJSON.Add ("maxSpeed", this.maxSpeed);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			return json;
		}

		#endregion

	}
}
