using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InstantiateMenu : MonoBehaviour {

	public static int mapL;
	public static string name;
	public static int started;
	public static int mapsDone=1;
	public static int score=0;
	public GameObject playButton;
	public GameObject imageTitle;
	public GameObject returnButton;
	public GameObject panel;
	public GameObject playerShow;
	public GameObject rankingButton;
	public GameObject inputText;
	public GameObject text;
	// Use this for initialization
	void Start () {
		playButton = GameObject.Find ("Canvas/PlayButton");
		rankingButton = GameObject.Find ("Canvas/RankingButton");
		imageTitle = GameObject.Find ("Canvas/Title");
		panel = GameObject.Find ("Panel");
		returnButton = GameObject.Find ("Canvas/ReturnMenuButton");
		playerShow = GameObject.Find ("MaleFreeSimpleMovement1");
		inputText = GameObject.Find ("Canvas/EnterName");
		text = GameObject.Find ("Canvas/EnterName/InputField/Text");
		Debug.Log (started);
		if (started == 2) {
			goOnMaps ();
			
		} else if (started == 1) {
			goOnMainMenu();
		} else {
			goOnInput ();
		}

		addListener ();
		colourButtons ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goOnMaps(){
		playButton.SetActive(false);
		rankingButton.SetActive(false);
		imageTitle.SetActive(false);
		returnButton.SetActive (true);
		panel.SetActive (true);
		playerShow.SetActive (true);
		inputText.SetActive(false);
	}

	public void goOnMainMenu(){
		playButton.SetActive(true);
		rankingButton.SetActive(true);
		imageTitle.SetActive(true);
		returnButton.SetActive (false);
		panel.SetActive (false);
		playerShow.SetActive (false);
		inputText.SetActive(false);
		started = 1;
	}

	public void goOnMainMenuFirst(){
		started=1;
		name = text.GetComponent<Text> ().text;
		Debug.Log ("holas");
		Debug.Log (name);
		goOnMainMenu ();
	}

	private void SubmitName(string arg0)
	{
		name = arg0;
	}

	public void goOnInput(){
		playButton.SetActive(false);
		rankingButton.SetActive(false);
		imageTitle.SetActive(true);
		returnButton.SetActive (false);
		panel.SetActive (false);
		playerShow.SetActive (false);
		inputText.SetActive(true);
	}

	public void goGame(string s){
		mapL = int.Parse(s);
		Application.LoadLevel ("Game");

	}

	void addListener(){
		foreach (Transform child in panel.transform) {
			child.gameObject.transform.GetComponent<Button> ().onClick.AddListener (() => goGame(child.gameObject.name));
			child.gameObject.transform.GetComponent<Button> ().interactable = false;

		}
	}

	void colourButtons(){
		int mapping=0;
		foreach (Transform child in panel.transform) {
			if(mapping<mapsDone)
			child.gameObject.transform.GetComponent<Button> ().interactable = true;
			mapping++;
		}
	}

	public void goRanking(){
		Application.LoadLevel ("Ranking");

	}
}
