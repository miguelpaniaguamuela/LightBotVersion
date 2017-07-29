using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject target;
	int state;
	// Use this for initialization
	void Start () {
		target = GameObject.Find ("floor");
		state = 1;


	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt(target.transform);
		Debug.Log (transform.localEulerAngles.y);
		//if (transform.localEulerAngles.y > -50 && transform.localEulerAngles.y < 50) {
			if (state == 1) {
			} else if (state == 0) {
			if ((transform.localEulerAngles.y <60 && transform.localEulerAngles.y >-1)
				/*|| (transform.localEulerAngles.y >320)*/)
				Right ();
			} else {
			if ((transform.localEulerAngles.y <55 && transform.localEulerAngles.y >=0)
				|| (transform.localEulerAngles.y >315))
				Left ();
			}
		//}
	}

	void Right () {
		transform.LookAt(target.transform);
		transform.Translate((Vector3.right*4) * Time.deltaTime);
	}

	void Left () {
		transform.LookAt(target.transform);
		transform.Translate((Vector3.left*4) * Time.deltaTime);
	}

	public void stateRight(){
		state = 0;
	}

	public void stateLeft(){
		state = 2;
	}

	public void noState(){
		state = 1;
	}
		
	public void setPosCamera(){
		transform.localEulerAngles = new Vector3 (33.0f, 47.1f, 0.0f);
		Debug.Log ("entro");
	}
}
