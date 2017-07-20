using UnityEngine;
using System.Collections;

public class UIMember : CUIBaseMonobehaviour, IMember {

	public virtual IResult GetAlreadyResult ()
	{
		return null;
	}

	public virtual IResult GetResultObject () {
		return null;
	}

	public virtual void Clear() {
		
	}

}
