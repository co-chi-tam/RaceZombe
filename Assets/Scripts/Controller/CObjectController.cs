using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CObjectController : CBaseController, ISimpleContext {

		#region Properties

		[Header ("Object")]
		[SerializeField]	protected Collider m_Collider;
		[SerializeField]	protected Animator m_Animator;
		[SerializeField]	protected AudioSource m_AudioSource;
		[SerializeField]	protected bool m_AutoInit = false;
		[Header ("Control")]
		[SerializeField]	protected LayerMask m_ExcepLayerMask;
		[SerializeField]	protected CObjectController m_TargetController;

		protected List<CComponent> m_Components;
		protected CTimerComponent m_TimerComponent;

		#endregion

		#region Implementation MonoBehaviour

		protected override void Awake() {
			if (this.m_AutoInit) {
				this.Init ();
			}
			base.Awake ();
			this.m_Components = new List<CComponent> ();
			this.m_TimerComponent = new CTimerComponent ();
			this.RegisterComponent ();
		}

		public override void Init() {
			base.Init ();
		}

		protected override void Start ()
		{
			base.Start ();
			this.m_TimerComponent.Init ();
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].StartComponent ();
			}
		}

		protected override void Update()
		{
			base.Update ();
			this.UpdateComponents (Time.deltaTime);
		}

		protected override void LateUpdate()
		{
			base.LateUpdate ();
		}

		protected override void OnDestroy() {
			base.OnDestroy ();
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].EndComponent ();
			}
		}

		#endregion

		#region Implementation Controller

		protected virtual void RegisterComponent() {
			this.m_Components.Add (this.m_TimerComponent);
		}

		protected virtual void UpdateComponents(float dt)
		{
			for (int i = 0; i < this.m_Components.Count; i++) {
				this.m_Components [i].UpdateComponent (dt);
			}
		}

		#endregion

		#region Main methods

		public virtual T GetCustomComponent<T>() where T : CComponent {
			for (int i = 0; i < this.m_Components.Count; i++) {
				if (this.m_Components [i].GetType ().Equals(typeof(T))) {
					return this.m_Components [i] as T;
				}
			}
			return default (T);
		}

		public virtual void ApplyDamage(CObjectController attacker, float damage) {
		
		}

		public virtual void ApplyEngineWear(float wear) {
		
		}

		public override void DestroyObject() {
			base.DestroyObject ();
		}
			
		public override void InteractiveOrtherObject (GameObject thisContantObj, GameObject contactObj) {
			base.InteractiveOrtherObject (thisContantObj, contactObj);
			var isExceptionLayer = this.m_ExcepLayerMask.value == (this.m_ExcepLayerMask.value | (1 << contactObj.gameObject.layer));
			if (isExceptionLayer == false) {
				var controller = contactObj.GetObjectController<CObjectController> ();
				if (controller == null) {
					return;
				}
				if (controller.GetActive() == false) {
					return;
				}
				this.ApplyDamage (controller, controller.GetDamage());
			}
		}

		public virtual void UpdateObject(float dt) {
			
		}

		#endregion

		#region FSM

		public virtual bool HaveGas() {
			return false;
		}

		public virtual bool HaveEnemy() {
			return false;
		}

		public virtual bool IsInactive() {
			return false;
		}

		#endregion

		#region Getter && Setter

		public virtual void SetTimer(float time, Action callback) {
			this.m_TimerComponent.SetTimer (time, callback);
		}

		public virtual void SetTarget(CObjectController target) {
			this.m_TargetController = target;
		}

		public virtual CObjectController GetTarget() {
			return this.m_TargetController;
		}

		public virtual float GetDamage() {
			return 0f;
		}

		public virtual float GetVelocityKMH() {
			return 0f;
		}

		public virtual float GetResistant() {
			return 0f;
		}

		public virtual void SetData(CObjectData value) {
		
		}

		public virtual CObjectData GetData() {
			return null;
		}

		public virtual void SetGas(float value) {
		
		}

		public virtual float GetGas() {
			return 0f;
		}

		public virtual float GetGasPercent() {
			return 1f;
		}

		public virtual float GetDurability() {
			return 0f;
		}

		public virtual void SetDurability(float value) {
		
		}

		public virtual float GetDurabilityPercent() {
			return 1f;
		}

		public virtual float GetCarPartPercent(CCarPartsComponent.ECarPart value) {
			return 1f;
		}

		public virtual void SetMissionObject(string key, float value) {

		}

		public virtual float GetMissionObject(string key) {
			return 0f;
		}

		public virtual float GetMissionPercent() {
			return 1f;
		}

		public virtual Collider GetCollider() {
			return this.m_Collider;
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

		#endregion
		
	}
}
