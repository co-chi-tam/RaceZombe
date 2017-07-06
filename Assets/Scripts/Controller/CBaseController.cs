using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CBaseController : MonoBehaviour, IActiveObject {

		[SerializeField]	protected bool m_Active = false;

		protected Transform m_Transform;

		protected virtual void Awake() {
			this.m_Transform = transform;
		}

		protected virtual void Start() {
			
		}

		protected virtual void Update()
		{
			
		}

		protected virtual void LateUpdate()
		{

		}

		protected virtual void OnDestroy() {
			
		}

		public virtual void DestroyObject() {
			Destroy (this.gameObject);
		}

		public virtual void InteractiveOrtherObject (GameObject contactObj) {
			
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

		public virtual void SetActive(bool value) {
			this.m_Active = value;
		}

		public virtual bool GetActive() {
			return this.m_Active;
		}

	}
}
