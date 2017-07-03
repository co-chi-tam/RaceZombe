using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	[Serializable]
	public class CCarPartsComponent: CComponent {

		[Header("Content parts")]
		[SerializeField]	protected CCarController m_Owner;
		[SerializeField]	protected GameObject m_TopPart;
		[SerializeField]	protected GameObject m_FrontPart;
		[SerializeField]	protected GameObject m_BackPart;

		public enum ECarPart: byte {
			NONE 	= 0,
			TOP 	= 1,
			FRONT 	= 2,
			BACK 	= 3
		}

		public new void Init (params CCarPartData[] parts)
		{
			base.Init ();
			for (int i = 0; i < parts.Length; i++) {
				if (parts[i] == null)
					continue;
				this.UpdateCarParts (parts[i]);
			}
		}

		private void UpdateCarParts(CCarPartData part) {
			if (part.partType == ECarPart.NONE)
				return;
			var objGO = GameObject.Instantiate (Resources.Load<CCarPartController> ("Prefabs/" + part.objectName));
			switch (part.partType) {
			case ECarPart.TOP:
				objGO.transform.SetParent (this.m_TopPart.transform);
				break;
			case ECarPart.FRONT:
				objGO.transform.SetParent (this.m_FrontPart.transform);
				break;
			case ECarPart.BACK:
				objGO.transform.SetParent (this.m_BackPart.transform);
				break;
			}
			objGO.transform.localPosition = Vector3.zero;
			objGO.transform.localRotation = Quaternion.identity;
			objGO.SetData (part);
			objGO.SetOwner (this.m_Owner);
		}

	}
}
