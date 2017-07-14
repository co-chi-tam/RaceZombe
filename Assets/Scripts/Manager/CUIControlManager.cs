using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingHuntZombie {
	public class CUIControlManager : MonoBehaviour {

		[SerializeField]	private CObjectController m_ObjectController;

		[SerializeField]	private Image m_CarDurabilityImage;
		[SerializeField]	private Image m_GasImage;
		[SerializeField]	private Image m_FrontPartDurabilityImage;
		[SerializeField]	private Image m_TopPartDurabilityImage;
		[SerializeField]	private Image m_BackPartDurabilityImage;

		protected void LateUpdate() {
			if (this.m_ObjectController == null)
				return;
			var carDurability = this.m_ObjectController.GetDurabilityPercent () / 100f;
			this.m_CarDurabilityImage.fillAmount = Mathf.Lerp (this.m_CarDurabilityImage.fillAmount, carDurability, 0.5f);

			var gas = this.m_ObjectController.GetGasPercent () / 100f;
			this.m_GasImage.fillAmount = Mathf.Lerp (this.m_GasImage.fillAmount, gas, 0.5f);

			var frontDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.FRONT) / 100f;
			this.m_FrontPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_FrontPartDurabilityImage.fillAmount, frontDurability, 0.5f);

			var topDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.TOP) / 100f;
			this.m_TopPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_TopPartDurabilityImage.fillAmount, topDurability, 0.5f);

			var backDurability = this.m_ObjectController.GetCarPartPercent(CCarPartsComponent.ECarPart.BACK) / 100f;
			this.m_BackPartDurabilityImage.fillAmount = Mathf.Lerp (this.m_BackPartDurabilityImage.fillAmount, backDurability, 0.5f); 
		}

		public void SetTarget(CObjectController controller) {
			this.m_ObjectController = controller;
		}
		
	}
}
