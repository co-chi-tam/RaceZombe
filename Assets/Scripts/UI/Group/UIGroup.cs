using UnityEngine;
using System.Collections;

public class UIGroup : CUIBaseMonobehaviour, IGroup {

	#region Properties

	[SerializeField]	private string m_GroupName = "group 1";
	[SerializeField]	public UIMember[] members;

	#endregion

	#region Monobehaviour

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void UpdateBaseTime (float dt)
	{
		base.UpdateBaseTime (dt);
	}

	#endregion

	#region IGroup implementation

	public virtual object[] GetObjectResults() {
		var objs = new object[members.Length];
		for (int i = 0; i < members.Length; i++) {
			objs [i] = members [i];
		}
		return objs;
	}

	#endregion

	#region IResult implementation

	public virtual void SetObject(object value) {
		
	}

	public virtual object GetObject() {
		return null;
	}

	public virtual void Clear() {
		
	}

	#endregion

}
