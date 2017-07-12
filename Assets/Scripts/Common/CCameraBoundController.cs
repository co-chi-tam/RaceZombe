using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CCameraBoundController : MonoBehaviour {

	[SerializeField]	private Collider m_BoundCollider;
	[SerializeField]	private Vector2 m_TopLeftBounds;
	[SerializeField]	private Vector2 m_BottomRightBounds;
	[SerializeField]	private float m_Speed = 20f;
	[SerializeField]	private float m_Damping = 0.5f;

	protected Camera m_Camera;
	protected Transform m_Transform;
	protected Vector3 m_LastMousePos;
	protected Vector3 m_LastTransfromPos;

	void Awake () {
		this.m_Camera = this.GetComponent<Camera> ();
		this.m_Transform = this.transform;
	}

	private void LateUpdate() {
#if UNITY_EDITOR || UNITY_STANDALONE
		this.MoveCameraStandalone ();
#else
		this.MoveCameraMobile ();
#endif
		this.CameraBounds ();
	}

	private void MoveCameraStandalone() {
		if (Input.GetMouseButtonDown (0)) {
			this.m_LastMousePos = this.m_Camera.ScreenToViewportPoint (Input.mousePosition);
			this.m_LastTransfromPos = this.m_Transform.position;
		}
		if (Input.GetMouseButton (0)) {
			var direction = this.m_Camera.ScreenToViewportPoint (Input.mousePosition) - this.m_LastMousePos;
			var movePos = direction.normalized;
			movePos.x = direction.x;
			movePos.y = 0f;
			movePos.z = direction.y;
			this.m_Transform.position = Vector3.Lerp (this.m_Transform.position, this.m_LastTransfromPos + -movePos * this.m_Speed, this.m_Damping);
		}
	}

	private void MoveCameraMobile() {
		if (Input.touchCount < 1)
			return;
		var touch = Input.GetTouch (0);
		switch (touch.phase) {
		case TouchPhase.Began:
			this.m_LastMousePos = this.m_Camera.ScreenToViewportPoint (touch.position);
			this.m_LastTransfromPos = this.m_Transform.position;
			break;
		case TouchPhase.Moved:
		case TouchPhase.Stationary:
			var direction = this.m_Camera.ScreenToViewportPoint (touch.position) - this.m_LastMousePos;
			var movePos = direction.normalized;
			movePos.x = direction.x;
			movePos.y = 0f;
			movePos.z = direction.y;
			this.m_Transform.position = Vector3.Lerp (this.m_Transform.position, this.m_LastTransfromPos + -movePos * this.m_Speed, this.m_Damping);
			break;
		default:
			break;
		}
	}

	private void CameraBounds() {
		var cameraPosition = this.m_Transform.position;
		if (cameraPosition.x < this.m_TopLeftBounds.x || cameraPosition.x > this.m_BottomRightBounds.x
			|| cameraPosition.z > this.m_TopLeftBounds.y || cameraPosition.z < this.m_BottomRightBounds.y) {
			cameraPosition.x = Mathf.Clamp (cameraPosition.x, this.m_TopLeftBounds.x, this.m_BottomRightBounds.x);
			cameraPosition.z = Mathf.Clamp (cameraPosition.z, this.m_BottomRightBounds.y, this.m_TopLeftBounds.y);
			this.m_Transform.position = cameraPosition;
		}
	}

}
