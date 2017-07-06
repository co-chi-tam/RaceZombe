using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CDamageObject : CComponent {

		[SerializeField]	protected float m_MaxResistant = 20f;
		[SerializeField]	protected float m_CurrentDamage = 0f;
		[SerializeField]	protected float m_MaxDamage = 100f;
		[SerializeField]	protected bool m_AutoTrigger;
		[Header ("Event")]
		public UnityEvent OnDamaged;

		public float maxResistant {
			get { return this.m_MaxResistant; }
			set { this.m_MaxResistant = value; }
		}

		public float currentDamage {
			get { return this.m_CurrentDamage; }
			set { this.m_CurrentDamage = value; }
		}

		public float maxDamage {
			get { return this.m_MaxDamage; }
			set { this.m_MaxDamage = value; }
		}

		public new void Init(float resistant, float maxDamage) {
			base.Init ();
			this.m_MaxResistant = resistant;
			this.m_MaxDamage = maxDamage;
		}

		public virtual void CalculteDamage(float damage) {
			var totalDamage = damage <= 0f ? 0f : damage;
			this.m_CurrentDamage += totalDamage;
			if (this.m_AutoTrigger == true) {
				this.EffectDamage ();
			}
		}

		public virtual void EffectDamage() {
			if (this.OnDamaged != null) {
				this.OnDamaged.Invoke ();
			}
		}

		public virtual bool IsOutOfDamage() {
			return this.m_CurrentDamage >= this.m_MaxDamage;
		}

	}
}
