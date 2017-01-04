using UnityEngine;
using System.Collections;

public class CameraParent : MonoBehaviour {
	GameObject UnityChan;
	GameObject mainCamara;
	// Use this for initialization
	void Start () {
		UnityChan = GameObject.Find ("/Player/unitychan");
		mainCamara = GameObject.Find ("/CamaraParent/Main Camera");
	}
	
	// Update is called once per frame
	void Update () {

		//更新成Unitychan的position
		Vector3 pos2 = UnityChan.transform.position;
		pos2.x = UnityChan.transform.position.x;
		pos2.y = UnityChan.transform.position.y;
		pos2.z = UnityChan.transform.position.z;
		transform.position = pos2;

		//右鍵調整角度Y
		if (Input.GetMouseButton (1)) {
			transform.Rotate (0,Input.GetAxisRaw("Mouse X"),0);
		}

		//滾輪調整焦距 100~2
		Camera.main.fieldOfView += (20 * Input.GetAxis ("Mouse ScrollWheel"));
		if (Camera.main.fieldOfView > 100) {
			Camera.main.fieldOfView = 100;
		}

		if (Camera.main.fieldOfView < 2) {
			Camera.main.fieldOfView = 2;
		}
	}
		
	public void setFieldOfView(float fieldOfView){
		Camera.main.fieldOfView = fieldOfView;

	}

}
