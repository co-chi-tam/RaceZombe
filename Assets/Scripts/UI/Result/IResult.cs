using UnityEngine;
using System.Collections;

public interface IResult {

	// OBJECT
	void SetObject(object value);
	object GetObject();

	// CLEAR
	void Clear();
}
