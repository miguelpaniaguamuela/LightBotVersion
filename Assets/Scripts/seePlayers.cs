using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seePlayers : MonoBehaviour {

	private string URLSeePuntuation = "guybot:fontanar@files.000webhost.com/public_html/SeePuntuation.php";
	private List<Player> playerRanking = new List<Player> ();
	private string[] CurrentArray = null;
	public Transform tfPanelLoadData;
	public Text txtLoading;
	public GameObject Panelpre;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (savePlayers.startSee) {
			StartCoroutine (getPlayers ());
			savePlayers.startSee = false;
		}
	}

	IEnumerator getPlayers()
	{
			txtLoading.text = "Loading...";
			WWW DataServer = new WWW ("https://guybot.000webhostapp.com/SeePuntuation.php");
			yield return DataServer;
			//Debug.Log (DataServer.progress);
			if (!string.IsNullOrEmpty (DataServer.error)) {
				//Debug.Log ("Problema al conectar a BD" + DataServer.error);
				txtLoading.text = DataServer.error;
			} else {
				Debug.Log ("Conectado a BD");
				txtLoading.text = "";
				GetRegisters (DataServer);
				SeeRegisters ();
			}
		
	}

	void GetRegisters(WWW DataServer)
	{
		CurrentArray = System.Text.Encoding.UTF8.GetString(DataServer.bytes).Split(";" [0]);

		for (int i = 0;  i <= CurrentArray.Length - 3; i = i + 2)
		{
			playerRanking.Add(new Player(CurrentArray[i], CurrentArray[i+1]));
			//Debug.Log (CurrentArray [i]);
		}
	}

	void SeeRegisters()
	{
		for (int i = 0; i < playerRanking.Count; i++) 
		{
			GameObject obj = Instantiate (Panelpre);
			Player jg = playerRanking [i];
			obj.GetComponent<setScore> ().SetScore (jg.name, jg.puntuation);
			obj.transform.SetParent (tfPanelLoadData);
			obj.GetComponent<Transform> ().localPosition = new Vector3(0, 0, 0); 
			obj.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
			if (jg.name == InstantiateMenu.name) {
				foreach (Transform child in obj.transform) {
					child.gameObject.GetComponent<Text> ().color = Color.red;
				}
			}
		}
	}
		
}

public class Player
{
	public string puntuation;
	public string name;

	public Player(string playerName, string playerPuntuation)
	{
		puntuation = playerPuntuation;
		name = playerName;
	}
}
