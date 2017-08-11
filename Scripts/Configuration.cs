using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;
using System.Xml.Linq;

public enum Environment{
	Production, Development
}

public class Configuration : MonoBehaviour {
	public static Configuration instant;
	[Header("Production")]
	public string prodRemoteUrl = "http://www.iyoovr.com/hsyx";
	[Header("Development")]
	public string devRemoteUrl = "http://www.iyoovr.com/hsyx";

	public string language = Language.Chinese;
	public bool enablePopupVideo = true;
	public Text message;
	public Environment environment;

	private string configStr;


	void Awake(){
		Configuration.instant = this;
		if (environment == Environment.Development) {
			Request.RemoteUrl = devRemoteUrl;
		} else {
			Request.RemoteUrl = prodRemoteUrl;
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (readConfig ());
		message.text = "";
	}

	IEnumerator readConfig ()
	{
		yield return Config.LoadConfig ("ui/config.xml");
		yield return Request.ReadPersistent ("ui/config.xml", str=>configStr = str);
		if (!String.IsNullOrEmpty (configStr)) {
			yield return I18n.Initialise (language);
			Director.Initialize (XDocument.Parse(configStr).Root);
			yield return Director.Load ();
			GetComponent<StartSceneController> ().OnLoaded();
		} else {
			if (I18n.language == Language.Chinese) {
				message.text = "初始化失败，请检查网络连接";
			}else
				message.text = "Failed to initialise, please check your Internet Connection";
		}
	}
}
