using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingHuntZombie {
	public class CUICarPartItem : MonoBehaviour, IResult {

		public Text carPartNameText;
		public Image carPartImage;
		public CCarPartData carPartData;

		public void SetUpItem(CCarPartData value) {
			this.carPartData = value;
			this.carPartNameText.text = value.objectName;
		}

		#region IResult implementation

		public void SetObject (object value)
		{
			var objData = value as CCarPartData;
			this.carPartData = objData;
		}

		public object GetObject ()
		{
			return this.carPartData;
		}

		public void Clear ()
		{
			this.carPartData = null;
		}

		#endregion
	}
}
