using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RacingHuntZombie {
	[Serializable]
	public class CNavigateMovableObject: CMovableObject {

		#region Properties

		[SerializeField]	protected NavMeshAgent m_NavMeshAgent;

		protected NavMeshPath m_NavMeshPath;

		#endregion

		#region Implementation Component

		public new void Init(float speed, Transform mTransform) {
			base.Init (speed, mTransform);
			this.m_NavMeshAgent.speed = speed;
			this.m_NavMeshPath = new NavMeshPath ();
		}

		#endregion

		#region Main methods

		public override void Move (float dt) {
//			base.Move (dt);
			if (this.m_NavMeshPath == null)
				return;
			if (this.m_NavMeshAgent.CalculatePath (this.targetPosition, this.m_NavMeshPath) == false)
				return;
			var direction = this.targetPosition - this.m_CurrentTransform.position;
			var angle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			var position = direction.normalized * this.GetMoveSpeedSample(dt);
			var destinationPosition = this.m_CurrentTransform.position + position;
			this.m_NavMeshAgent.destination = (destinationPosition);
		}

		public virtual void MoveDestination(float dt) {
			this.m_NavMeshAgent.isStopped = false;
			this.m_NavMeshAgent.destination = this.targetPosition;
			if (this.OnMoved != null) {
				this.OnMoved.Invoke ();
			}
		}

		public override void Stop () {
//			base.Stop ();
			this.m_NavMeshAgent.isStopped = true;
		}

		public override bool IsOutOfRange() {
			base.IsOutOfRange ();
			if (this.m_NavMeshPath == null)
				return false;
			if (this.m_NavMeshAgent.isOnNavMesh == false)
				return true;
			if (this.m_NavMeshAgent.CalculatePath (this.m_NavMeshAgent.destination, this.m_NavMeshPath) == false)
				return true;
			return base.IsOutOfRange();
		}

		public override bool IsNearTarget() {
			base.IsNearTarget ();
			if (this.m_NavMeshPath == null)
				return false;
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

		#endregion
		
	}
}
