using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

		public override string ToJSON ()
		{
			base.ToJSON ();
			this.m_DictJSON.Add ("gas", this.gas);
			this.m_DictJSON.Add ("engineWear", this.engineWear);
			var carPartsJSON = "[";
			for (int i = 0; i < this.carParts.Length; i++) {
				carPartsJSON += this.carParts[i].ToJSON() + (i < this.carParts.Length - 1 ? "," : "");
			}
			carPartsJSON += "]";
			this.m_DictJSON.Add ("carParts", carPartsJSON);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			json = json.Replace ("\\", "");
			json = json.Replace ("\"[", "[");
			json = json.Replace ("]\"", "]");
			return json;
		}

	}
}