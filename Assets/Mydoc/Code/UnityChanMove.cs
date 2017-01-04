using UnityEngine;
using System.Collections;

public class UnityChanMove : MonoBehaviour {
	public float moveSpeed = 5f; 
	public float rotationSpeed = 360f;
	public float jumpPower = 3.6f;
	public bool isGrounded;
	public bool Jump = false;
	public LayerMask layer;
	public bool StageClear = false;

	Vector3 gravity = Vector3.zero;
	CharacterController characterController;
	Animator animator;
	GameObject Camera;
	public GameObject bulletObj;
	//地雷二維陣列
	int[,] landmine; 

	//地雷已設置Flag
	public bool landmineSetFlag;

	void Start () {

		//基本款
		Camera = GameObject.Find ("/CamaraParent");
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		landmineSetFlag = false;
	}

	// Update is called once per frame
	void Update () {

		//地面判定
		checkIsGrounded ();

		//遊戲完成
		if (StageClear) {
			Quaternion rotation = Quaternion.Euler(0, 180, 0);
			transform.rotation = rotation;
			return;
		}

		//迴避非跳躍動畫
		if (animator.GetCurrentAnimatorStateInfo (0).nameHash == Animator.StringToHash ("Base Layer.Jump")) {
			animator.SetBool ("Jumping", false);
		}

		UnityChanJump ();
		moveAndGravity ();

	}

	//地面判定
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

	//移動、重力處理
	void moveAndGravity(){
		Vector3 direction;

		//左鍵時角色不移動
		if (Input.GetMouseButton (0)) {
			direction = Vector3.zero;
		} else {
			direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		}
			
		if(Input.GetKeyDown (KeyCode.Z)){
			shoot (transform.position + (transform.rotation * new Vector3 (0, 1.3f, 0.7f)));
		}
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

	void UnityChanJump(){
		
		//跳躍事件
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



	//通知區
	void OnControllerColliderHit(ControllerColliderHit hit){

		//限制觸發事件物件為Cube
		if (hit.gameObject.name.Substring(0,4) != "Cube") {
			return;
		}

		Cube Cube = hit.gameObject.GetComponent<Cube> ();

		//如果已設置地雷
		if (landmineSetFlag == true) {

			//如果方塊的狀態是尚未顯示
			if (Cube.display == false) {
				Cube.setDisplay (true);

				//取消玩家標記
				if(Cube.playerCheck){
					Cube.playerCheck = false;
				}

				//碰到地雷GameOver
				if (Cube.Landmines) {
					GameNotClear ();
				}

			}

			return;
		}
			
		//紀錄最初碰觸的箱子,迴避地雷用
		string firstCube = hit.transform.parent.gameObject.name.Substring (5, 1)+"x"+hit.gameObject.name.Substring (4, 1);

		//設置地雷
		//如果還沒設置過地雷則設置地雷
		if (landmineSetFlag == false) {
			landmineSetFlag = true;
			landmine = CreateLandmineArray(firstCube);
		}

		//印出地雷狀況
		//checkLandmineCubes ();

		//發地雷
		//Cubes/Cubes1/Cube5 < 1x5
		//Cubes/Cubes2/Cube5 < 2x5
		LandmineSet(landmine);
		Cube.setDisplay (true);

		//如有標記取消玩家標記
		if(Cube.playerCheck){
			Cube.playerCheck = false;
		}

	}


	//產生地雷二維陣列 參數為第一次觸碰的地雷(EX > 1x5)
	public int[,] CreateLandmineArray(string firstCube){

			int landmineCount=0;
			int[,] array2D = new int[6,6] ;

			for (int i = 1; i < 6; i++) {
				for (int j = 1; j < 6; j++){

					//如果是第一個碰觸的不骰跳過
					if (firstCube.Equals (i+"x"+j)) {
						continue;
					}

					//地雷集滿五顆跳過
					if (landmineCount == 5) {
						continue;
					}
						
					//0~4
				//Debug.Log ("Random = "+Random.Range(0,5));
					//如果0-4骰中0 也就是20% 並且地雷數量小於5
					if(Random.Range(0,5)==0 && landmineCount <5){
						array2D [i, j] = 1;
						landmineCount++;
					}

				}
			}
			
		//Debug.Log ("landmineCount =" +landmineCount);
			//如果結果不是五顆地雷則重新呼叫自己
			if (landmineCount != 5) {
			array2D = CreateLandmineArray(firstCube);
			}
			
		//Debug.Log ("1x3=>>"+array2D[1,3]);
			return array2D;
			
	}

	//確認地雷目前況狀用
	void checkLandmineCubes(){
		for(int i = 1; i<6; i++){
				Debug.Log (
				landmine[1,i].ToString() + 
				landmine[2,i].ToString() + 
				landmine[3,i].ToString() + 
				landmine[4,i].ToString() + 
				landmine[5,i].ToString());
		}
	}

	//發地雷哦！
	void LandmineSet(int[,] LandmineArray){
		string Path;
		GameObject Cubes;

		for(int i = 1; i<6; i++){
			for(int j = 1;j<6;j++){
				Path = "/Cubes/Cubes"+i+"/Cube"+j+"/";
				Cubes = GameObject.Find (Path);

				if (landmine [i, j] != 0) {
					Cubes.GetComponent<Cube> ().setLandmines(true);
				}

				int C1 = 0,	C2 = 0,C3 = 0,C4 = 0,
					C5 = 0,C6 = 0,C7 = 0,C8  =0;

				//以下為迴避不存在方塊,像是0x0
				bool iCubeNotNull;
				bool jCubeNotNull;

				//C1
				iCubeNotNull = 0< (i-1) &&(i-1) <6;
				jCubeNotNull = 0< (j-1) &&(j-1) <6;
				if (iCubeNotNull && jCubeNotNull) {
					C1 = landmine [i - 1, j - 1];
				}

				//C2
				jCubeNotNull = 0< (j-1) &&(j-1) <6;
				if(jCubeNotNull){
					C2 = landmine [i, j - 1];
				}

				//C3
				iCubeNotNull = 0< (i+1) &&(i+1) <6;
				jCubeNotNull = 0< (j-1) &&(j-1) <6;
				if(iCubeNotNull && jCubeNotNull){
					C3 = landmine [i + 1, j - 1];
				}
				//C4
				iCubeNotNull = 0< (i-1) &&(i-1) <6;
				if(iCubeNotNull){
					C4=landmine [i - 1, j];
				}

				//C5
				iCubeNotNull = 0< (i+1) &&(i+1) <6;
				if(iCubeNotNull){
					C5=landmine [i + 1, j];
				}

				//C6
				iCubeNotNull = 0< (i-1) &&(i-1) <6;
				jCubeNotNull = 0< (j+1) &&(j+1) <6;
				if(iCubeNotNull && jCubeNotNull){
					C6=landmine [i - 1, j + 1];
				}

				//C7
				jCubeNotNull = 0< (j+1) &&(j+1) <6;
				if (jCubeNotNull) {
					C7 = landmine [i, j + 1];
				}

				//C8
				iCubeNotNull = 0< (i+1) &&(i+1) <6;
				jCubeNotNull = 0< (j+1) &&(j+1) <6;
				if(iCubeNotNull && jCubeNotNull){
					C8=landmine [i + 1, j + 1];
				}

				//計算方塊周圍地雷數
				//int sum =C1+C2+C3+C4+C5+C6+C7+C8;
				Cubes.GetComponent<Cube> ().setNumber(C1+C2+C3+C4+C5+C6+C7+C8);

			}
		}
	}

	//Shoot!
	void shoot(Vector3 vector3){
		Instantiate (bulletObj, vector3, transform.rotation);
	}


	//win 有時間跟loss合併成一個function
	public void GameClear(){

		Material material;
		MeshRenderer renderer;

		//遊戲結束Flag and Animetor設定
		StageClear = true;
		animator.SetBool ("StageClear", true);

		//更改系統訊息文字
		GameObject StageStatus = GameObject.Find ("/Player/unitychan/StageStatus");
		material = Resources.Load("Material/StageClear", typeof(Material)) as Material;
		StageStatus.GetComponent<MeshRenderer> ().material = material;

		//更改攝影機角度
		GameObject Camera = GameObject.Find ("/CamaraParent/Main Camera");
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		Camera.transform.rotation = rotation;
		Vector3 position = Camera.transform.position;
		position.y = 0.7f;
		Camera.transform.position = position;
		Camera.GetComponent<Camera> ().fieldOfView = 38f;

		//5sec回首頁
		IEnumerator coroutine = ReturnStart (5f);
		StartCoroutine(coroutine);

	}

	//lost 有時間跟win合併成一個function
	void GameNotClear(){

		Material material;
		MeshRenderer renderer;

		//遊戲結束Flag and Animetor設定
		StageClear = true;
		animator.SetBool ("StageNotClear", true);

		//更改系統訊息文字
		GameObject StageStatus = GameObject.Find ("/Player/unitychan/StageStatus");
		material = Resources.Load("Material/StageFail", typeof(Material)) as Material;
		StageStatus.GetComponent<MeshRenderer> ().material = material;

		//更改攝影機角度
		GameObject Camera = GameObject.Find ("/CamaraParent/Main Camera");
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		Camera.transform.rotation = rotation;
		Vector3 position = Camera.transform.position;
		position.y = 0.7f;
		Camera.transform.position = position;
		Camera.GetComponent<Camera> ().fieldOfView = 38f;


		//5sec回首頁
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

