using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleSingleton;

namespace RacingHuntZombie {
	public class CUISelectMissionManager : CMonoSingleton<CUISelectMissionManager> {

		[Header ("Map Object")]
		[SerializeField]	protected GameObject[] m_MapMissionObjects;

		[Header ("Mission Object")]
		[SerializeField]	protected GameObject m_MissionRoot;
		[SerializeField]	protected CMissionPoint m_MissionPointPrefabs;
		[SerializeField]	protected CMissionInfo m_MissionInfo;

		protected List<CGameModeData> m_CurrentMissionList;
		protected List<GameObject> m_CurrentMissionObjList;
		protected CGameModeData m_CurrentMission;

		protected override void Awake ()
		{
			base.Awake ();
			this.m_CurrentMissionList = new List<CGameModeData> ();
			this.m_CurrentMissionObjList = new List<GameObject> ();
			this.m_CurrentMission = null;
			this.m_MissionInfo.gameObject.SetActive (false);
		}

		protected virtual void Start() {
			
		}

		public virtual void Init() {
			if (this.m_MapMissionObjects.Length == 0) {
				var countBlocks = GameObject.FindGameObjectsWithTag ("Block");
				this.m_MapMissionObjects = new GameObject[countBlocks.Length];
				for (int i = 0; i < m_MapMissionObjects.Length; i++) {
					this.m_MapMissionObjects [i] = countBlocks [i];
				}
			}
		}

		public virtual void LoadMission(params CGameModeData[] missions) {
			for (int i = 0; i < missions.Length; i++) {
				var randomBlock = Random.Range (0, m_MapMissionObjects.Length);
				var selectedBlock = this.m_MapMissionObjects [randomBlock];
				if (this.m_CurrentMissionObjList.Contains (selectedBlock)) {
					i--;
					continue;
				}
				var missionPoint = GameObject.Instantiate (this.m_MissionPointPrefabs);
				var missionData = missions [i];
				missionPoint.missionBlockGO = selectedBlock;
				missionPoint.missionPointText.text = missionData.gameModeHardPoint;
				missionPoint.missionPointButton.onClick.RemoveAllListeners ();
				missionPoint.missionPointButton.onClick.AddListener (() => {
					this.SelectMission(missionData);
				});
				missionPoint.gameObject.SetActive (true);
				missionPoint.gameObject.name = "Mission " + missionData.gameModeHardPoint;
				this.m_CurrentMissionList.Add (missionData);
				this.m_CurrentMissionObjList.Add (selectedBlock);
				missionPoint.transform.SetParent (this.m_MissionRoot.transform);
			}
			this.m_MissionPointPrefabs.gameObject.SetActive (false);
		}

		public virtual void SelectMission(CGameModeData mission) {
			this.m_CurrentMission = mission;
			this.m_MissionInfo.gameObject.SetActive (mission != null);
			this.m_MissionInfo.missionInfoNameText.text = mission.gameModeName;
			this.m_MissionInfo.missionInfoDescriptionText.text = mission.gameModeDescription;
			CTaskUtil.Set (CTaskUtil.PLAYER_SELECTED_MISSION, mission);
		}

		public virtual void SubmitMission() {
			if (this.m_CurrentMission != null) {
				CRootTask.Instance.GetCurrentTask ().OnTaskCompleted ();
			}
		}

	}
}
