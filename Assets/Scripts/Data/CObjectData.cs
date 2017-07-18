using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CObjectData {

		#region Properties

		public string objectName;
		public string objectModelPath;
		public string objectAvatarPath;
		public string[] objectTypes;
		public float maxResistant;
		public float currentDamage;
		public float currentDurability;
		public float maxDurability;
		public float actionDelay;

		protected Dictionary<string, object> m_DictJSON;

		#endregion

		#region Contructor

		public CObjectData ()
		{
			this.objectName			= string.Empty;
			this.objectModelPath	= string.Empty;
			this.objectAvatarPath	= string.Empty;
			this.maxResistant 		= 2f;
			this.currentDamage 		= 3f;
			this.currentDurability 	= 100f;
			this.maxDurability 		= 100f;
			this.actionDelay 		= 0.1f;

			this.m_DictJSON = new Dictionary<string, object> ();
		}

		#endregion

		#region Main methods

		public virtual string ToJSON ()
		{
			this.m_DictJSON.Add ("objectName", this.objectName);
			this.m_DictJSON.Add ("objectModelPath", this.objectModelPath);
			this.m_DictJSON.Add ("objectAvatarPath", this.objectAvatarPath);
			var objectTypeJSON = "[";
			for (int i = 0; i < this.objectTypes.Length; i++) {
				objectTypeJSON += this.objectTypes[i].ToString() + (i < this.objectTypes.Length - 1 ? "," : "");
			}
			objectTypeJSON += "],";
			this.m_DictJSON.Add ("objectTypes", objectTypeJSON);
			this.m_DictJSON.Add ("currentResistant", this.maxResistant);
			this.m_DictJSON.Add ("currentDamage", this.currentDamage);
			this.m_DictJSON.Add ("currentDurability", this.currentDurability);
			this.m_DictJSON.Add ("maxDurability", this.maxDurability);
			this.m_DictJSON.Add ("actionDelay", this.actionDelay);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			json = json.Replace ("\\", "");
			json = json.Replace ("\"[", "[");
			json = json.Replace ("]\"", "]");
			return json;
		}

		#endregion
		
	}
}
