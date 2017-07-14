using UnityEngine;
using System.Collections;
using FSM;

namespace RacingHuntZombie {
	public class FSMGameUpdateState : FSMBaseState {

		protected CGameManager m_GameManager;

		public FSMGameUpdateState(IContext context) : base (context)
		{
			this.m_GameManager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
			this.m_GameManager.UpdateGame ();
		}

		public override void ExitState()
		{
			base.ExitState ();
		}

	}
}
