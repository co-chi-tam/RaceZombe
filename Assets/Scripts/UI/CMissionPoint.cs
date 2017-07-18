using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingHuntZombie {
	public class CMissionPoint : MonoBehaviour {

		public Text missionPointText;
		public Button missionPointButton;
		public GameObject missionBlockGO;

		protected RectTransform m_RectTransform;

		protected virtual void Awake() {
			this.m_RectTransform = transform as RectTransform;
		}

		protected virtual void LateUpdate() {
			var screenPoint = Camera.main.WorldToScreenPoint (missionBlockGO.transform.position);
			this.m_RectTransform.position = screenPoint;
		}

	}
}
