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

		[Header ("Root")]
		[SerializeField]	private GameObject m_RootCar;
		[SerializeField]	private GameObject m_RootZombie;
		[SerializeField]	private GameObject m_RootObstacle;

		[Header ("Control")]
		[SerializeField]	private UIJoytick m_Joytick;
		[SerializeField]	private CCarController m_CarControl;

		protected virtual void LateUpdate() {
			var joytick 	= this.m_Joytick.InputDirectionXY;
			var angleInput 	= (joytick.x > 0.5 || joytick.x < -0.5) ? joytick.x : 0f;  //Input.GetAxis("Horizontal");
			var torqueInput = joytick.y; //Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, this.m_Joytick.GetEnableJoytick());
		}

	}
}
