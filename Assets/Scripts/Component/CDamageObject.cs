using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RacingHuntZombie {
	[Serializable]
	public class CDamageObject : CComponent {

		#region Properties

		[SerializeField]	protected float m_MaxResistant = 20f;
		[SerializeField]	protected float m_CurrentDamage = 0f;
		[SerializeField]	protected float m_MaxDamage = 100f;
		[SerializeField]	protected float percentDamage = 100f;
		[Header ("Event")]
		public UnityEvent OnDamaged;
		public UnityEvent On10PercentDamage;
		public UnityEvent On30PercentDamage;
		public UnityEvent On50PercentDamage;
		public UnityEvent On80PercentDamage;

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

		#endregion

		#region Implementation Component

		public new void Init(float resistant, float maxDamage) {
			base.Init ();
			this.m_MaxResistant = resistant;
			this.m_MaxDamage = maxDamage;
		}

		#endregion

		#region Main methods

		public virtual void CalculteDamage(float damage) {
			var totalDamage = damage <= 0.1f ? 0.1f : damage;
			this.m_CurrentDamage += totalDamage;
			this.EffectDamage ();
		}

		public virtual void EffectDamage() {
			if (this.OnDamaged != null) {
				this.OnDamaged.Invoke ();
			}
			percentDamage = 100f - (this.m_CurrentDamage / this.m_MaxDamage * 100f);
			if (percentDamage < 100f - 10f) {
				if (this.On10PercentDamage != null) {
					this.On10PercentDamage.Invoke ();
				}
			} 
			if (percentDamage < 100f - 30f) {
				if (this.On30PercentDamage != null) {
					this.On30PercentDamage.Invoke ();
				}
			} 
			if (percentDamage < 100f - 50f) {
				if (this.On50PercentDamage != null) {
					this.On50PercentDamage.Invoke ();
				}
			} 
			if (percentDamage < 100f - 80f) {
				if (this.On80PercentDamage != null) {
					this.On80PercentDamage.Invoke ();
				}
			}
		}

		public virtual bool IsOutOfDamage() {
			return this.m_CurrentDamage >= this.m_MaxDamage;
		}

		#endregion

	}
}
