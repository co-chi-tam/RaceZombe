using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingHuntZombie {
	public class CMissionPoint : MonoBehaviour {

		public RectTransform missionRect;
		public Text missionPointText;
		public Button missionPointButton;
		public GameObject missionBlockGO;

		protected RectTransform m_RectTransform;
		protected float m_ScreenWidth = 1280f;
		protected float m_ScreenHeight = 800f;
		protected float m_PointWidth = 75f;
		protected float m_PointHeight = 75f;

		protected virtual void Awake() {
			this.m_RectTransform = transform as RectTransform;
			this.m_ScreenWidth = Screen.width;
			this.m_ScreenHeight = Screen.height;
			this.m_PointWidth = missionRect.sizeDelta.x / 2f;
			this.m_PointHeight = missionRect.sizeDelta.y / 2f;
		}

		protected virtual void LateUpdate() {
			this.FitPosition ();
		}

		private void FitPosition() {
			var screenFitPoint = Camera.main.WorldToScreenPoint (missionBlockGO.transform.position);
//			if (screenFitPoint.x < 0f + this.m_PointWidth) {
//				screenFitPoint.x = 0f + this.m_PointWidth;
//			} else if (screenFitPoint.x > this.m_ScreenWidth - this.m_PointWidth) {
//				screenFitPoint.x = this.m_ScreenWidth - this.m_PointWidth;
//			}
//			if (screenFitPoint.y < 0f + this.m_PointHeight) {
//				screenFitPoint.y = 0f + this.m_PointHeight;
//			} else if (screenFitPoint.y > this.m_ScreenHeight - this.m_PointHeight) {
//				screenFitPoint.y = this.m_ScreenHeight - this.m_PointHeight;
//			}
			screenFitPoint.z = 0f;
			this.m_RectTransform.position = screenFitPoint;
		}

	}
}
