using UnityEngine;
using System.Collections;

public class UIResult : CUIBaseMonobehaviour, IResult {

	#region Properties

	[SerializeField]	protected object m_ResultObject;

	#endregion

	#region IResult implementation

	public virtual void SetObject (object value) {
		m_ResultObject = value;
	}

	public virtual object GetObject() {
		return m_ResultObject;
	}

	public virtual void Clear() {
		m_ResultObject = null;
	}

	#endregion

}
