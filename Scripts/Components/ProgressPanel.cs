using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ProgressPanel : MonoBehaviour {

	public ProgressBar bar;
	public Text desc;
	public Text btnText;
	public int totalAssets;
	public Action onCancelHandler; 
	//public int curretAssets;

	public void Start(){
		btnText.text = I18n.Translate ("cancel");
	}

	public void Show(int total = 5){
		totalAssets = total;
		bar.maxValue = total;
		Load (0);
		this.gameObject.SetActive (true);
		desc.text = I18n.Translate ("loading_desc");
	}

	public void Hide(){
		this.gameObject.SetActive (false);
	}

	public void Load (int n){
		bar.SetValue (n);
		desc.text = I18n.Translate ("loading_desc") + n.ToString() + "/" + totalAssets.ToString();
	}

	public void OnCancelClick(){
		if (onCancelHandler != null)
			onCancelHandler.Invoke ();
	}
}
