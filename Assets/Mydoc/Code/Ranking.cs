using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {

	public GUIStyle rankingWnd;
	float msgwWidth;
	float msgwHeigh;
	float msgwX;
	float msgwY;
	float msgwW;
	float msgwH;
	int[] ranking = new int[11];

	int playerSeconds;
	bool textRedFlag;

	void Start () {
		enabled = false;
	}

	void Update () {
	}

	void OnGUI(){
		textRedFlag = false;

		msgwX = (Screen.width / 10) * 6;
		msgwY = (Screen.height / 10) * 1;
		msgwWidth = (Screen.width / 10) * 3;
		msgwHeigh = (Screen.height / 10) * 7;

		GUI.Box (new Rect (msgwX , msgwY, msgwWidth, msgwHeigh),"");

		float rankingTextX = msgwX + (msgwWidth / 10);
		float rankingTextY;
		GUIStyle myStyle = new GUIStyle();
		myStyle.fontSize = 25;
		myStyle.normal.textColor = Color.white;
		for (int i = 0; i < 10; i++) {
			//YのPosY+10から表示される、毎回枠の10％のHeightを追加する
			rankingTextY = msgwY + ((msgwHeigh / 10)) * (i) + 5;

			if (ranking [i] == playerSeconds && !textRedFlag) {
				myStyle.normal.textColor = Color.red;
				textRedFlag = true;
			} else {
				myStyle.normal.textColor = Color.white;
			}

			GUI.Label (new Rect (rankingTextX, rankingTextY , 100, 100),
				(i+1)+"st： "+(string)ranking[i].ToString(), myStyle);
		}

	}

	//ソートする
	public void RankingSort(){

		int tmp = 0;
	
		for (int i = 0; i < 11; i++) {
			for(int j=0; j<11;j++){

				if (j < i || j==i) {
					continue;
				}

				if (ranking [i] > ranking [j]) {
					tmp = ranking [i];
					ranking [i] = ranking [j];
					ranking [j] = tmp;
				}
		
			}
		}
			

		//記録を更新する
		string save = "";
		for (int i = 0; i < 10; i++) {

			save = save + ranking[i]+",";
		}
		PlayerPrefs.SetString("ranking",save.Substring(0,save.Length-1));
	}

	//Rankingを取得する、初めてなら初期値
	public void GetRanking(int playerSecond){

		if (!PlayerPrefs.HasKey ("ranking")) {
			PlayerPrefs.SetString("ranking","200,300,350,400,500,600,900,800,700,999");
		} 
			
		string rankingSave = PlayerPrefs.GetString ("ranking");
		string[] rankingSaveStringArray = rankingSave.Split (',');

		for (int i = 0; i < 10; i++) {
			ranking[i] = int.Parse(rankingSaveStringArray[i]);
		}

		ranking [10] = playerSecond;
		playerSeconds = playerSecond;
	}
		
}
