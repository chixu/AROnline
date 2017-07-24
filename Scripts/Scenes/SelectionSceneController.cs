using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System.IO;
using UnityEngine.UI;

public class SelectionSceneController : MonoBehaviour
{
	public GameObject selectionItem;
	public GameObject itemsPanel;
	public ProgressPanel progressPanel;
	public OKCancelPanel okCancelPanel;
	private XElement layout;
	private List<GameObject> selectionItems;
	// Use this for initialization
	private bool enabled = true;

	void Start ()
	{
//		Debug.Log ("Start");
		selectionItems = new List<GameObject> ();
		progressPanel.onCancelHandler = () => {
			Config.forceBreak = true;
			progressPanel.Hide();
		};
		StartCoroutine (initScene ());
	}

	IEnumerator initScene ()
	{
		yield return Request.ReadPersistent ("ui/ui.xml", LayoutLoaded);
		if (layout != null) {
			XElement itemsEle = layout.Element ("items");
			var items = itemsEle.Elements ();
			int index = 0;
			foreach (XElement item in items) {
				string desc = item.Attribute ("desc").Value;
				string icon = item.Attribute ("icon").Value;
				Debug.Log (item);
				GameObject obj = GameObject.Instantiate (selectionItem);
				//obj.transform.r
				//obj.transform.parent = itemsPanel.gameObject.transform;
				//obj.GetComponent<RectTransform> ().localPosition = Vector3.zero;
				obj.transform.SetParent(itemsPanel.transform, false);
				RectTransform rectT = obj.GetComponent<RectTransform> ();
				rectT.localPosition = new Vector3 (rectT.localPosition.x, rectT.localPosition.y-170 * index);
				selectionItems.Add (obj);

				SelectionItem itemComp = obj.GetComponent<SelectionItem> ();
				itemComp.name = desc;
				itemComp.text.text = I18n.Translate (desc);
				itemComp.SetOnClick (OnItemClick);
//				WWW www = new WWW(Path.Combine(Application.persistentDataPath, "ui/"+icon));
//				itemComp.image.sprite = Sprite.Create(www.texture, new Rect(0,0,www.texture.width, www.texture.height), new Vector2(0,0));
				StartCoroutine(LoadIcon ("ui/"+icon, itemComp.image));
				index++;
			}
		}
	}

	bool Enabled{
		get{
			return enabled;
		}
		set{
			enabled = value;
			for (int i = 0; i < selectionItems.Count; i++) {
				Button btn = selectionItems [0].GetComponent<Button> ();
				btn.interactable = enabled;
			}
		}
	}

	void OnItemClick(SelectionItem item){
		StartCoroutine (OnItemClickHandler (item.name));
	}

	IEnumerator OnItemClickHandler(string name){
		Logger.Log (name + " clicked");
		okCancelPanel.Reset ();
		Enabled = false;
		yield return Config.LoadConfig (name + "/config.xml", FileLoaded, okCancelPanel);
		Enabled = true;
		if (!Config.forceBreak && !okCancelPanel.isCancel) {
			Hashtable arg = new Hashtable ();
			arg.Add ("name", name);
			arg.Add ("data", Xml.GetChildByAttribute(layout.Element ("items"), "desc", name));
			SceneManagerExtension.LoadScene ("Scan", arg);
		}
	}

	void FileLoaded(int idx, int total){
		if (idx == 0) {
			progressPanel.Show (total);
			return;
		}
		progressPanel.Load (idx);
		if (idx == total) {
			progressPanel.Hide ();
		}
	}

	IEnumerator LoadIcon(string url, Image image){
		//Debug.Log (Path.Combine ("file:////"+ Application.persistentDataPath, url));
		WWW www = new WWW(Request.ResolvePath(Application.persistentDataPath + "/" + url));
		yield return www;
		image.sprite = Sprite.Create(www.texture, new Rect(0,0,www.texture.width, www.texture.height), new Vector2(0,0));
	}

	void OnGUI ()
	{
//		for (int i = 0; i < selectionItems.Count; i++) {
//			selectionItems [i].GetComponent<RectTransform> ().localPosition = Vector3.zero;
//		}
	}

	void LayoutLoaded (string str)
	{
		layout = XDocument.Parse (str).Root;
	}
}
