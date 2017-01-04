using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	public bool display ;
	public bool Landmines;
	public int number;
	public bool playerCheck;
	public string playerCheckType;


	Material material;
	MeshRenderer renderer;
	Material[] mats;

	// Use this for initialization
	void Start () {
		
		renderer = GetComponent<MeshRenderer> ();
		display = false;
		Landmines = false;
		playerCheck = false;

	}
	// Update is called once per frame
	void Update () {
		
		//確認結果有無顯示
		if(!checkDisplay()){
			return;
		}

		//確認有無地雷
		if (checkLandmine ()) {
			//方塊改為紅色，顯示炸彈 遊戲結束
			//renderer.material.color = Color.red;
			material = Resources.Load("Material/Boom", typeof(Material)) as Material;
			renderer.material = material;

			return;
		}


		//確認數字
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

	//確認結果有無顯示
	bool checkDisplay(){
		if (this.display == true) {
			return true;
		} else {
			return false;
		}
	} 

	//確認玩家有無標記
	bool checkPlayerCheck(){
		
		if (this.playerCheck == true) {
			return true;
		} else {
			return false;
		}
	} 

	//確認有無地雷
	bool checkLandmine(){
		if (this.Landmines == true) {
			return true;
		} else {
			return false;
		}
	} 

	//確認有無地雷
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

