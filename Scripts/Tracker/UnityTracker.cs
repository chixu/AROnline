using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnityTracker: ITracker {

	public void Initialize(){

	}

	public void TrackEvent(string eventName, Dictionary<string, object> data){
		Logger.Log ("UnityTracker " + eventName + " " + Print.Dictionary(data), "blue");
		Analytics.CustomEvent(eventName, data);
	}
}
