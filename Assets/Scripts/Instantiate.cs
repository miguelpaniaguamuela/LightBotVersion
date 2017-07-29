using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Instantiate : MonoBehaviour {

	public string level;
	public int lvl;
	public Transform floor;
	public Transform light;
	public Transform player;
	public Transform lightraro;
	public GameObject final;
	public GameObject panel;
	public GameObject audioButton;
	public GameObject camera;
	public GameObject levelText;	
	public GameObject scoreText;

	int objectives;
	int objectivesDone;
	int audioIns;
	private int[,] map = new int[10,10];

	// Use this for initialization

	void Start () {
		lvl = InstantiateMenu.mapL;
		objectivesDone = 0;
		panel = GameObject.Find ("Canvas/Panel");
		level += "maps/";
		level += InstantiateMenu.mapL.ToString ();
		audioIns = 1;
		audioButton = GameObject.Find ("Canvas/AudioButton");
		levelText = GameObject.Find ("Canvas/LevelNum");
		scoreText = GameObject.Find ("Canvas/ScoreNum");
		camera = GameObject.Find ("Camera");
		camera.GetComponent<cameraScript> ().setPosCamera ();
		InstantiateMenu.started = 2;
		chargeLevel (level);

	}
	
	// Update is called once per frame
	void Update () {
		levelText.GetComponent<Text> ().text = lvl.ToString ();
		scoreText.GetComponent<Text> ().text = InstantiateMenu.score.ToString ();
	}

	void InitMap(){
		int lights=0;
		for (int x = 1; x <= (map [0,0]); x++) {
			for (int z = 0; z < map [0,1]; z++) {

				switch (map [x,z])
				{
					case 1:
						(Instantiate (floor, new Vector3 (x, 0, z), Quaternion.identity)).transform.parent = this.gameObject.transform;
						break;
					case 2:
						(Instantiate (light, new Vector3 (x, 0, z), Quaternion.identity)).transform.parent = this.gameObject.transform;
						//lightraro.GetComponent<MeshRenderer> ().material = Blue;
						break;
					default:
						break;
				}
			}
		}
		(Instantiate (player, new Vector3 (map [0,2], 0.75f, map [0,3]), Quaternion.Euler(new Vector3(map [0,4],map [0,5],map [0,6])))).transform.parent = this.gameObject.transform;
		objectives = map [0, 7];
		foreach (Transform child in this.transform) {
			if (child.gameObject.name == "light(Clone)") {
				child.gameObject.name = child.gameObject.transform.position.x+" "+child.gameObject.transform.position.z;
				lights++;
			}
		}
	}

	void readTextFile(string file_path)
	{	
		bool onDuty;
		int line = 0;
		TextAsset txt = Resources.Load (level) as TextAsset;
		string content = txt.text;
		char separator = ' ';

		string[] fLines = Regex.Split ( content, "\r\n" );

		for ( int i=0; i < fLines.Length; i++ ) {
			string valueLine = fLines[i];
			string[] values = Regex.Split ( valueLine, " " ); // your splitter here

			for ( int v=0; v < values.Length; v++ ) {
				string s = values [v];
				map [i, v] = int.Parse (s);

			}
		}

		line = line + 1;

		InitMap ();
	}

	public bool CheckPosition(int xl, int zl){
		if(xl >= 1 && zl >= 0 && xl <= map[0,0] && zl < map[0,1]){
			if (map [xl, zl]!= null && map [xl, zl]!= 0 ) {
				
			return true;
			}
		}
		return false;
	}

	public bool CheckLight(int xl, int zl){

		if(xl >= 1 && zl >= 0 && xl <= map[0,0] && zl < map[0,1]){
			if (map [xl, zl]!= null && map [xl, zl]== 2 ) {
				return true;
			}
		}
		return false;
	}

	public int returnInitPosX(){
		return map [0, 2];
	}
	public int returnInitPosZ(){
		return map [0, 3];
	}
	public Vector3 returnRotation(){
		return new Vector3 (map [0, 4], map [0, 5], map [0, 6]);
	}
	public void changeMaterialOn (int x, int z){
		string named = x.ToString () + " " + z.ToString ();
		Material mat= Resources.Load ("Materials/blue", typeof(Material)) as Material;
		MeshRenderer mr = GameObject.Find (named).transform.GetComponent<MeshRenderer> ();
		if (mr.material.name == "blue (Instance)") {
			mr.material = Resources.Load ("Materials/green", typeof(Material)) as Material;
			objectivesDone++;
		}
		if(objectivesDone==objectives)
		StartCoroutine(waitNextLevel ());
	}
	public void changeMaterialOff (){
		MeshRenderer mr = GameObject.Find ("light(Clone)").transform.GetComponent<MeshRenderer> ();
		mr.material = Resources.Load ("Materials/blue", typeof(Material)) as Material;
	}
	void chargeLevel(string lvl){
		readTextFile (lvl);
	}

	IEnumerator waitNextLevel(){
		yield return new WaitForSeconds (2.0f);
		foreach (Transform child in panel.transform) {
			GameObject.Destroy (child.gameObject);
		}
		foreach (Transform child in this.transform) {
			GameObject.Destroy (child.gameObject);
		}
		objectivesDone = 0;
		if (lvl == InstantiateMenu.mapsDone) {
			InstantiateMenu.mapsDone++;
			InstantiateMenu.score = InstantiateMenu.score + 10;
		}
		lvl++;
		level = "";
		level += "maps/";
		level += lvl.ToString();
		panel.GetComponent<panelTask> ().Init();
		panel.GetComponent<panelTask> ().clearList ();
		if (lvl > 8)
			goMenu ();
		chargeLevel (level);
	}

	public void resetValues(){
		foreach (Transform child in this.transform) {
			if (child.gameObject.name != "floor(Clone)" && child.gameObject.name != "MaleFreeSimpleMovement1(Clone)") {
				MeshRenderer mr = child.gameObject.transform.GetComponent<MeshRenderer> ();
					mr.material = Resources.Load ("Materials/blue", typeof(Material)) as Material;
			}
		}
		objectivesDone = 0;
	}

	public void audioInstruction(){

		audioIns++;
		if (audioIns > 2)
			audioIns = 0;
		
		switch (audioIns) 
		{
		case 0:
			panel.GetComponent<panelTask> ().changeSpriteButton ("Textures/speaker-mid");
			this.GetComponent<AudioSource> ().volume = 0.5f;
			break;
			case 1:
			panel.GetComponent<panelTask> ().changeSpriteButton ("Textures/speaker-on");
			this.GetComponent<AudioSource> ().volume = 1.0f;
			break;
			case 2:
			panel.GetComponent<panelTask> ().changeSpriteButton ("Textures/speaker-off");
			this.GetComponent<AudioSource> ().volume = 0.0f;
			break;
		}
	}

	public void goMenu(){
		Application.LoadLevel ("Menu");

	}


}