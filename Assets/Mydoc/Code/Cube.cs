using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public bool display ;
	public bool Landmines;
	//Cube隣の地雷の数↓
	public int number;
	public bool playerCheck;

	Material material;
	MeshRenderer renderer;
	Material[] mats;

	void Start () {

		renderer = GetComponent<MeshRenderer> ();
		display = false;
		Landmines = false;
		playerCheck = false;

	}
	// Update is called once per frame
	void Update () {

		//表示されないと戻す
		if(!checkDisplay()){
			return;
		}

		//該当Cubeに地雷あるかどうか
		if (checkLandmine ()) {
			//赤に変更し、Gameover
			//renderer.material.color = Color.red;
			material = Resources.Load("Material/Boom", typeof(Material)) as Material;
			renderer.material = material;

			return;
		}


		//周りの地雷数に応じて数字を変化する
		switch(number){
		case 0:
			material = Resources.Load("Material/Zero", typeof(Material)) as Material;
			renderer.material = material;
			break;

		case 1:
			material = Resources.Load("Material/One", typeof(Material)) as Material;
			renderer.material = material;
			break;

		case 2:
			material = Resources.Load("Material/Two", typeof(Material)) as Material;
			renderer.material = material;
			break;

		case 3:
			material = Resources.Load("Material/Three", typeof(Material)) as Material;
			renderer.material = material;
			break;

		case 4:
			material = Resources.Load("Material/Four", typeof(Material)) as Material;
			renderer.material = material;
			break;

		}

	}

	//表示されたかどうかを確認するメソッド
	bool checkDisplay(){
		if (this.display == true) {
			return true;
		} else {
			return false;
		}
	} 

	//プレイヤーが赤チェックを付けたどうかを確認するメソッド
	bool checkPlayerCheck(){

		if (this.playerCheck == true) {
			return true;
		} else {
			return false;
		}
	} 

	//このCubeに地雷あるかのメソッド
	bool checkLandmine(){
		if (this.Landmines == true) {
			return true;
		} else {
			return false;
		}
	} 

	//このCube隣の地雷数をReturnする
	int checkNumber(){
		return this.number;
	} 

	public void setLandmines(bool landmines){
		this.Landmines = landmines;
	}

	public void setNumber(int number){
		this.number = number;
	}

	public void setDisplay(bool display){
		this.display = display;
	}

	void OnMouseDown(){
		if (playerCheck) {
			renderer.material.color = Color.white;
			playerCheck = false;
		}else{
			renderer.material.color = Color.red;
			playerCheck = true;
		}


	}   


}
