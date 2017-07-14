using UnityEngine;
using System.Collections;
using FSM;

namespace RacingHuntZombie {
	public class FSMGameCloseState : FSMBaseState {

		protected CGameManager m_GameManager;

		public FSMGameCloseState(IContext context) : base (context)
		{
			this.m_GameManager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
			this.m_GameManager.CloseGame ();
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
		}

		public override void ExitState()
		{
			base.ExitState ();
		}

	}
}
