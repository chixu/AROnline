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
using UnityEngine.Video;

public class ScanVideoState:ScanSceneState{
    //private VideoPlayer vp;

    public ScanVideoState(){
		name = "video";
	}
		
	public override void OnEnter (Hashtable args = null)
	{
		base.OnEnter(args);
        GameObject curr = ScanSceneController.currentTrackableObject.transform.GetChild(0).gameObject;
        string path = ScanSceneController.currentTrackableObject.GetComponent<CustomTrackableEventHandler>().videoPath;
        Logger.Log(path, "purple");
        scene.description.gameObject.SetActive (false);
		VideoController.instant.Play(curr, path);
    }

	public override void OnExit(){
		base.OnExit ();
		scene.description.gameObject.SetActive (true);
		VideoController.instant.Stop ();
    } 
}
