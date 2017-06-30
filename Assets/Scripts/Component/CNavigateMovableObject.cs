using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RacingHuntZombie {
	[Serializable]
	public class CNavigateMovableObject: CComponent {

		[SerializeField]	private NavMeshAgent m_NavMeshAgent;
		[SerializeField]	private Transform m_CurrentTrans;
		[SerializeField]	private Vector3 m_TargetPosition;
		[SerializeField]	private float m_MinDistance = 1f;
		[SerializeField]	private float m_MaxDistance = 10f;

		private NavMeshPath m_NavMeshPath;

		public virtual void Init(float min, float max, Transform currentTrans) {
			this.m_MinDistance = min;
			this.m_MaxDistance = max;
			this.m_CurrentTrans = currentTrans;
		}

		public override void StartComponent ()
		{
			base.StartComponent ();
			this.m_NavMeshPath = new NavMeshPath ();
		}

		public virtual void SetDestination(Vector3 value) {
			this.m_NavMeshAgent.destination = value;
		}

		public virtual Vector3 GetDestination() {
			return this.m_NavMeshAgent.destination;
		}

		public virtual bool IsOutOfRange() {
			if (this.m_NavMeshAgent.isOnNavMesh == false)
				return true;
			if (this.m_NavMeshAgent.CalculatePath (this.m_TargetPosition, this.m_NavMeshPath) == false)
				return true;
			return (Vector3.Distance (this.m_CurrentTrans.position, this.m_TargetPosition) > this.m_MaxDistance);
		}

		public virtual bool IsNearTarget() {
			if (this.m_NavMeshAgent.isOnNavMesh == false)
				return false;
			if (this.m_NavMeshAgent.CalculatePath (this.m_TargetPosition, this.m_NavMeshPath) == false)
				return false;
			return (Vector3.Distance (this.m_CurrentTrans.position, this.m_TargetPosition) < this.m_MinDistance);
		}
		
	}
}
