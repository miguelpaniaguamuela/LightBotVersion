using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class panelTask : MonoBehaviour {

	public Transform momentButton;
	public GameObject play;
	public GameObject returned;
	public GameObject player;
	public GameObject front;
	public GameObject right;
	public GameObject left;
	public GameObject light;

	public GameObject audioButton;
	public GameObject cfront;
	public GameObject cright;
	public GameObject cleft;
	public GameObject clight;
	private int ids;
	List<string> list = new List<string>();
	int instructionsNum;
	int instructionsNow;

	// Use this for initialization
	public void Start () {
		cfront = GameObject.Find ("FrontButton");
		cright = GameObject.Find ("RightButton");
		cleft = GameObject.Find ("LeftButton");
		clight = GameObject.Find ("LightButton");
		player = GameObject.Find ("MaleFreeSimpleMovement1(Clone)");
		play = GameObject.Find ("PlayButton");
		returned = GameObject.Find ("ReturnButton");
		audioButton = GameObject.Find ("AudioButton");
		returned.SetActive (false);
		play.SetActive (true);
		instructionsNum=16;
		instructionsNow=0;
	}

	public void Init(){
		instructionsNow=0;
		returned.SetActive (false);
		play.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		if(player==null)
			player = GameObject.Find ("MaleFreeSimpleMovement1(Clone)");
		
		if (player.GetComponent<PlayerScript> ().getExecuting ()) {
			returned.GetComponent<Button> ().interactable = false;
		} else {
			returned.GetComponent<Button> ().interactable = true;
		}

		disableButtons ();

	}

	public void addRight(){
		GameObject newButton = Instantiate (right) as GameObject;
		addToPanel (newButton, "0 ");
	}

	public void addLeft(){
		GameObject newButton = Instantiate (left) as GameObject;
		addToPanel (newButton, "1 ");
	}

	public void addFront(){
		GameObject newButton = Instantiate (front) as GameObject;
		addToPanel (newButton, "2 ");
	}

	public void addLight(){
		GameObject newButton = Instantiate (light) as GameObject;
		addToPanel (newButton, "3 ");
	}
	void addToPanel(GameObject obj, string num){
		instructionsNow++;
		string s = addToList (num);
		obj.transform.SetParent(this.transform, false);
		obj.GetComponent<Button> ().onClick.AddListener (() => RemoveComponent(s));
		obj.name = s;
	}
	public void RemoveComponent(string num){
		if (!player.GetComponent<PlayerScript>().getExecuting()) {
			Destroy (this.transform.Find (num).gameObject);
			list.Remove (num);
			instructionsNow--;
		}
	}
	public void SendInstructions(){
		player.GetComponent<PlayerScript> ().move (list);
		play.SetActive (false);
		returned.SetActive (true);
	}
	public void goBack(){
		player.GetComponent<PlayerScript> ().goInit ();
		play.SetActive (true);
		returned.SetActive (false);
	}
	string addToList(string order){
		order += ids.ToString ();
		//Debug.Log (list.Count);
		list.Add (order);
		ids = ids + 1;
		return order;
	}
	public void lightPanel(int num){
		if (num > 0) {
			momentButton = this.gameObject.transform.GetChild (num-1);
			momentButton.GetComponent<Image> ().color = Color.white;
		}
		momentButton = this.gameObject.transform.GetChild (num);
		momentButton.GetComponent<Image> ().color = Color.green;
	}
	public void clearPanel(int num){
			momentButton = this.gameObject.transform.GetChild (num-1);
			momentButton.GetComponent<Image> ().color = Color.white;
	}
	public void clearList(){
		list.Clear ();
	}

	public void disableButtons(){
		if (instructionsNow == instructionsNum) {
			cfront.GetComponent<Button> ().interactable = false;
			cright.GetComponent<Button> ().interactable = false;
			cleft.GetComponent<Button> ().interactable = false;
			clight.GetComponent<Button> ().interactable = false;
		} else {
			cfront.GetComponent<Button> ().interactable = true;
			cright.GetComponent<Button> ().interactable = true;
			cleft.GetComponent<Button> ().interactable = true;
			clight.GetComponent<Button> ().interactable = true;
		}
	}

	public void changeSpriteButton(string s){
		Debug.Log ("olii");
		audioButton.GetComponent<Button>().image.sprite= Resources.Load (s, typeof(Sprite)) as Sprite;
	}


}
