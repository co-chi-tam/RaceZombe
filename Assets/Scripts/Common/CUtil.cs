﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RacingHuntZombie {
	public static class CUtil {

		/// <summary>
		/// Gets the object controller.
		/// </summary>
		public static T GetObjectController<T>(this GameObject value) where T : CObjectController {
			var controller = value.GetComponent<CObjectController> (); // object
			if (controller == null) {
				controller = value.GetComponentInParent<CObjectController> (); // parent/object
			}
			if (controller == null) {
				controller = value.transform.root.GetComponent<CObjectController> (); // root/parent/object
			}
			return controller as T;
		}

		/// <summary>
		/// Gets the custom component.
		/// </summary>
		public static T GetCustomComponent<T>(this GameObject value) where T : CComponent {
			var controller = value.GetComponent<CObjectController> (); // object
			if (controller == null)
				return default(T);
			return controller.GetCustomComponent<T>();
		}

		/// <summary>
		/// To the vector2 .
		/// Exp: x|y
		/// </summary>
		public static Vector2 ToVector2(this string value) {
			var strSplit = value.Split ('|');
			var v2 = Vector2.zero;
			if (strSplit.Length == 2) {
				v2.x = float.Parse (strSplit [0]);
				v2.y = float.Parse (strSplit [1]);
			}
			return v2;
		}

		/// <summary>
		/// To the vector3 .
		/// Exp: x|y|z
		/// </summary>
		public static Vector2 ToVector3(this string value) {
			var strSplit = value.Split ('|');
			var v3 = Vector3.zero;
			if (strSplit.Length == 3) {
				v3.x = float.Parse (strSplit [0]);
				v3.y = float.Parse (strSplit [1]);
				v3.z = float.Parse (strSplit [2]);
			}
			return v3;
		}

		/// <summary>
		/// To the ticks.
		/// </summary>
		public static long ToTicks(this long value) {
			var ticks = ((value * 10000) + 621355968000000000);
			return ticks;
		}

		/// <summary>
		/// To the timer.
		/// </summary>
		public static long ToTimer(this long value) {
			var timer = (value - 621355968000000000) / 10000;
			return timer;
		}

		/// <summary>
		/// To the byte array.
		/// </summary>
		public static byte[] ToByteArray(this object obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

	}
}
