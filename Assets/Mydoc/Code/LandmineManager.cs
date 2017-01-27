using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineManager : MonoBehaviour {

	//地雷のArray
	int[,] landmine; 
	//public GameObject GameManager;
	private CreateStage CreateStage;
	int Row;
	int Column;
	//地雷Set Flag
	public bool landmineSetFlag;
	//地雷の数
	public int LandmineQuantity = 0;

	void Start () {
		CreateStage = GetComponent<CreateStage>();
		Row = CreateStage.GetRow ();
		Column = CreateStage.GetColumn ();
		landmineSetFlag = false;

		//地雷の数はキューブ/5
		LandmineQuantity = Row * Column / 5;

	}

	void Update () {
		
	}

	//Cubeを触るとき発生するイベント　By Unityちゃん
	public void CubeTouch(GameObject HitCube){
		Cube Cube = HitCube.gameObject.GetComponent<Cube> ();

		//もし地雷設定した場合、
		if (landmineSetFlag == true) {

			//キューブが表示されてない場合表示にする
			if (Cube.display == false) {
				Cube.setDisplay (true);

				//プレイヤーの赤チェックを取り消し
				if(Cube.playerCheck){
					Cube.playerCheck = false;
				}

				//該当キューブは地雷ならGameOver
				if (Cube.Landmines) {

					GameObject UnityChan = GameObject.Find ("/Player/unitychan");
					UnityChan.GetComponent <UnityChanMove>().GameNotClear();
					/////////////////////////////////////////////////GameNotClear ();
				}

			}
			return;
		}
			
		//最初のキューブを記録する,迴避地雷設定を回避ため
		string firstCube = HitCube.gameObject.name.Substring (4, 3);


		//地雷はまだ設定されない場合、[,]Arrayを作成する
		if (landmineSetFlag == false) {
			landmineSetFlag = true;
			landmine = CreateLandmineArray(firstCube);
		}

		//地雷設定
		LandmineSet(landmine);

		//触ったキューブを表示にする
		Cube.setDisplay (true);

		//プレイヤーの赤チェックを取り消し、二回書いたような気がする...一つのメソッドにまとめるの方がいいかも
		if(Cube.playerCheck){
			Cube.playerCheck = false;
		}
			
	}

	//地雷Arrayを作成する　、関数は最初触ったキューブ(EX > 1x5)
	public int[,] CreateLandmineArray(string firstCube){

		//キューブが5以下なら地雷は1にする
		if (LandmineQuantity == 0) {
			LandmineQuantity = 1;
		}

		int landmineCount=0;
		int[,] array2D = new int[Row+1,Column+1] ; 

		for (int i = 1; i <= Row; i++) {
			for (int j = 1; j <= Column; j++){

				//最初のキューブならcontinue
				if (firstCube.Equals (i+"x"+j)) {
					continue;
				}

				//地雷数が足りたならcontinue
				if (landmineCount == LandmineQuantity) {
					continue;
				}
					
				//ランダムがゼロなら地雷を設定する　２０％
				if(Random.Range(0,5)==0 && landmineCount < LandmineQuantity){
					array2D [i, j] = 1;
					landmineCount++;
				}

			}
		}
			
		//地雷が足りない場合、もう一度実行する
		if (landmineCount != LandmineQuantity) {
			array2D = CreateLandmineArray(firstCube); 
		}

		//Debug.Log ("1x3=>>"+array2D[1,3]);
		return array2D;

	}
		
	//地雷Arrayをプリンタする
	void checkLandmineCubes(){
		for(int i = 1; i<6; i++){
			Debug.Log (
				landmine[1,i].ToString() + 
				landmine[2,i].ToString() + 
				landmine[3,i].ToString() + 
				landmine[4,i].ToString() + 
				landmine[5,i].ToString()+"i="+i);
		}
	}

	//地雷設定メソッド
	void LandmineSet(int[,] LandmineArray){
		string Path;
		GameObject Cubes;

		//0x1より1x2の方が読み安いので+1にする
		int Row = this.Row + 1;
		int Column = this.Column + 1;

		for(int i = 1; i<Row; i++){
			for(int j = 1;j<Column;j++){
				//Cube2x5(Clone)
				Path = "/Cube"+i+"x"+j+"(Clone)";
				Cubes = GameObject.Find (Path);

				if (landmine [i, j] != 0) {
					Cubes.GetComponent<Cube> ().setLandmines(true);
				}

				int C1 = 0,	C2 = 0,C3 = 0,C4 = 0,
				C5 = 0,C6 = 0,C7 = 0,C8  =0;

				//存在してないキューブを回避する,0x0みたいの
				bool iCubeNotNull;
				bool jCubeNotNull;

				//C1
				iCubeNotNull = 0< (i-1) &&(i-1) <Row;
				jCubeNotNull = 0< (j-1) &&(j-1) <Column;
				if (iCubeNotNull && jCubeNotNull) {
					C1 = landmine [i - 1, j - 1];
				}

				//C2
				jCubeNotNull = 0< (j-1) &&(j-1) <Column;
				if(jCubeNotNull){
					C2 = landmine [i, j - 1];
				}

				//C3
				iCubeNotNull = 0< (i+1) &&(i+1) <Row;
				jCubeNotNull = 0< (j-1) &&(j-1) <Column;
				if(iCubeNotNull && jCubeNotNull){
					C3 = landmine [i + 1, j - 1];
				}
				//C4
				iCubeNotNull = 0< (i-1) &&(i-1) <Row;
				if(iCubeNotNull){
					C4=landmine [i - 1, j];
				}

				//C5
				iCubeNotNull = 0< (i+1) &&(i+1) <Row;
				if(iCubeNotNull){
					C5=landmine [i + 1, j];
				}

				//C6
				iCubeNotNull = 0< (i-1) &&(i-1) <Row;
				jCubeNotNull = 0< (j+1) &&(j+1) <Column;
				if(iCubeNotNull && jCubeNotNull){
					C6=landmine [i - 1, j + 1];
				}

				//C7
				jCubeNotNull = 0< (j+1) &&(j+1) <Column;
				if (jCubeNotNull) {
					C7 = landmine [i, j + 1];
				}

				//C8
				iCubeNotNull = 0< (i+1) &&(i+1) <Row;
				jCubeNotNull = 0< (j+1) &&(j+1) <Column;
				if(iCubeNotNull && jCubeNotNull){
					C8=landmine [i + 1, j + 1];
				}

				//周りにある地雷の数をまとめて設定する
				//int sum =C1+C2+C3+C4+C5+C6+C7+C8;
				Cubes.GetComponent<Cube> ().setNumber(C1+C2+C3+C4+C5+C6+C7+C8);

			}
		}
	}
		
	public int getLandmineQuantity(){
		return this.LandmineQuantity;
	}
}
