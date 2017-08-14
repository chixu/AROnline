using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;
using System.Xml.Linq;

public class Director {
	public static string SCAN_OBJECT_PREFIX = "AR-";


	public static TrackerManager trackerManager;
	public static UserBehavior userBehavior;

	public static void Initialize(XElement config){
		trackerManager = new TrackerManager ();
		var trackers = config.Element ("trackers").Elements();
		foreach (XElement n in trackers) {
			string name = Xml.Attribute (n, "name");
			ITracker tracker = null;
			if (name == "unity")
				tracker = new UnityTracker ();
			else if (name == "umeng") {
				string appid = "";
				#if UNITY_IPHONE
				appid = Xml.Attribute (n, "iosid");
				#else
				appid = Xml.Attribute (n, "androidid");
				#endif
				if(!string.IsNullOrEmpty(appid))
					tracker = new UmengTracker (appid);
			}
			if (tracker != null) {
				var events = n.Element ("events");
				if (events != null) {
					var trackevents = events.Elements ();
					foreach (XElement nn in trackevents) {
						string eventname = Xml.Attribute (nn, "name");
						tracker.eventBlacklist.Add (eventname, eventname);
					}
				}
				trackerManager.AddTracker (tracker);
			}
		}
		userBehavior = new UserBehavior ();
	}

	public static IEnumerator Load(){
		yield return userBehavior.Load ();
	}
}
