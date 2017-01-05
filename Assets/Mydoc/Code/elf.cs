using UnityEngine;
using System.Collections;

public class elf : MonoBehaviour {

	public float moveSpeed = 3f; 
	public float rotationSpeed = 360f;
	string type;
	GameObject Camera;
	MeshRenderer renderer;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<MeshRenderer> ();
		Camera = GameObject.Find ("/CamaraParent");
		type = "Red";
	}
	
	// Update is called once per frame
	void Update () {
		move ();

		if (Input.GetKeyDown (KeyCode.X)) {
			typeChange ();
		}
	}

	void move(){
		
		//沒按下左鍵時角色不移動
		if (Input.GetMouseButton (0)) {
			direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		} else {
			direction = Vector3.zero;
		}
		Quaternion CameraQuaternion = Camera.transform.rotation;
		direction =  (CameraQuaternion * direction);
		direction = direction * moveSpeed;
		transform.position	+= direction * Time.deltaTime;
	}

	void typeChange(){


		//white >  Green > Red 
		switch (type) {

		//? 設定為白色
		case "White":
			renderer.material.color = Color.green;
			this.type = "Green";
			break;

			//Red 設定為紅色
		case "Green":
			renderer.material.color = Color.red;
			this.type = "Red";
			break;

			//Green 設定為綠色
		case "Red":
			renderer.material.color = Color.white;
			this.type = "White";
			break;

		}
	}
}
