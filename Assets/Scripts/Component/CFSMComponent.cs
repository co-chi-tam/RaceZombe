using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace RacingHuntZombie {
	[Serializable]
	public class CFSMComponent : CComponent {

		#region Properties

		[SerializeField]	protected TextAsset m_FSMJsonText;
		[SerializeField]	protected string m_FSMStateName;

		protected FSMManager m_FSMManager;

		#endregion

		#region Implementation Component

		public new void Init (ISimpleContext context)
		{
			base.Init ();
			this.m_FSMManager = new FSMManager ();
			this.m_FSMManager.LoadFSM (this.m_FSMJsonText.text);
			this.m_FSMManager.RegisterState ("IdleState", 	new FSMIdleState(context));
			this.m_FSMManager.RegisterState ("ActiveState", new FSMActiveState(context));
			this.m_FSMManager.RegisterState ("DeathState", 	new FSMDeathState(context));

			this.m_FSMManager.RegisterCondition ("HaveEnemy", 	context.HaveEnemy);
			this.m_FSMManager.RegisterCondition ("HaveGas", 	context.HaveGas);
			this.m_FSMManager.RegisterCondition ("IsDeath", 	context.IsDeath);
		}

		public override void UpdateComponent (float dt)
		{
			base.UpdateComponent (dt);
			this.m_FSMManager.UpdateState (dt);
			this.m_FSMStateName = this.m_FSMManager.currentStateName;
		}

		#endregion

	}
}
