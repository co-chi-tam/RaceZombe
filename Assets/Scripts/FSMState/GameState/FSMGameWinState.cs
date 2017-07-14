using UnityEngine;
using System.Collections;
using FSM;

namespace RacingHuntZombie {
	public class FSMGameWinState : FSMBaseState {

		protected CGameManager m_GameManager;

		public FSMGameWinState(IContext context) : base (context)
		{
			this.m_GameManager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
			this.m_GameManager.WinGame ();
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
