using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CMeteoriteController : CInterativeCarPartController {

		[SerializeField]	private bool m_RepeatAction = false;
		[SerializeField]	private GameObject[] m_ActiveObjects;

		protected override void Start ()
		{
			base.Start ();
			this.m_CurrentActionDelay = this.m_Data.actionDelay;
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.m_CurrentActionDelay <= 0f) {
				this.InteractiveOrtherObject (null);
				if (this.m_RepeatAction == false) {
					this.DestroyObject ();
				}
			}
		}

		public override void SetActive (bool value)
		{
			base.SetActive (value);
			for (int i = 0; i < this.m_ActiveObjects.Length; i++) {
				this.m_ActiveObjects [i].SetActive (value);
			}
		}
		
	}
}
