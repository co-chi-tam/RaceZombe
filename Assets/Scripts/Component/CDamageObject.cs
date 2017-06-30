using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CDamageObject : CComponent {

		[SerializeField]	protected float m_CurrentResistant = 0;
		[SerializeField]	protected float m_CurrentDamage = 0f;
		[SerializeField]	protected float m_MaxDamage = 100f;

		public virtual void Init(float resistant, float maxDamage) {
			this.m_CurrentResistant = resistant;
			this.m_MaxDamage = maxDamage;
		}

		public virtual void CalculteDamge(float damage) {
			var totalDamage = damage - this.m_CurrentResistant <= 0f ? 0f : damage - this.m_CurrentResistant;
			this.m_CurrentDamage += totalDamage;
		}

		public virtual bool IsOutOfDamage() {
			return this.m_CurrentDamage >= this.m_MaxDamage;
		}

	}
}
