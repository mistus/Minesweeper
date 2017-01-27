using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour {

	int Row = 5;
	int Column = 5;

	float planeHeight;
	float planeWidth;
	public GameObject plane;
	public GameObject Cube;
	public GameObject Wall;
	public GameObject Unitychan;

	void Start () {
		CreatePlane ();
		CreateCube();
		CreateWall ();
	}

	void Update () {
		
	}
		
	//床を作る
	void CreatePlane(){
		//Position(0,-0.5,0), rotation(0,0,0)長さはCubex1+Cube+1*2 (Cubeの長さ＋道の長さ)
		Vector3 position = new Vector3 (0,-0.5f,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		planeHeight = Column * 3 + 1.25f; 
		planeWidth = Row * 3 + 1.25f;
		plane.transform.localScale = new Vector3 (planeWidth * 0.1f, 1, planeHeight * 0.1f); 
		Instantiate (plane, position, rotation);
	}

	//Cubeを作る
	void CreateCube(){
		Vector3 position = new Vector3 (0,0,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);


		for (int i = 0; i < Row; i++) {

			for (int j = 0; j < Column; j++) {
				//2+3*n > 道の広さ(2)+(道の広さ(2)+Cube(1))*数
				position = new Vector3 (2 + (3 * i) - (planeWidth / 2), 0, -2 - (3 * j) + (planeHeight / 2));
				//0x1より1x2の方が読みやすいので+1
				Cube.name = "Cube"+(i+1)+"x"+(j+1);
				Instantiate (Cube, position, rotation);
			}
				
		}

	}

	//壁を作る
	void CreateWall(){
		Vector3 position = new Vector3 (0,0,0);
		Quaternion rotation = Quaternion.Euler(0, 0, 0);

		//左の壁 (planeWidth / 2)で一番左に移動する、次は壁の広さ1左に移動する
		float posX =  -(0.5f)- (planeWidth / 2);
		position = new Vector3 (posX,0,0);
//		plane.transform.localScale = new Vector3 (planeHeight * 0.1f, 1, planeWidth * 0.1f); 
		Wall.transform.localScale = new Vector3 (1 , 3, planeHeight); 
		Wall.name = "LeftWall";
		Instantiate (Wall, position, rotation);

		//右の壁計算方が左壁と同じ
		posX = (0.5f)+(planeWidth / 2);
		position = new Vector3 (posX,0,0);
		Wall.transform.localScale = new Vector3 (1 , 3, planeHeight); 
		Wall.name = "RightWall";
		Instantiate (Wall, position, rotation);

		//下の壁                                        ↓ ↓+2にしなくでも大丈夫だが、きれいにならないね...欠けてる
		float posY= -(0.5f) - (planeHeight / 2);
		position = new Vector3 (0,0,posY);
		Wall.transform.localScale = new Vector3 (planeWidth + 2 , 3, 1); 
		Wall.name = "BottomWall";
		Instantiate (Wall, position, rotation);

		//上の壁
		posY= (0.5f)+(planeHeight / 2);
		position = new Vector3 (0,0,posY);
		Wall.transform.localScale = new Vector3 (planeWidth + 2 , 3, 1); 
		Wall.name = "TopmWall";
		Instantiate (Wall, position, rotation);


	}
		
	public int GetRow(){
		return this.Row;
	}

	public int GetColumn(){
		return this.Column;
	}


}
