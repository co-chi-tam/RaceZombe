using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CMissionComponent : CComponent {

		[SerializeField]	protected float m_MissionPercent = 0f;

		protected Dictionary<string, float> m_MissionObjects;
		protected bool m_IsMissionUpdated = false;

		public override void Init ()
		{
			base.Init ();
			this.m_MissionObjects = new Dictionary<string, float> ();
			this.m_MissionObjects.Add ("CarDurability", 0f);
			this.m_MissionObjects.Add ("CarGas", 0f);
		}

		public virtual bool IsMissionComplete(Dictionary<string, float> missionMaps) {
			if (this.m_MissionObjects == null)
				return false;
			if (this.m_IsMissionUpdated == false)
				return false;
			var result = missionMaps.Count > 0;
			var percent = 0f;
			foreach (var item in missionMaps) {
				if (this.m_MissionObjects.ContainsKey (item.Key)) {
					var keyValue = this.m_MissionObjects [item.Key];
					percent += keyValue / item.Value;
					result &= this.ComditionComplete (keyValue, item.Value);
				} else {
					result = false;
					break;
				}
			}
			this.m_IsMissionUpdated = false;
			this.m_MissionPercent = percent / missionMaps.Count;
			return result;
		}

		public virtual bool ComditionComplete(float a, float b)  {
			return a >= b;
		}

		public virtual void SetMissionObject(string key, float value) {
			if (this.m_MissionObjects == null)
				return;
			if (this.m_MissionObjects.ContainsKey (key) == false) {
				this.m_MissionObjects.Add (key, 0);
			}
			var currentValue = this.m_MissionObjects [key];
			var totalValue = currentValue + value;
			this.m_MissionObjects [key] = totalValue;
			this.m_IsMissionUpdated = true;
		}
	
		public virtual float GetMissionObject(string key) {
			if (this.m_MissionObjects == null)
				return 0;
			if (this.m_MissionObjects.ContainsKey (key) == false)
				return 0;
			return this.m_MissionObjects [key];
		}

		public virtual float GetMissionPercent() {
			return this.m_MissionPercent;
		}
		
	}
}
