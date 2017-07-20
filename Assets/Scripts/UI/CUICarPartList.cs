using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingHuntZombie {
	public class CUICarPartList : MonoBehaviour {

		[SerializeField]	protected GameObject m_ListRoot;
		[SerializeField]	protected CUICarPartItem m_ItemPrefab;
		[SerializeField]	protected TextAsset[] m_ItemDatas;

		protected void Start() {
			// TEST
			for (int i = 0; i < this.m_ItemDatas.Length; i++) {
				var itemData = TinyJSON.JSON.Load (this.m_ItemDatas [i].text).Make<CCarPartData> ();
				var itemGO = Instantiate (this.m_ItemPrefab);
				itemGO.transform.SetParent (this.m_ListRoot.transform);
				itemGO.SetUpItem (itemData);
				itemGO.gameObject.SetActive (true);
			}
			this.m_ItemPrefab.gameObject.SetActive (false);
		}
		
	}
}
