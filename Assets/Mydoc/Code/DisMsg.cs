using UnityEngine;
using System.Collections;

public class DisMsg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GUIStyle msgWnd;


	void OnGUI(){
		const float screenwidth = 1136;

		const float msgwWidth = 100;
		const float msgwHeigh = 100;
		const float msgwPosX = (screenwidth - msgwWidth) / 2;
		const float msgwPosY = 0;


		float factorSize = Screen.width / screenwidth;

		float msgwX;
		float msgwY;
		float msgwW = msgwWidth * factorSize;
		float msgwH = msgwHeigh * factorSize;

		//fontSet
		GUIStyle myStyle = new GUIStyle ();
		myStyle.fontSize = (int) factorSize * 30;

		//msgwX = msgwPosX * factorSize;
		//msgwY = msgwPosY * factorSize;

		//GUI.Box(new Rect(msgwX, msgwY, msgwW, msgwH),"Windon" ,msgWnd);

		//myStyle.normal.textColor = Color.black;
		//msgwX = (msgwPosX + 22) * factorSize;
		//msgwY = (msgwPosY + 22) * factorSize;

	    //GUI.Label(new Rect(msgwX, msgwY, msgwW, msgwH),"massgae" ,msgWnd);
			
		myStyle.normal.textColor = Color.black;
		msgwX = (msgwPosX + 170) * factorSize;
		msgwY = (msgwPosY + 0) * factorSize;


		//取得Cube整體狀況
		AllCube AllCube;
		string Path = "/Cubes/";
		AllCube = (AllCube)GameObject.Find (Path).GetComponent<AllCube> ();


		int noCheckCube = AllCube.noCheckCube;
		int landmines = AllCube.landmines;

		int times = (int)(Time.time / 1);
		//string Massage = "開始時間："+times+" , 地雷數量： 5/25";
		string Massage = "開始時間："+times+" , 地雷數量："+landmines+"/"+noCheckCube;

		myStyle.fontSize =  25 ;
		GUI.Label(new Rect(msgwX, msgwY, msgwW, msgwH) ,Massage ,myStyle);

	}
}
