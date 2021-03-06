﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CCarPartsComponent: CComponent {

		#region Properties

		[Header("Content parts")]
		[SerializeField]	protected CCarController m_Owner;
		[SerializeField]	protected GameObject m_TopPart;
		[SerializeField]	protected GameObject m_FrontPart;
		[SerializeField]	protected GameObject m_BackPart;

		public enum ECarPart: int {
			NONE 	= -1,
			TOP 	= 0,
			FRONT 	= 1,
			BACK 	= 2
		}

		protected Dictionary<ECarPart, CCarPartController> m_CarPartMap;

		#endregion

		#region Implementation Component

		public new void Init (params CCarPartData[] parts)
		{
			base.Init ();
			this.m_CarPartMap = new Dictionary<ECarPart, CCarPartController> ();
			for (int i = 0; i < parts.Length; i++) {
				if (parts[i] == null)
					continue;
				this.SpawnCarParts (parts[i]);
			}
		}

		#endregion

		#region Main methods

		private void SpawnCarParts(CCarPartData part) {
			if (part.partType == ECarPart.NONE)
				return;
			var objCtrl = GameObject.Instantiate (Resources.Load<CCarPartController> ("Prefabs/" + part.objectModelPath));
			switch (part.partType) {
			case ECarPart.TOP:
				objCtrl.transform.SetParent (this.m_TopPart.transform);
				break;
			case ECarPart.FRONT:
				objCtrl.transform.SetParent (this.m_FrontPart.transform);
				break;
			case ECarPart.BACK:
				objCtrl.transform.SetParent (this.m_BackPart.transform);
				break;
			}
			objCtrl.transform.localPosition = Vector3.zero;
			objCtrl.transform.localRotation = Quaternion.identity;
			objCtrl.SetActive (true);
			objCtrl.SetData (part);
			objCtrl.SetOwner (this.m_Owner);
			this.m_CarPartMap[part.partType] = objCtrl;
		}

		public CCarPartController GetCarPart(ECarPart part) {
			if (part == ECarPart.NONE)
				return null;
			if (this.m_CarPartMap.ContainsKey (part) == false)
				return null;
			return this.m_CarPartMap [part];
		}

		public GameObject GetCarPartObject(ECarPart part) {
			switch (part) {
			case ECarPart.TOP:
				return this.m_TopPart;
			case ECarPart.FRONT:
				return this.m_FrontPart;
			case ECarPart.BACK:
				return this.m_BackPart;
			}
			return null;
		}

		#endregion

	}
}
