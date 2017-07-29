using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShow : MonoBehaviour {

	float rotateVel;
	// Use this for initialization
	void Start () {
		this.GetComponent<SimpleCharacterControl> ().walkShow ();
		//StartCoroutine (rotateInTime ();
		rotateVel = -180;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SimpleCharacterControl> ().walk ();
		rotateVel = rotateVel + (30 * Time.deltaTime);
		transform.rotation = Quaternion.Euler(new Vector3(0,rotateVel,0));

	}
}
		
