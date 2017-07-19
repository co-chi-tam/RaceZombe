﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	class CSelectCarSceneTask : CSimpleTask {

		#region Properties

		private float m_Timer = 3f;

		#endregion

		#region Constructor

		public CSelectCarSceneTask () : base ()
		{
			this.taskName = "SelectCarScene";
			this.nextTask = "RaceScene";
		}

		#endregion

		#region Implementation Task

		public override void StartTask ()
		{
			base.StartTask ();
		}

		public override void UpdateTask (float dt)
		{
			base.UpdateTask (dt);
			if (this.m_IsCompleteTask == false) {
				this.m_Timer -= dt;
				this.m_IsCompleteTask = this.m_Timer < 0f;
			} else {
				if (this.OnCompleteTask != null) {
					this.OnCompleteTask ();
				}
			}
		}

		#endregion
		
	}
}
