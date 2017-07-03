using System;
using UnityEngine;

namespace RacingHuntZombie {
	public interface IActiveObject {

		void SetActive(bool value);
		bool GetActive();
		
	}
}
