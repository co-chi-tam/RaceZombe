using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour {

	[SerializeField]	private Transform m_FollowObject;
	[SerializeField]	private Vector3 m_OffsetPosition;
	[SerializeField]	private Vector3 m_OffsetQuanternion;

	private Transform m_Transform;

	private void Awake() {
		this.m_Transform = this.transform;
	}

	private void LateUpdate() {
		if (this.m_FollowObject == null)
			return;
		var followPosition = this.m_FollowObject.position;
		this.m_Transform.position = this.m_OffsetPosition + followPosition;
			this.m_Transform.rotation = Quaternion.Euler (this.m_OffsetQuanternion);
	}

	public void SetFollower(Transform value) {
		this.m_FollowObject = value;
	}

}
