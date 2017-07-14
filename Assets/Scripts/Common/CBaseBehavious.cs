using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseBehavious : MonoBehaviour {

	#region Properties

	protected Transform m_Transform;

	#endregion 

	#region Implementation MonoBehaviour

	protected virtual void OnEnable() {
	
	}

	protected virtual void OnDiable() {
	
	}

	protected virtual void Awake() {
		this.m_Transform = this.transform;
	}

	protected virtual void Start() {
		
	}

	protected virtual void FixedUpdate() {
		this.FixedUpdateBaseTime (Time.fixedDeltaTime);
	}

	protected virtual void FixedUpdateBaseTime (float dt)
	{

	}

	protected virtual void Update() {
		this.UpdateBaseTime (Time.deltaTime);
	}

	protected virtual void UpdateBaseTime (float dt)
	{
		
	}

	protected virtual void LateUpdate() {
		this.LateUpdateBaseTime (Time.deltaTime);
	}

	protected virtual void LateUpdateBaseTime (float dt) {

	}

	#endregion

}

