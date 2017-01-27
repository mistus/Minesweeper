using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageDisplay : MonoBehaviour {

	public GUIStyle msgWnd;
	const float screenwidth = 1136;
	const float msgwWidth = 100;
	const float msgwHeigh = 100;
	const float msgwPosX = (screenwidth - msgwWidth) / 2;
	const float msgwPosY = 0;
	float factorSize = Screen.width / screenwidth;
	float msgwX;
	float msgwY;
	float msgwW;
	float msgwH;

	CubeManager CubeManager;

	void Start () {
		CubeManager = (CubeManager)GetComponent<CubeManager> ();
		msgwW = msgwWidth * factorSize;
		msgwH = msgwHeigh * factorSize;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){

		//fontSet
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = (int) factorSize * 30;

		myStyle.normal.textColor = Color.black;
		msgwX = (msgwPosX + 170) * factorSize;
		msgwY = (msgwPosY + 0) * factorSize;


		//取得Cube整體狀況 
		//依序 未開方塊數、地雷數(扣掉標記)、遊戲經過時間
		int noCheckCube = CubeManager.noCheckCube;
		int landmines = CubeManager.landmines;
		int times = (int)(Time.time / 1);

		//string Massage = "開始時間："+times+" , 地雷數量： 5/25";
		string Massage = "開始時間："+times+" , 地雷の数："+landmines+"/"+noCheckCube;

		myStyle.fontSize =  25 ;
		GUI.Label(new Rect(msgwX, msgwY, msgwW, msgwH) ,Massage ,myStyle);
		//GUI.Box (new Rect (10,10,100,90), "Loader Menu");
	}
}
