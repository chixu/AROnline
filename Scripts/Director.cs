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

	public static void Initialize(XElement config){
		trackerManager = new TrackerManager ();
		var trackers = config.Element ("trackers").Elements();
		foreach (XElement n in trackers) {
			string name = Xml.Attribute (n, "name");
			Logger.Log (name, "blue");
			if(name == "unity")
				trackerManager.AddTracker (new UnityTracker ());
			else if(name == "umeng")
				trackerManager.AddTracker (new UmengTracker ());
		}

		var trackevents = config.Element ("trackevents").Elements();
		foreach (XElement n in trackevents) {
			string name = Xml.Attribute (n, "name");
			Logger.Log (name, "blue");
			trackerManager.eventBlacklist.Add (name, name);
		}

	}
}
