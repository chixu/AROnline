using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour {

	public bool loaded = false;
	private bool timeToLoad = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (NextSceneAfterSeconds (5));
	}

	public void NextScene(){
		if (loaded && timeToLoad)
			SceneManager.LoadScene ("Selection");
	}

	public void OnClick(){
		if (loaded)
			SceneManager.LoadScene ("Selection");
	}
	
	IEnumerator NextSceneAfterSeconds(int second){
		yield return new WaitForSeconds (second);
		timeToLoad = true;
		NextScene ();
	}

	public void OnLoaded(){
		loaded = true;
		NextScene ();
	}
}
