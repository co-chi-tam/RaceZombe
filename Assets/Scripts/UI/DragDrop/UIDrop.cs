using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class UIDrop : UIMember, IDropHandler {

	#region Properties

	[SerializeField]	public UIGroup group;
	[SerializeField]	private EDropState m_EDropState = EDropState.Free;
	[SerializeField]	private GameObject m_Result;

	public enum EDropState
	{
		Free 	= 0,
		Dropped = 1
	}

	private Button m_CancelDropButton;
	private IResult m_IResultRepair;

	public GameObject cloneDragableObject;
	public UIDrag dragableObject;
	public Action<Vector2, IResult> OnEventDrop;
	public Action<Vector2, IResult> OnEventEndDrop;
	public Action<Vector2, IResult> OnEventCancelDrop;

	#endregion

	#region Monobehaviour

	protected override void Awake ()
	{
		base.Awake ();
		m_CancelDropButton = this.GetComponent<Button> ();
		if (m_Result != null) {
			m_IResultRepair = m_Result.GetComponent<IResult> ();
		}
	}

	#endregion

	#region IDropHandler implementation

	public virtual void OnDrop (PointerEventData eventData)
	{
		OnItemDrop (eventData.position);
		if (m_EDropState == EDropState.Free) {
			
		} else {
			OnItemCancelDrop(Input.mousePosition, m_IResultRepair);
		}
		SetDropObject (eventData.pointerDrag, eventData.position);
	}

	#endregion

	#region IMember implementation

	public override IResult GetAlreadyResult ()
	{
		base.GetAlreadyResult ();
		if (dragableObject != null && m_EDropState == EDropState.Dropped) {
			return dragableObject.GetAlreadyResult();
		} 
		return null;
	}

	public override IResult GetResultObject ()
	{
		return m_IResultRepair;
	}

	public override void Clear ()
	{
		base.Clear ();
		ClearDropObject ();
	}

	#endregion

	#region Main methods

	protected virtual void OnItemDrop(Vector2 position) {
		
	}

	protected virtual void OnItemEndDrop(Vector2 position, IResult result) {
		CloneDragObject ();
		if (OnEventEndDrop != null) {
			OnEventEndDrop (position, result);
		}
	}

	protected virtual void OnItemCancelDrop(Vector2 position, IResult result) {
		ClearDropObject ();
		if (OnEventCancelDrop != null) {
			OnEventCancelDrop (position, result);
		}
	}

	public void SetDropObject(GameObject dropObject, Vector2 position) {
		dragableObject = dropObject.GetComponent<UIDrag> ();
		if (dragableObject != null && dragableObject.group == this.group) {
			dragableObject.OnEventEndDrag -= OnItemEndDrop;
			dragableObject.OnEventEndDrag += OnItemEndDrop;
			dragableObject.dropableObject = this;
			m_CancelDropButton.onClick.RemoveAllListeners ();
			m_CancelDropButton.onClick.AddListener (() => {
				OnItemCancelDrop(Input.mousePosition, m_IResultRepair);
			});
			m_EDropState = EDropState.Dropped;
			if (OnEventDrop != null) {
				OnEventDrop (position, m_IResultRepair);
			} 
		} 
	}

	private void CloneDragObject() {
		dragableObject.enabled = false;
		cloneDragableObject = Instantiate(dragableObject.content);
		var contentRectTransform = cloneDragableObject.transform as RectTransform;
		contentRectTransform.SetParent (this.transform);
		contentRectTransform.localPosition = Vector3.zero;
		contentRectTransform.localScale = Vector3.one;
		contentRectTransform.anchorMin = Vector2.zero;
		contentRectTransform.anchorMax = Vector2.one;
		contentRectTransform.anchoredPosition = Vector2.zero;
		contentRectTransform.sizeDelta = Vector2.one;
		dragableObject.enabled = true;
		dragableObject.content.SetActive (false);
	}

	public void ClearDropObject() {
		if (cloneDragableObject != null) {
			DestroyImmediate (cloneDragableObject);
		}
		if (dragableObject != null) {
			dragableObject.enabled = true;
			dragableObject.content.SetActive (true);
			dragableObject.OnEventEndDrag -= OnItemEndDrop;
			dragableObject.dropableObject = null;
			cloneDragableObject = null;
			dragableObject = null;
			m_CancelDropButton.onClick.RemoveAllListeners ();
		}
		m_EDropState = EDropState.Free;
		if (m_IResultRepair != null) {
			m_IResultRepair.Clear ();
		}
	}

	public void SetState(EDropState state) {
		m_EDropState = state;
	}

	#endregion

}
