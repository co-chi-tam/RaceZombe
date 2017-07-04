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

		[Header ("Map")]
		[SerializeField]	private CCameraController m_Camera;
		[SerializeField]	private GameObject m_CarPrefabs;
		[SerializeField]	private GameObject[] m_CarSpawnPoints;
		[SerializeField]	private GameObject m_ZombiePrefabs;
		[SerializeField]	private GameObject[] m_ZombieSpawnPoints;

		[Header ("Control")]
		[SerializeField]	private UIJoytick m_Joytick;
		[SerializeField]	private CCarController m_CarControl;

		protected virtual void Start() {
			
		}

		protected virtual void LateUpdate() {
			if (this.m_CarControl == null)
				return;
#if UNITY_EDITOR
			var angleInput 	= Input.GetAxis("Horizontal");
			var torqueInput = Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, torqueInput == 0f);
			if (Input.GetKey (KeyCode.L)) {
				this.UpdateTopCarPart ();
			}
#else
			var joytick 	= this.m_Joytick.InputDirectionXY;
			var angleInput 	= (joytick.x > 0.5 || joytick.x < -0.5) ? joytick.x : 0f;  //Input.GetAxis("Horizontal");
			var torqueInput = joytick.y; //Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, this.m_Joytick.GetEnableJoytick());
#endif
		}

		public void UpdateTopCarPart() {
			this.m_CarControl.UpdateCarPart (CCarPartsComponent.ECarPart.TOP, null);
		}

		public virtual void StartRace(Action complete) {
			CHandleEvent.Instance.AddEvent (this.HandleSpawnMapObjects(), complete);
		}

		private IEnumerator HandleSpawnMapObjects() {
			yield return HandleSpawnCar ();
			yield return HandleSpawnZombies ();
		}

		private IEnumerator HandleSpawnCar() {
			var carGO = Instantiate(this.m_CarPrefabs);
			yield return carGO;
			carGO.transform.position = this.m_CarSpawnPoints [0].transform.position;
			this.m_CarControl = carGO.GetComponent<CCarController> ();
			this.m_Camera.SetFollower (carGO.transform);
		}

		private IEnumerator HandleSpawnZombies() {
			for (int i = 0; i < m_ZombieSpawnPoints.Length; i++) {
				var zombieGO = Instantiate(this.m_ZombiePrefabs);
				yield return zombieGO;
				zombieGO.transform.position = this.m_ZombieSpawnPoints [i].transform.position;
				var zombieCtrl = zombieGO.GetComponent<CZombieController> ();
				zombieCtrl.SetTargetCollider (this.m_CarControl.GetComponent<Collider>());
			}
		}

	}
}
