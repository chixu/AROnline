using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {

	public static void RegisterClickEvent(this GameObject obj){
		obj.AddComponent<OnClick> ();
		Collider collider = obj.GetComponent<Collider> ();
		if (collider == null)
			obj.AddComponent<Collider> ();
	}
}
