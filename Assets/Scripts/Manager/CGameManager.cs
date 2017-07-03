using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UICustomize;
using SimpleSingleton;
using ObjectPool;
using Pul;

namespace RacingHuntZombie {

	public class CGameManager : CMonoSingleton<CGameManager> {

		[Header ("Control")]
		[SerializeField]	private UIJoytick m_Joytick;
		[SerializeField]	private CCarController m_CarControl;

		protected virtual void LateUpdate() {
#if UNITY_EDITOR
			var angleInput 	= Input.GetAxis("Horizontal");
			var torqueInput = Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, torqueInput == 0f);
#else
			var joytick 	= this.m_Joytick.InputDirectionXY;
			var angleInput 	= (joytick.x > 0.5 || joytick.x < -0.5) ? joytick.x : 0f;  //Input.GetAxis("Horizontal");
			var torqueInput = joytick.y; //Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, this.m_Joytick.GetEnableJoytick());
#endif
		}

	}
}
