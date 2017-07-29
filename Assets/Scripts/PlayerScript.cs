using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public GameObject map;
	public GameObject panel;
	float initX;
	float initY;
	float initZ;
	bool check=false;
	bool executing;
	Vector3 rotation;
	Vector3 positionFinal;
	int rotationFinal;
	int count=0;
	List<int> movementsComplete = new List<int>();
	List<int> finalMovements = new List<int>();
	// Use this for initialization
	void Start () {
		map = GameObject.Find ("Instantiate");
		panel = GameObject.Find ("Canvas/Panel");
		positionFinal = transform.localPosition;
		initX = map.GetComponent<Instantiate> ().returnInitPosX ();
		initZ = map.GetComponent<Instantiate> ().returnInitPosZ ();
		initY = 0.25f;
		rotation = map.GetComponent<Instantiate> ().returnRotation ();
		executing = false;
		//map.GetComponent<Instantiate> ().CheckPosition (1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void move (List<string> movements) {
		executing = true;
		count = 0;
		finalMovements.Clear ();
		char separator = ' ';
		for(int i=0;i<movements.Count;i++){
			string[] instruction = movements [i].Split (separator);
			finalMovements.Add(int.Parse(instruction[0]));
		}
		moveTwo (count);
	}

	bool moveFront(){
		int direction;
		int angle = (int)System.Math.Round (transform.localEulerAngles.y);

		if (angle == 0) {
			positionFinal.z += 1;
		} else if (angle == 90) {
			positionFinal.x += 1;
		} else if (angle == 180) {
			positionFinal.z -= 1;
		} else{
			positionFinal.x -= 1;
		}
		int xl = (int)System.Math.Round (positionFinal.x);
		int zl = (int)System.Math.Round (positionFinal.z);
		return map.GetComponent<Instantiate> ().CheckPosition (xl, zl);
	}

	public void goInit(){
		map.GetComponent<Instantiate> ().resetValues ();
		positionFinal.x = initX;
		positionFinal.z = initZ;
		//positionFinal.y = initY;
		transform.localPosition = positionFinal;
		transform.localEulerAngles = rotation;
	}
	IEnumerator moveInTime(int ins, Vector3 vec, bool work){
		bool wait = false;
		int initRot=(int)System.Math.Round (transform.localEulerAngles.y);
				float timePassed = 0;
				Vector3 deltaPos = vec - transform.localPosition;
				if (ins == 2) this.GetComponent<SimpleCharacterControl> ().walk ();
				while (timePassed < 0.5f) {
					timePassed += Time.deltaTime;
					switch (ins) {
					case 0/*right*/:
						transform.Rotate (vec * Time.deltaTime*2);
						break;
					case 1/*left*/:
						transform.Rotate (vec * Time.deltaTime*2);
						break;
					case 2/*front*/:
						if(work)
							transform.localPosition += deltaPos * Time.deltaTime*2;
						break;
						case 3/*light*/:
							this.GetComponent<SimpleCharacterControl> ().light ();
							wait = true;
						break;
					default:
						break;
					}
					yield return null;
				}
		if (wait) {
			yield return new WaitForSeconds (1.7f);
			wait = false;
			int xa = (int)System.Math.Round (transform.localPosition.x);
			int za = (int)System.Math.Round (transform.localPosition.z);
			Debug.Log (xa);
			Debug.Log (za);
			if (map.GetComponent<Instantiate> ().CheckLight (xa, za)) {
				map.GetComponent<Instantiate> ().changeMaterialOn (xa, za);
			}
		}
		this.GetComponent<SimpleCharacterControl> ().stay ();
		count++;
		if(ins==0||ins==1)transform.localRotation = Quaternion.Euler(new Vector3(0,vec.y + initRot,0));

		if (count < finalMovements.Count) {

			positionFinal = transform.localPosition;
			moveTwo (count);
		} else {
			panel.GetComponent<panelTask> ().clearPanel (count);
			executing = false;
		}
	}

	void moveTwo (int i) {
		panel.GetComponent<panelTask> ().lightPanel (i);
		switch (( finalMovements [i])) 
		{

		case 0/*right*/:
			//transform.Rotate (new Vector3 (0, 90, 0), Space.Self);
			StartCoroutine (moveInTime (0, new Vector3 (0, 90, 0), true));

			break;
		case 1/*left*/:
			//transform.Rotate (new Vector3 (0, -90, 0), Space.Self);
			StartCoroutine (moveInTime (1, new Vector3 (0, -90, 0), true));
			break;
		case 2/*front*/:
			bool work = moveFront();

				StartCoroutine(moveInTime (2, positionFinal, work));
			//transform.localPosition = positionFinal;
			break;
		case 3/*light*/:
			StartCoroutine(moveInTime (3, positionFinal, true));
			break;
		default:
			break;
		}

	}

	public bool getExecuting(){
		return executing;
	}

}
