using UnityEngine;
using System.Collections;
using FSM;

namespace RacingHuntZombie {
	public class FSMDeathState : FSMBaseState
	{
		protected float m_Countdown = 3f;
		protected CObjectController m_Controller;
		protected CBreakableObject m_BreakableObject;

		public FSMDeathState(IContext context) : base (context)
		{
			this.m_Controller = context as CZombieController;
			this.m_BreakableObject = this.m_Controller.GetCustomComponent<CBreakableObject> ();
		}

		public override void StartState()
		{
			base.StartState ();
			this.m_BreakableObject.BreakObjects (true);
			this.m_Controller.SetActive (false);
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
			this.m_Countdown -= dt;
			if (this.m_Countdown <= 0f) {
				this.m_Controller.DestroyObject ();
			}
		}

		public override void ExitState()
		{
			base.ExitState ();
		}
	}
}
