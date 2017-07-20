using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class UIPointer : UIMember, IPointerDownHandler, IPointerUpHandler {

	public Action<Vector2> OnEventPointerDown;
	public Action<Vector2> OnEventPointerStay;
	public Action<Vector2> OnEventPointerUp;

	protected bool m_PointerStay = false;
	protected PointerEventData m_PointerEventData;

	#region IPointerHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		OnItemFocusEnter (eventData.position);
		m_PointerStay = true;
		if (OnEventPointerDown != null) {
			OnEventPointerDown (eventData.position);
		}
		m_PointerEventData = eventData;
	}

	public void OnPointerStay(PointerEventData eventData) {
		OnItemFocusStay (eventData.position);
		if (OnEventPointerStay != null) {
			OnEventPointerStay (eventData.position);
		}
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		OnItemFocusExit (eventData.position);
		m_PointerStay = false;
		if (OnEventPointerUp != null) {
			OnEventPointerUp (eventData.position);
		}
		m_PointerEventData = null;
	}

	#endregion

	#region Monobehaviour

	protected override void UpdateBaseTime (float dt)
	{
		base.UpdateBaseTime (dt);
		if (m_PointerStay) {
			OnPointerStay (m_PointerEventData);
		}
	}

	protected void OnDestroy() {
		this.Clear ();
	}

	#endregion

	#region Main methods

	protected virtual void OnItemFocusEnter(Vector2 position) {

	}

	protected virtual void OnItemFocusStay(Vector2 position) {

	}

	protected virtual void OnItemFocusExit(Vector2 position) {

	}

	#endregion
}
