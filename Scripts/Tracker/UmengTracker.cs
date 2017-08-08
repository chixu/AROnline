using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmengTracker: ITracker {

	public void Initialize(){

	}

	public void TrackEvent(string eventName, Dictionary<string, object> data){
		Debug.Log ("UmengTracker " + eventName);
	}
}
