using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CMissionComponent : CComponent {

		protected Dictionary<string, int> m_MissionObjects;

		public override void Init ()
		{
			base.Init ();
			this.m_MissionObjects = new Dictionary<string, int> ();
		}

		public virtual bool IsMissionComplete(Dictionary<string, int> missionMaps) {
			if (this.m_MissionObjects == null)
				return false;
			var result = missionMaps.Count > 0;
			foreach (var item in missionMaps) {
				if (this.m_MissionObjects.ContainsKey (item.Key)) {
					var keyValue = this.m_MissionObjects [item.Key];
					result &= this.ComditionComplete (keyValue, item.Value);
				} else {
					result = false;
					break;
				}
			}
			return result;
		}

		public virtual bool ComditionComplete(int a, int b)  {
			return a >= b;
		}

		public virtual void SetMissionObject(string key, int value) {
			if (this.m_MissionObjects == null)
				return;
			if (this.m_MissionObjects.ContainsKey (key) == false) {
				this.m_MissionObjects.Add (key, 0);
			}
			var currentValue = this.m_MissionObjects [key];
			var totalValue = currentValue + value;
			this.m_MissionObjects [key] = totalValue;
		}
	
		public virtual int GetMissionObject(string key) {
			if (this.m_MissionObjects == null)
				return 0;
			if (this.m_MissionObjects.ContainsKey (key) == false)
				return 0;
			return this.m_MissionObjects [key];
		}
		
	}
}
