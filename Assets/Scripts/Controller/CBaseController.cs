using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CBaseController : MonoBehaviour, IActiveObject {

		[SerializeField]	private bool m_Active = false;

		protected Transform m_Transform;
		protected List<CComponent> m_Components;

		protected virtual void Awake() {
			this.m_Transform = transform;
			this.m_Components = new List<CComponent> ();
			this.RegisterComponent ();
		}

		protected virtual void Start() {
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].StartComponent ();
			}
			// TEST
			this.m_Active = true;
		}

		protected virtual void Update()
		{
			this.UpdateComponents (Time.deltaTime);
		}

		protected virtual void LateUpdate()
		{
			
		}

		protected virtual void OnDestroy() {
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].EndComponent ();
			}
		}

		protected virtual void RegisterComponent() {
			
		}

		protected virtual void UpdateComponents(float dt)
		{
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].UpdateComponent (dt);
			}
		}

		public virtual T GetCustomComponent<T>() where T : CComponent {
			for (int i = 0; i < this.m_Components.Count; i++) {
				if (this.m_Components [i].GetType ().Equals(typeof(T))) {
					return this.m_Components [i] as T;
				}
			}
			return default (T);
		}

		protected virtual void ApplyDamage(CBaseController attacker, float damage) {
		
		}

		protected virtual void InteractiveOrtherObject(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i].otherCollider;
				if (contactObj.gameObject.layer != LayerMask.NameToLayer ("Ground")) {
					var controller = contactObj.gameObject.GetColliderController<CBaseController> ();
					if (controller == null) {
						continue;
					}
					if (controller.GetActive() == false) {
						continue;
					}
					this.ApplyDamage (controller, controller.GetDamage());
				}
			}
		}

		protected virtual void StayOrtherObject(Collision collision) {

		}

		protected virtual void ExitOrtherObject(Collision collision) {

		}

		protected virtual void OnCollisionEnter(Collision collision) {
			this.InteractiveOrtherObject (collision);
		}

		protected virtual void OnCollisionStay(Collision collision) {
			this.StayOrtherObject (collision);
		}

		protected virtual void OnCollisionExit(Collision collision) {
			this.ExitOrtherObject (collision);
		}

		protected virtual void OnTriggerEnter(Collider collider) {
			
		}

		protected virtual void OnTriggerStay(Collider collider) {

		}

		public virtual float GetDamage() {
			return 0f;
		}

		public virtual float GetVelocityKMH() {
			return 0f;
		}

		public virtual void SetData(CObjectData value) {
		
		}

		public virtual CObjectData GetData() {
			return null;
		}

		public virtual void SetActive(bool value) {
			this.m_Active = value;
		}

		public virtual bool GetActive() {
			return this.m_Active;
		}
		
	}
}
