using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RacingHuntZombie {
	[Serializable]
	public class CNavigateMovableObject: CMovableObject {

		[SerializeField]	private NavMeshAgent m_NavMeshAgent;
		[SerializeField]	private Collider m_Collider;

		private NavMeshPath m_NavMeshPath;

		public new void Init(float min, float max, float speed) {
			base.Init (speed, this.m_NavMeshAgent.transform);
			this.m_NavMeshAgent.speed = speed;
			this.m_NavMeshPath = new NavMeshPath ();
			this.m_CurrentTransform = this.m_NavMeshAgent.transform;
		}

		public virtual void MoveDestination(float dt) {
			this.m_NavMeshAgent.isStopped = false;
			this.m_NavMeshAgent.destination = this.targetPosition;
		}

		public override void Stop ()
		{
//			base.Stop ();
			this.m_NavMeshAgent.isStopped = true;
		}

		public override bool IsOutOfRange() {
			base.IsOutOfRange ();
			if (this.m_NavMeshAgent.isOnNavMesh == false)
				return true;
			if (this.m_NavMeshAgent.CalculatePath (this.m_NavMeshAgent.destination, this.m_NavMeshPath) == false)
				return true;
			return base.IsOutOfRange();
		}

		public override bool IsNearTarget() {
			base.IsNearTarget ();
			if (this.m_NavMeshAgent.isOnNavMesh == false)
				return false;
			if (this.m_NavMeshAgent.CalculatePath (this.m_NavMeshAgent.destination, this.m_NavMeshPath) == false)
				return false;
			return base.IsNearTarget();
		}

		public virtual bool CanMoveTo(Vector3 position, ref Vector3[] path) {
			var result = this.m_NavMeshAgent.CalculatePath (position, this.m_NavMeshPath);
			path = this.m_NavMeshPath.corners;
			return result;
		}
		
	}
}
