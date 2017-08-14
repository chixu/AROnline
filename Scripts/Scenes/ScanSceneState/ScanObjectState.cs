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

public class ScanObjectState:ScanSceneState
{
	private AudioSource audio;

	public ScanObjectState(){
		name = "object";
	}

	public override void OnEnter(Hashtable args = null){
		base.OnEnter ();
		audio = ScanSceneController.currentTrackableObject.GetComponent<AudioSource> ();
		TrackStart (ScanSceneController.currentTrackableObject.name, "Object");
		if (audio)
			audio.Play ();
	}

	public override void OnExit(){
		TrackEnd (ScanSceneController.currentTrackableObject.name, "Object");
		if (audio)
			audio.Stop ();
		base.OnExit ();
	}
}
