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

		private bool m_AllLoadingComplete = false;
		private List<CZombieController> m_Zombies = new List<CZombieController> ();

		protected virtual void Start() {
			Application.targetFrameRate = 60;
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
			if (Input.GetKey (KeyCode.M)) {
				this.UpdateBackCarPart ();
			}
#else
			var joytick 	= this.m_Joytick.InputDirectionXY;
			var angleInput 	= (joytick.x > 0.5 || joytick.x < -0.5) ? joytick.x : 0f;  //Input.GetAxis("Horizontal");
			var torqueInput = joytick.y; //Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, this.m_Joytick.GetEnableJoytick());
#endif
			if (this.m_AllLoadingComplete) {
				for (int i = 0; i < this.m_Zombies.Count; i++) {
					if (this.m_Zombies [i] == null) {
						CHandleEvent.Instance.AddEvent (this.HandleSetupZombie (true, 
							this.m_ZombieSpawnPoints [i % this.m_ZombieSpawnPoints.Length].transform.position, 
							this.m_CarControl), null);
						this.m_Zombies.RemoveAt(i);
						i--;
					}
				}
			}
		}

		public void UpdateTopCarPart() {
			this.m_CarControl.UpdateCarPart (CCarPartsComponent.ECarPart.TOP, null);
		}

		public void UpdateBackCarPart() {
			this.m_CarControl.UpdateCarPart (CCarPartsComponent.ECarPart.BACK, null);
		}

		public virtual void StartRace(Action complete) {
			CHandleEvent.Instance.AddEvent (this.HandleSpawnMapObjects(), complete);
		}

		private IEnumerator HandleSpawnMapObjects() {
			yield return HandleSpawnCar ();
			yield return HandleSpawnZombies ();
			this.m_AllLoadingComplete = true;
		}

		private IEnumerator HandleSpawnCar() {
			var carGO = Instantiate(this.m_CarPrefabs);
			yield return carGO;
			carGO.transform.position = this.m_CarSpawnPoints [0].transform.position;
			this.m_CarControl = carGO.GetComponent<CCarController> ();
			this.m_CarControl.SetActive (true);
			this.m_Camera.SetTarget (carGO.transform);
		}

		private IEnumerator HandleSpawnZombies() {
			for (int i = 0; i < m_ZombieSpawnPoints.Length; i++) {
				yield return this.HandleSetupZombie(true, this.m_ZombieSpawnPoints [i].transform.position, this.m_CarControl);
			}
		}

		private IEnumerator HandleSetupZombie(bool active, Vector3 position, CCarController target) {
			var zombieGO = Instantiate(this.m_ZombiePrefabs);
			yield return zombieGO;
			zombieGO.transform.position = position;
			var zombieCtrl = zombieGO.GetComponent<CZombieController> ();
			zombieCtrl.SetActive (active);
			zombieCtrl.SetTarget (target);
			this.m_Zombies.Add (zombieCtrl);
		}

	}
}
