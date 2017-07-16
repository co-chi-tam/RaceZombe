using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CMissionComponent : CComponent {

		protected Dictionary<CObjectData.EObjectType, int> m_MissionObjects;

		public override void Init ()
		{
			base.Init ();
			this.m_MissionObjects = new Dictionary<CObjectData.EObjectType, int> ();
		}

		public virtual bool IsMissionComplete(Dictionary<CObjectData.EObjectType, int> missionMaps) {
			if (this.m_MissionObjects == null)
				return false;
			var result = missionMaps.Count > 0;
			foreach (var item in missionMaps) {
				if (this.m_MissionObjects.ContainsKey (item.Key)) {
					var keyValue = this.m_MissionObjects [item.Key];
					result &= keyValue == item.Value;
				} else {
					result = false;
					break;
				}
			}
			return result;
		}

		public virtual void SetMissionObject(CObjectData.EObjectType key, int value) {
			if (this.m_MissionObjects == null)
				return;
			if (this.m_MissionObjects.ContainsKey (key) == false) {
				this.m_MissionObjects.Add (key, 0);
			}
			var currentValue = this.m_MissionObjects [key];
			var totalValue = currentValue + value;
			this.m_MissionObjects [key] = totalValue;
		}
	
		public virtual int GetMissionObject(CObjectData.EObjectType key) {
			if (this.m_MissionObjects == null)
				return 0;
			if (this.m_MissionObjects.ContainsKey (key) == false)
				return 0;
			return this.m_MissionObjects [key];
		}
		
	}
}
