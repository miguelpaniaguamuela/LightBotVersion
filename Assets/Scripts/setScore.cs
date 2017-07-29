using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class setScore : MonoBehaviour {

	public GameObject Name;
	public GameObject Puntuation;

	public void SetScore(string playerName, string playerpuntuation)
	{
		Name.GetComponent<Text> ().text = playerName;
		Puntuation.GetComponent<Text> ().text = playerpuntuation;
	}
}
