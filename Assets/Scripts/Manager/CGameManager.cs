﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UICustomize;
using SimpleSingleton;
using ObjectPool;
using FSM;

namespace RacingHuntZombie {

	public class CGameManager : CMonoSingleton<CGameManager>, IContext {

		[Header ("Game Mode")]
		[SerializeField]	protected TextAsset m_GameModeText;
		[SerializeField]	protected CGameModeData m_GameModeData;
		[SerializeField]	protected TextAsset m_FSMGraphText;
		[SerializeField]	protected string m_FSMCurrentStateName;

		[Header ("Map")]
		[SerializeField]	private CCameraController m_Camera;
		[SerializeField]	private CUIControlManager m_UIControl;
		[SerializeField]	private GameObject m_CarPrefabs;
		[SerializeField]	private GameObject[] m_ZombiePrefabs;
		[SerializeField]	private GameObject[] m_EnemyPrefabs;
		[SerializeField]	private CMapManager m_MapManager;

		[Header ("Control")]
		[SerializeField]	private UIJoytick m_Joytick;

		private CCarController m_CarControl;
		private bool m_AllLoadingComplete = false;
		private List<CZombieController> m_Zombies = new List<CZombieController> ();
		private FSMManager m_FSMManager;

		protected override void Awake() {
			base.Awake ();
			this.m_FSMManager = new FSMManager ();
			this.m_FSMManager.LoadFSM (this.m_FSMGraphText.text);
			this.m_FSMManager.RegisterState ("GameStartState", 	new FSMGameStartState(this));
			this.m_FSMManager.RegisterState ("GameUpdateState", new FSMGameUpdateState(this));
			this.m_FSMManager.RegisterState ("GameWinState", 	new FSMGameWinState(this));
			this.m_FSMManager.RegisterState ("GameCloseState", 	new FSMGameCloseState(this));

			this.m_FSMManager.RegisterCondition ("IsLoadingCompleted", 	this.IsLoadingCompleted);
			this.m_FSMManager.RegisterCondition ("IsPlayerDeath", 		this.IsPlayerDeath);
			this.m_FSMManager.RegisterCondition ("IsMissionComplete",	this.IsMissionComplete);
		}

		protected virtual void Start() {
			Application.targetFrameRate = 60;
			this.m_GameModeData = TinyJSON.JSON.Load (this.m_GameModeText.text).Make<CGameModeData>();
		}

		protected virtual void FixedUpdate() {
			this.m_FSMManager.UpdateState (Time.fixedDeltaTime);
			this.m_FSMCurrentStateName = this.m_FSMManager.currentStateName;
		}

		public void UpdateTopCarPart() {
			if (this.m_CarControl == null)
				return;
			this.m_CarControl.UpdateCarPart (CCarPartsComponent.ECarPart.TOP, null);
		}

		public void UpdateBackCarPart() {
			if (this.m_CarControl == null)
				return;
			this.m_CarControl.UpdateCarPart (CCarPartsComponent.ECarPart.BACK, null);
		}

		public virtual void StartRace(Action complete) {
			CHandleEvent.Instance.AddEvent (this.HandleSpawnMapObjects(), complete);
		}

		public virtual void UpdateGame() {
			if (this.m_CarControl == null)
				return;
#if UNITY_EDITOR
			var angleInput 	= Input.GetAxis("Horizontal");
			var torqueInput = Input.GetAxis("Vertical");
			this.m_CarControl.UpdateDriver (angleInput, torqueInput, angleInput == 0 && torqueInput == 0f);
			if (Input.GetKey (KeyCode.L)) {
				this.UpdateTopCarPart ();
			}
			if (Input.GetKey (KeyCode.M)) {
				this.UpdateBackCarPart ();
			}
#else
			var joytick 	= this.m_Joytick.InputDirectionXY;
			var angleInput 	= (joytick.x > 0.25 || joytick.x < -0.25) ? joytick.x : 0f;
			var torqueInput = joytick.y;

			this.m_CarControl.UpdateDriver (angleInput, torqueInput, angleInput == 0 && torqueInput == 0f);
#endif
			if (this.m_AllLoadingComplete) {
				for (int i = 0; i < this.m_Zombies.Count; i++) {
					if (this.m_Zombies [i] == null) {
						CHandleEvent.Instance.AddEvent (this.HandleSetupZombie (i, 
							this.m_CarControl), null);
						this.m_Zombies.RemoveAt(i);
						i--;
					}
				}
			}
		}

		public virtual void WinGame() {
			CUIControlManager.Instance.OpenMissionCompletePanel ();
		}

		public virtual void CloseGame() {
			CUIControlManager.Instance.OpenMissionClosePanel ();
		}

		public virtual void ResetGame() {
			CRootTask.Instance.GetCurrentTask ().OnTaskCompleted ();
		}

		private IEnumerator HandleSpawnMapObjects() {
			yield return HandleSpawnCar ();
			yield return HandleSpawnZombies ();
			yield return HandleSpawnEnemy ();
			this.m_AllLoadingComplete = true;
		}

		private IEnumerator HandleSpawnCar() {
			var carGO = Instantiate(this.m_CarPrefabs);
			yield return carGO;
			var carSpawnPoints = this.m_MapManager.GetCarSpawnPoints ();
			carGO.transform.position = carSpawnPoints[0].transform.position;
			this.m_CarControl = carGO.GetComponent<CCarController> ();
			this.m_CarControl.Init ();
			this.m_CarControl.SetActive (true);
			this.m_CarControl.IsBot = false;
			m_Camera.SetTarget (this.m_CarControl.transform);
			m_UIControl.SetTarget (this.m_CarControl);
		}

		private IEnumerator HandleSpawnEnemy() {
			var enemyCarIndex = this.m_EnemyPrefabs.Length;
			var carGO = Instantiate(this.m_EnemyPrefabs[0]);
			yield return carGO;
			var carSpawnPoints = this.m_MapManager.GetZombieSpawnPoints ();
			carGO.transform.position = carSpawnPoints[0].transform.position;
			var carEnemy = carGO.GetComponent<CCarController> ();
			carEnemy.Init ();
			carEnemy.SetActive (true);
			carEnemy.SetTarget (this.m_CarControl);
			carEnemy.IsBot = true;
		}

		private IEnumerator HandleSpawnZombies() {
			var zombieSpawnPoints = this.m_MapManager.GetZombieSpawnPoints ();
			for (int i = 0; i < zombieSpawnPoints.Length; i++) {
				yield return this.HandleSetupZombie(i, this.m_CarControl);
			}
		}

		private IEnumerator HandleSetupZombie(int index, CCarController target) {
			var zombieSpawnPoints = this.m_MapManager.GetZombieSpawnPoints ();
			var position = zombieSpawnPoints [index].transform.position;
			var zombieGO = Instantiate(this.m_ZombiePrefabs [index % this.m_ZombiePrefabs.Length]);
			yield return zombieGO;
			zombieGO.transform.position = position;
			var zombieCtrl = zombieGO.GetComponent<CZombieController> ();
			zombieCtrl.Init ();
			zombieCtrl.SetActive (true);
			zombieCtrl.SetTarget (target);
			this.m_Zombies.Add (zombieCtrl);
		}

		protected virtual bool IsLoadingCompleted() {
			return this.m_AllLoadingComplete;
		}

		protected virtual bool IsPlayerDeath() {
			return this.m_CarControl.GetActive() == false
				|| this.m_CarControl.GetGas () <= 0f;
		}

		protected virtual bool IsMissionComplete() {
			if (this.m_GameModeData == null)
				return false;
			if (this.m_GameModeData.gameMissionInfo == null)
				return false;
			return this.m_CarControl.GetMissionObject(this.m_GameModeData.gameMissionInfo);
		}

	}
}
