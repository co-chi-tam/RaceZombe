using UnityEngine;
using System.Collections;
using FSM;

namespace RacingHuntZombie {
	public class FSMActiveState : FSMBaseState
	{
		protected CObjectController m_Controller;

		public FSMActiveState(IContext context) : base (context)
		{
			this.m_Controller = context as CZombieController;
		}

		public override void StartState()
		{
			base.StartState ();
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
			this.m_Controller.ChasingTarget (dt);
		}

		public override void ExitState()
		{
			base.ExitState ();
		}
	}
}
