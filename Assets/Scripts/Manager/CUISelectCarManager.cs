using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleSingleton;

namespace RacingHuntZombie {
	public class CUISelectCarManager : CMonoSingleton<CUISelectMissionManager> {

		[Header ("Car Object")]
		[SerializeField]	protected TextAsset m_ObjectText;
		[SerializeField]	protected CObjectController m_ObjectController;
		[SerializeField]	protected Vector3 m_RotationDirection;
		[SerializeField]	protected float m_RotationSpeed = 10f;
		[Header ("Car part panel")]
		[SerializeField]	protected GameObject m_FrontPanel;
		[SerializeField]	protected GameObject m_TopPanel;
		[SerializeField]	protected GameObject m_BackPanel;
		[SerializeField]	protected GameObject m_FrontPartSelectPanel;
		[SerializeField]	protected GameObject m_TopPartSelectPanel;
		[SerializeField]	protected GameObject m_BackPartSelectPanel;
		[SerializeField]	protected UIDrop m_FrontCarPartSelected;
		[SerializeField]	protected UIDrop m_TopCarPartSelected;
		[SerializeField]	protected UIDrop m_BackCarPartSelected;
		[Header ("Line")]
		[SerializeField]	protected GameObject m_LineRoot;
		[SerializeField]	protected RectTransform m_Line2DPrefab;

		protected CCarPartsComponent m_CarPartsComponent;
		protected GameObject m_FrontObject;
		protected GameObject m_TopObject;
		protected GameObject m_BackObject;

		protected List<RectTransform> m_Line2DList;

		// TEST
		protected CCarData m_CurrentCarData;

		protected virtual void Start() {
			this.InitLine2D ();
			this.LoadFrontPartSelectPanel ();
			// TEST
			this.m_CurrentCarData = TinyJSON.JSON.Load (this.m_ObjectText.text).Make<CCarData>();
			this.m_ObjectController.SetData (this.m_CurrentCarData);
		}

		protected void LateUpdate() {
			this.DrawLine2D ();
			this.RotationObject ();
		}

		public virtual void LoadFrontPartSelectPanel() {
			this.m_FrontPartSelectPanel.SetActive (true);
			this.m_TopPartSelectPanel.SetActive (false);
			this.m_BackPartSelectPanel.SetActive (false);
		}

		public virtual void LoadTopPartSelectPanel() {
			this.m_FrontPartSelectPanel.SetActive (false);
			this.m_TopPartSelectPanel.SetActive (true);
			this.m_BackPartSelectPanel.SetActive (false);
		}

		public virtual void LoadBackPartSelectPanel() {
			this.m_FrontPartSelectPanel.SetActive (false);
			this.m_TopPartSelectPanel.SetActive (false);
			this.m_BackPartSelectPanel.SetActive (true);
		}

		public void SubmitCar() {
			var frontPart = this.m_FrontCarPartSelected.GetAlreadyResult ();
			var topPart = this.m_TopCarPartSelected.GetAlreadyResult ();
			var backPart = this.m_BackCarPartSelected.GetAlreadyResult ();
			CCarPartData[] carPartsData = new CCarPartData[3];
			if (frontPart != null) {
				carPartsData[0] = frontPart.GetObject() as CCarPartData;
			}
			if (topPart != null) {
				carPartsData[1] = topPart.GetObject() as CCarPartData;
			}
			if (backPart != null) {
				carPartsData[2] = backPart.GetObject() as CCarPartData;
			}
			this.m_CurrentCarData.carParts = carPartsData;
			CTaskUtil.Set (CTaskUtil.PLAYER_SELECTED_CAR, this.m_CurrentCarData);
			CRootTask.Instance.GetCurrentTask ().OnTaskCompleted ();
		}

		public virtual void RotationObject() {
			if (this.m_ObjectController == null)
				return;
			var rotation = this.m_ObjectController.transform.rotation.eulerAngles;
			var offsetSpeed = this.m_RotationSpeed * Time.deltaTime;
			rotation.x += this.m_RotationDirection.x * offsetSpeed;
			rotation.y += this.m_RotationDirection.y * offsetSpeed;
			rotation.z += this.m_RotationDirection.z * offsetSpeed;
			this.m_ObjectController.transform.rotation = Quaternion.Euler (rotation);
		}

		public virtual void InitLine2D() {
			this.m_CarPartsComponent = this.m_ObjectController.GetCustomComponent<CCarPartsComponent> ();
			if (this.m_CarPartsComponent == null)
				return;
			this.m_Line2DList = new List<RectTransform> ();
			this.m_FrontObject = this.m_CarPartsComponent.GetCarPartObject (CCarPartsComponent.ECarPart.FRONT);
			this.CreateLine2D ("Line Front");
			this.m_TopObject = this.m_CarPartsComponent.GetCarPartObject (CCarPartsComponent.ECarPart.TOP);
			this.CreateLine2D ("Line Top");
			this.m_BackObject = this.m_CarPartsComponent.GetCarPartObject (CCarPartsComponent.ECarPart.BACK);
			this.CreateLine2D ("Line Back");
		}

		public virtual void DrawLine2D() {
			if (this.m_CarPartsComponent == null)
				return;
			var frontPartScreenPoint = Camera.main.WorldToScreenPoint (this.m_FrontObject.transform.position);
			this.LineTo2D (this.m_Line2DList[0], this.m_FrontPanel.transform.position, frontPartScreenPoint);
			var topPartScreenPoint = Camera.main.WorldToScreenPoint (this.m_TopObject.transform.position);
			this.LineTo2D (this.m_Line2DList[1], this.m_TopPanel.transform.position, topPartScreenPoint);
			var backPartScreenPoint = Camera.main.WorldToScreenPoint (this.m_BackObject.transform.position);
			this.LineTo2D (this.m_Line2DList[2], this.m_BackPanel.transform.position, backPartScreenPoint);
		}

		public virtual void LineTo2D(RectTransform line, Vector3 toV3, Vector3 fromV3) {
			line.transform.position = (fromV3 + toV3) / 2f;
			line.transform.right = (toV3 - fromV3).normalized;
			var magnitude = ((toV3 - fromV3) * 1f).magnitude;
			line.sizeDelta = new Vector2 (magnitude, 10f);
		}

		public virtual void CreateLine2D(string name) {
			var line = Instantiate (this.m_Line2DPrefab);
			line.transform.SetParent (this.m_LineRoot.transform);
			line.name = name;
			line.sizeDelta = this.m_Line2DPrefab.sizeDelta;
			line.localScale = this.m_Line2DPrefab.localScale;
			line.localPosition = this.m_Line2DPrefab.localPosition;
			line.gameObject.SetActive (true);
			this.m_Line2DList.Add (line);
		}
		
	}
}
