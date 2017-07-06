using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace RacingHuntZombie {
	public interface ISimpleContext : IContext {

		bool HaveEnemy ();
		bool IsDeath();
		
	}
}
