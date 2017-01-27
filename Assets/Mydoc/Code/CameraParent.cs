using UnityEngine;
using System.Collections;

public class CameraParent : MonoBehaviour {
	public GameObject UnityChan;
	public GameObject mainCamara;
	// Use this for initialization
	void Start () {
		//UnityChan = GameObject.Find ("/Player/unitychan");
		//mainCamara = GameObject.Find ("/CamaraParent/Main Camera");
	}

	void Update () {

		//positionをUnitychanのposisionに更新する
		Vector3 pos2 = UnityChan.transform.position;
		pos2.x = UnityChan.transform.position.x;
		pos2.y = UnityChan.transform.position.y;
		pos2.z = UnityChan.transform.position.z;
		transform.position = pos2;

		//右クリックでRotate調整する
		if (Input.GetMouseButton (1)) {
			transform.Rotate (0,Input.GetAxisRaw("Mouse X"),0);
		}

		//fieldOfViewを 2~100に設定する
		Camera.main.fieldOfView += (20 * Input.GetAxis ("Mouse ScrollWheel"));
		if (Camera.main.fieldOfView > 100) {
			Camera.main.fieldOfView = 100;
		}

		if (Camera.main.fieldOfView < 2) {
			Camera.main.fieldOfView = 2;
		}
	}

}
