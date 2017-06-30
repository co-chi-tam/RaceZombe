using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CBaseController : MonoBehaviour {

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
		}

		protected virtual void Update()
		{
			this.UpdateComponents (Time.deltaTime);
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

		protected virtual void OnCollisionEnter(Collision collision) {
			
		}

		protected virtual void OnCollisionStay(Collision collision) {

		}

		protected virtual void OnTriggerEnter(Collider collider) {

		}

		protected virtual void OnTriggerStay(Collider collider) {

		}
		
	}
}
