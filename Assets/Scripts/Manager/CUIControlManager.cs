using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleSingleton;

namespace RacingHuntZombie {
	public class CUIControlManager : CMonoSingleton<CUIControlManager> {

		[Header ("Target Control")]
		[SerializeField]	private CObjectController m_ObjectController;

		[Header ("Car Info")]
		[SerializeField]	private Image m_CarDurabilityImage;
		[SerializeField]	private Image m_GasImage;
		[SerializeField]	private Image m_FrontPartDurabilityImage;
		[SerializeField]	private Image m_TopPartDurabilityImage;
		[SerializeField]	private Image m_BackPartDurabilityImage;
		[SerializeField]	private Image m_MissionProcessImage;
		[SerializeField]	private Text m_MissionProcessText;

		[Header ("Mission Panel")]
		[SerializeField]	private GameObject m_MissionCompletePanel;
		[SerializeField]	private GameObject m_MissionFailPanel;

		protected CGameManager m_GameManager;

		protected override void Awake ()
		{
			base.Awake ();
		}

		protected virtual void Start() {
			this.m_GameManager = CGameManager.GetInstance ();
		}

		protected void LateUpdate() {
			if (this.m_ObjectController == null)
				return;
			var carDurability = this.m_ObjectController.GetDurabilityPercent ();
			this.m_CarDurabilityImage.fillAmount = Mathf.Lerp (this.m_CarDurabilityImage.fillAmount, carDurability, 0.5f);

			var gas = this.m_ObjectController.GetGasPercent ();
			this.m_GasImage.fillAmount = Mathf.Lerp (this.m_GasImage.fillAmount, gas, 0.5f);

			var frontDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.FRONT);
			this.m_FrontPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_FrontPartDurabilityImage.fillAmount, frontDurability, 0.5f);

			var topDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.TOP);
			this.m_TopPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_TopPartDurabilityImage.fillAmount, topDurability, 0.5f);

			var backDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.BACK);
			this.m_BackPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_BackPartDurabilityImage.fillAmount, backDurability, 0.5f); 
		
			var missionProcesss = this.m_ObjectController.GetMissionPercent ();
			this.m_MissionProcessImage.fillAmount = Mathf.Lerp (this.m_MissionProcessImage.fillAmount, missionProcesss, 0.5f); 

			var gameMission = this.m_GameManager.GetMissionData ();
			this.m_MissionProcessText.text = gameMission.gameModeDescription;
		}

		public void OpenMissionCompletePanel() {
			this.m_MissionCompletePanel.gameObject.SetActive (true);
			this.m_MissionFailPanel.gameObject.SetActive (false);
		}

		public void OpenMissionClosePanel() {
			this.m_MissionCompletePanel.gameObject.SetActive (false);
			this.m_MissionFailPanel.gameObject.SetActive (true);
		}

		public void SetTarget(CObjectController controller) {
			this.m_ObjectController = controller;
		}

		public void ResetGame() {
			CGameManager.Instance.ResetGame ();
		}
		
	}
}
