using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CBaseController : MonoBehaviour, IActiveObject {

		[Header ("Object")]
		[SerializeField]	protected bool m_Active = false;
		[SerializeField]	protected Collider m_Collider;
		[SerializeField]	protected Animator m_Animator;
		[SerializeField]	protected AudioSource m_AudioSource;

		protected Transform m_Transform;
		protected List<CComponent> m_Components;
		protected float m_CurrentActionDelay = 0f;

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

		public virtual void ApplyDamage(CBaseController attacker, float damage) {
		
		}

		public virtual void ApplyEngineWear(float wear) {
		
		}

		public virtual void DestroyObject() {
			Destroy (this.gameObject);
		}
			
		public virtual void InteractiveOrtherObject (GameObject contactObj) {
			if (contactObj.gameObject.layer != LayerMask.NameToLayer ("Ground")) {
				var controller = contactObj.gameObject.GetObjectController<CBaseController> ();
				if (controller == null) {
					return;
				}
				if (controller.GetActive() == false) {
					return;
				}
				this.ApplyDamage (controller, controller.GetDamage());
			}
		}

		public virtual void StayOrtherObject(GameObject contactObj) {
			
		}

		public virtual void ExitOrtherObject(GameObject contactObj) {

		}

		protected virtual void OnCollisionEnter(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i].otherCollider.gameObject;
				this.InteractiveOrtherObject (contactObj);
			}
		}

		protected virtual void OnCollisionStay(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i].otherCollider.gameObject;
				this.StayOrtherObject (contactObj);
			}
		}

		protected virtual void OnCollisionExit(Collision collision) {
			for (int i = 0; i < collision.contacts.Length; i++) {
				var contactObj = collision.contacts [i].otherCollider.gameObject;
				this.ExitOrtherObject (contactObj);
			}
		}

		protected virtual void OnTriggerEnter(Collider collider) {
			
		}

		protected virtual void OnTriggerStay(Collider collider) {
			
		}

		protected virtual void OnTriggerExit(Collider collider) {
			
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

		public virtual Collider GetCollider() {
			return this.m_Collider;
		}

		public virtual void SetActive(bool value) {
			this.m_Active = value;
		}

		public virtual bool GetActive() {
			return this.m_Active;
		}

		public virtual void SetAnimator(string name, object param = null) {
			if (this.m_Animator == null)
				return;
			if (param is int) {
				this.m_Animator.SetInteger (name, (int)param);
			} else if (param is bool) {
				this.m_Animator.SetBool (name, (bool)param);
			} else if (param is float) {
				this.m_Animator.SetFloat (name, (float)param);
			} else if (param == null) {
				this.m_Animator.SetTrigger (name);
			}
		}

		public virtual void SetAudioPlay() {
			if (this.m_AudioSource == null)
				return;
			this.m_AudioSource.Play ();
		}
		
	}
}
