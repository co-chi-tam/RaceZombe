﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingHuntZombie {
	public class CZombieExplosionController : CZombieController {

		#region Properties

		[SerializeField]	protected CExplosionObject m_ExplosionObject;

		#endregion

		#region Implementation MonoBehaviour

		public override void Init() {
			this.m_ExplosionObject.Init (this.m_Transform, this.m_Data.currentDamage);
			base.Init ();
		}

		#endregion

		#region Implementation Controller

		protected override void RegisterComponent ()
		{
			base.RegisterComponent ();
			this.m_Components.Add (this.m_ExplosionObject);
		}

		protected override void ChasingTarget(float dt) {
			base.ChasingTarget (dt);
			if (this.m_MovableObject.IsNearTarget () == true) {
				this.m_ExplosionObject.WorldExplosion ((contactRigidbody) => {
					var controller = contactRigidbody.GetComponent<CObjectController>();
					if (controller != null && controller != this) {
						controller.ApplyDamage (this, this.m_ExplosionObject.GetDamage());
					}
					return controller != null;
				});
				// Auto destroy.
				this.ApplyDamage (this, this.m_Data.currentDurability);
			}
		}

		#endregion
		
	}
}
