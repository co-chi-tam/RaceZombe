using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CSelectModeSceneTask : CSimpleTask {

		#region Properties

		#endregion

		#region Constructor

		public CSelectModeSceneTask () : base ()
		{
			this.taskName = "SelectModeScene";
			this.nextTask = "RaceScene";
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
			this.m_IsLoadingTask = true;
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
			if (this.m_IsCompleteTask == false) {
				var cameraPos = Camera.main.transform.rotation.eulerAngles;
				cameraPos.y += Time.deltaTime * 15f;
				Camera.main.transform.rotation = Quaternion.Euler (cameraPos);
				this.m_IsCompleteTask = cameraPos.y >= 360f;
			} else {
				if (this.OnCompleteTask != null) {
					this.OnCompleteTask ();
				}
			}


		}

		#endregion
	}
}
