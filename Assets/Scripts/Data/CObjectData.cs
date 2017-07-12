using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {	
	[Serializable]
	public class CObjectData {

		public string objectName;
		public string objectModelPath;
		public string objectAvatarPath;
		public float currentResistant;
		public float currentDamage;
		public float currentDurability;
		public float maxDurability;
		public float actionDelay;

		protected Dictionary<string, object> m_DictJSON;

		public CObjectData ()
		{
			this.objectName			= string.Empty;
			this.objectModelPath	= string.Empty;
			this.objectAvatarPath	= string.Empty;
			this.currentResistant 	= 2f;
			this.currentDamage 		= 3f;
			this.currentDurability 	= 100f;
			this.maxDurability 		= 100f;
			this.actionDelay 		= 0.1f;

			this.m_DictJSON = new Dictionary<string, object> ();
		}

		public virtual string ToJSON ()
		{
			this.m_DictJSON.Add ("objectName", this.objectName);
			this.m_DictJSON.Add ("objectModelPath", this.objectModelPath);
			this.m_DictJSON.Add ("objectAvatarPath", this.objectAvatarPath);
			this.m_DictJSON.Add ("currentResistant", this.currentResistant);
			this.m_DictJSON.Add ("currentDamage", this.currentDamage);
			this.m_DictJSON.Add ("currentDurability", this.currentDurability);
			this.m_DictJSON.Add ("maxDurability", this.maxDurability);
			this.m_DictJSON.Add ("actionDelay", this.actionDelay);
			var json = MiniJSON.Json.Serialize (this.m_DictJSON);
			return json;
		}
		
	}
}
