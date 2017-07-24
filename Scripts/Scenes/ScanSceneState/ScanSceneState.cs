 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using Vuforia;
using UnityEngine.SceneManagement;
using System.Xml.Linq;

public class ScanSceneState
{
	public ScanSceneController scene;
	public string name;
	//public Dictionary<string, ScanSceneState> states;

	public static ScanSceneState GetState(string name){
		if (name == "idle")
			return new ScanIdleState ();
		else if(name == "object")
			return new ScanObjectState ();
		else if(name == "video")
			return new ScanVideoState ();
		else if(name == "menuvideo")
			return new ScanMenuVideoState ();
		else if(name == "menuobject")
			return new ScanMenuObjectState ();
		else
			return new ScanMenuState ();
	}

	public virtual void OnTrackFound(){

	}

	public virtual void OnTrackLost(){

	}

	public virtual void OnBackClick(){
		ScanSceneController.instant.exited = true;
		OnExit ();
		SceneManager.LoadScene ("Selection");
	}

	public virtual void OnEnter(Hashtable args = null){
		if (ScanSceneController.currentTrackableObject == null)
			return;
		CustomTrackableEventHandler cteh = ScanSceneController.currentTrackableObject.GetComponent<CustomTrackableEventHandler> ();
		if (cteh) {
			ScanSceneController.instant.subtitle.Play (cteh.subtitlePath);
		}
	}

	public virtual void OnExit(){
		ScanSceneController.instant.subtitle.Stop ();
	}
}
