using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanMove : MonoBehaviour {

	public float moveSpeed = 5f; 
	public float rotationSpeed = 360f;
	public float jumpPower = 3.6f;
	public bool isGrounded;
	public bool Jump = false;
	public LayerMask layer;
	public bool StageClear = false;

	public GameObject Camera;
	public GameObject bulletObj;
	public GameObject gameManager;

	CharacterController characterController;
	Vector3 gravity = Vector3.zero;
	Animator animator;

	void Start () {

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {

		//床にいるかをチェックする
		checkIsGrounded ();

		//遊戲終了ならカメラに向う、且つ移動不可
		if (StageClear) {
			Quaternion rotation = Quaternion.Euler(0, 180, 0);
			transform.rotation = rotation;
			return;
		}

		//ジャンプしてない場合、ジャンプ動画表示しない
		if (animator.GetCurrentAnimatorStateInfo (0).nameHash == Animator.StringToHash ("Base Layer.Jump")) {
			animator.SetBool ("Jumping", false);
		}

		UnityChanJump ();
		moveAndGravity ();

	}

	//床にいるかをチェックする
	bool checkIsGrounded(){

		layer = LayerMask.NameToLayer ("Everything");
		Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
		float tolerance = 0.09f;

		if (Physics.Raycast (ray, tolerance, layer)) {
			isGrounded = true;
			animator.SetBool ("isGrounded", true);
			return true;
		} else {
			isGrounded = false;
			animator.SetBool ("isGrounded", false);
			return false;
		}

	}

	//移動、重力処理
	void moveAndGravity(){
		
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		//transform.rotation = Camera.transform.rotation;
		Quaternion CameraQuaternion = Camera.transform.rotation;
		//transform.rotation	= Quaternion.Euler( 0, 50, 0);
		direction =  (CameraQuaternion * direction);

		if (direction.sqrMagnitude > 0.01f) {

			Vector3 forward = Vector3.Slerp (transform.forward, direction, rotationSpeed);

			transform.LookAt(transform.position + forward);
		}

		direction = direction * moveSpeed;

		//if (!characterController.isGrounded) {
		if (!isGrounded) {
			gravity = gravity + Physics.gravity * Time.deltaTime;
		} else {
			gravity = Vector3.zero;

			if (Jump == true) {
				//	gravity.y = 7f;
				gravity.y = jumpPower;
				Jump = false;
			}
		}

		direction = direction + gravity;

		characterController.Move(direction * Time.deltaTime);
		animator.SetFloat ("Speed", characterController.velocity.magnitude);

	}

	//ジャンプ事件
	void UnityChanJump(){

		if(Input.GetKeyDown(KeyCode.Space)){

			if (animator.GetCurrentAnimatorStateInfo (0).nameHash == Animator.StringToHash ("Base Layer.Jump")) {
				return;
			}


			if (!checkIsGrounded ()) {
				//if (!characterController.isGrounded) {

				return;
			}

			animator.SetBool ("isGrounded", true);
			animator.SetBool ("Jumping", true);

			Jump = true;
		}
	}
		
	//Cubeを触る時発生するイベント
	void OnControllerColliderHit(ControllerColliderHit GameObjectHit){
		
		//Cubeのみ発生しない 
		if (GameObjectHit.gameObject.name.Substring(0,4) != "Cube") {
			return;
		}
			
		LandmineManager LandmineManger = gameManager.GetComponent<LandmineManager> ();
		LandmineManger.CubeTouch (GameObjectHit.gameObject);
	}



	//勝利する時のカメラ、アニメの設定変更
	public void GameClear(){

		Material material;
		MeshRenderer renderer;

		//ゲーム終了のフラグ、移動できないになる
		StageClear = true;
		animator.SetBool ("StageClear", true);

		//Unityちゃん頭の上にあるステータスをStageClearに変更する
		GameObject StageStatus = GameObject.Find ("/Player/unitychan/StageStatus");
		material = Resources.Load("Material/StageClear", typeof(Material)) as Material;
		StageStatus.GetComponent<MeshRenderer> ().material = material;

		//カメラを真正面にする
		GameObject Camera = GameObject.Find ("/CamaraParent/Main Camera");
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		Camera.transform.rotation = rotation;
		Vector3 position = Camera.transform.position;
		position.y = 0.7f;
		position.z = position.z + 2f;
		Camera.transform.position = position;
		Camera.GetComponent<Camera> ().fieldOfView = 70f;

		//5秒後ホームページに遷移する
		IEnumerator coroutine = ReturnStart (5f);
		StartCoroutine(coroutine);

	}

	//lost 有時間跟win合併成一個function
	public void GameNotClear(){

		Material material;
		MeshRenderer renderer;

		//ゲーム終了のフラグ、移動できないになる
		StageClear = true;
		animator.SetBool ("StageNotClear", true);

		//Unityちゃん頭の上にあるステータスをStageFailに変更する
		GameObject StageStatus = GameObject.Find ("/Player/unitychan/StageStatus");
		material = Resources.Load("Material/StageFail", typeof(Material)) as Material;
		StageStatus.GetComponent<MeshRenderer> ().material = material;

		//カメラを真正面にする
		GameObject Camera = GameObject.Find ("/CamaraParent/Main Camera");
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		Camera.transform.rotation = rotation;
		Vector3 position = Camera.transform.position;
		position.y = 0.7f;
		position.z = position.z + 2f;
		Camera.transform.position = position;
		Camera.GetComponent<Camera> ().fieldOfView = 70f;


		//5秒後ホームページに遷移する
		IEnumerator coroutine = ReturnStart (5f);
		StartCoroutine(coroutine);

	}

	//time 秒後回到首頁
	IEnumerator ReturnStart(float time)
	{
		yield return new WaitForSeconds(time);
		Application.LoadLevel ("00_Start");

	}
		
}

