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
		}

		public virtual bool IsOutOfDamage() {
			return this.m_CurrentDamage >= this.m_MaxDamage;
		}

		#endregion

	}
}
