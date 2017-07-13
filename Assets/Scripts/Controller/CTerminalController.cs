using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CTerminalController : CCarPartController {

		[Header ("Call Object")]
		[SerializeField]	protected CCarPartData m_CallObjectData;

		protected float m_CurrentActionDelay = -1f;

		protected override void Start ()
		{
			base.Start ();
		}

		protected override void LateUpdate ()
		{
			base.LateUpdate ();
			if (this.m_CurrentActionDelay > 0f) {
				this.m_CurrentActionDelay -= Time.deltaTime;
			}
		}

		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj)
		{
			if (this.m_CurrentActionDelay > 0f)
				return;
//			base.InteractiveOrtherObject (contactObj);
			var objController = Instantiate (Resources.Load<CInteractiveCarPartController>("Prefabs/" + this.m_CallObjectData.objectModelPath));
			var objPosition = this.m_Transform.position;
			var objRotation = this.m_Transform.forward;
			objPosition.y = 0f;
			objController.transform.position = objPosition;
			objController.transform.rotation = Quaternion.Euler (objRotation);
			objController.SetData (this.m_CallObjectData);
			objController.SetActive(true);
			objController.SetOwner (this.GetOwner ());
			this.m_CurrentActionDelay = this.m_Data.actionDelay;
		}

		protected override void OnCollisionEnter (Collision collision)
		{
//			base.OnCollisionEnter (collision);
		}

		protected override void OnCollisionStay (Collision collision)
		{
//			base.OnCollisionStay (collision);
		}

		protected override void OnCollisionExit (Collision collision)
		{
//			base.OnCollisionExit (collision);
		}

		protected override void OnTriggerEnter (Collider collider)
		{
//			base.OnTriggerEnter (collider);
		}

		protected override void OnTriggerExit (Collider collider)
		{
//			base.OnTriggerExit (collider);
		}

	}
}
