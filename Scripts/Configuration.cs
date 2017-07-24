using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;

public class Configuration : MonoBehaviour {
	public string remoteUrl = "http://www.iyoovr.com/hsyx";
	public string language = Language.Chinese;
	public Text message;
	private string configStr;

	// Use this for initialization
	void Start () {
		Request.RemoteUrl = remoteUrl;
		StartCoroutine (readConfig ());
		message.text = "";
	}

	IEnumerator readConfig ()
	{
		yield return Config.LoadConfig ("ui/config.xml");


		yield return Request.ReadPersistent ("ui/config.xml", str=>configStr = str);
		if (!String.IsNullOrEmpty (configStr)) {
			yield return I18n.Initialise (language);
			GetComponent<StartSceneController> ().OnLoaded();
		} else {
			if (I18n.language == Language.Chinese) {
				message.text = "初始化失败，请检查网络连接";
			}else
				message.text = "Failed to initialise, please check your Internet Connection";
		}
	}
}
