using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SelectionItem : MonoBehaviour {

	public Text text;
	public Image image;
	private Action<SelectionItem> onClickHandler;
	// Use this for initialization


	public void SetOnClick(Action<SelectionItem> handler){
		onClickHandler = handler;
	}
		
//	public void OnClick(){
//		StartCoroutine (OnClickHandler ());
//	}

	public void OnClick(){
		if (onClickHandler != null)
			onClickHandler.Invoke (this);
	}

	//public void Init
}
