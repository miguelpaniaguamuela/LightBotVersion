using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savePlayers : MonoBehaviour {

	static public bool startSee;
	private string NewPuntuationURL = "guybot.000webhostapp.com/NewPuntuation.php?";
	private string secretKey = "faherian";
	// Use this for initialization
	void Start () 
	{
		/*Debug.Log (InstantiateMenu.name);
		Debug.Log (" ");
		Debug.Log (InstantiateMenu.score);*/
		startSee = false;
		StartCoroutine (SendPlayers (InstantiateMenu.name, InstantiateMenu.score));
	}

	public string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding ();
		byte[] bytes = ue.GetBytes (strToEncrypt);
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider ();
		byte[] hashBytes = md5.ComputeHash (bytes);
		string hashString = "";
		for (int i = 0; i < hashBytes.Length; i++) 
		{
			hashString += System.Convert.ToString (hashBytes [i], 16).PadLeft (2, '0');
		}

		return hashString.PadLeft (32, '0');
	}

	IEnumerator SendPlayers(string playerName, int playerPuntuation)
	{
		string hash = Md5Sum(playerName + playerPuntuation + secretKey);
		/*Debug.Log (InstantiateMenu.name);
		Debug.Log (" ");
		Debug.Log (InstantiateMenu.score);*/
		string PostURL = NewPuntuationURL + "Name=" + WWW.EscapeURL(playerName) + "&Puntuation=" + playerPuntuation + "&hash=" + hash;

		WWW DataPost = new WWW("https://" + PostURL);
		yield return DataPost;

		if (!string.IsNullOrEmpty(DataPost.error))
		{
			print("problema al intentar enviar jugador y su puntuacion a la base de datos: " + DataPost.error);
		}
		else
		{
			Debug.Log((System.Text.Encoding.UTF8.GetString(DataPost.bytes)));
		}
		startSee=true;
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void goMenu(){
		Application.LoadLevel ("Menu");

	}
}
