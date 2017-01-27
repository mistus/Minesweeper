using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour {

	public int noCheckCube = 0;
	public int landmines = 5;
	string Path;
	int normalCubeNoDisplay = 0;
	int Row = 0;
	int Column = 0;
	Cube Cubes;
	CreateStage CreateStage;

	void Start () {
		CreateStage = GetComponent<CreateStage>();
		Row = CreateStage.GetRow ();
		Column = CreateStage.GetColumn ();
	}
		
	void Update () {
		updateCubeStatus ();
	}

	//全部のキューブの状態をチェックして文字を作成する
	void updateCubeStatus(){
		
		noCheckCube=0;
		landmines = GetComponent<LandmineManager>().getLandmineQuantity();
		normalCubeNoDisplay = 0;

		for (int i = 1; i < Row+1; i++) {
			for (int j = 1; j < Column+1; j++) {

				Path = "/Cube"+i+"x"+j+"(Clone)";
				Cubes = (Cube)GameObject.Find (Path).GetComponent<Cube>();


				if (!Cubes.display) {
					noCheckCube++;
				}

				if (Cubes.playerCheck) {
					landmines--;
				}

				if (!Cubes.display && (!Cubes.Landmines)) {
					normalCubeNoDisplay++;
				}

			}
		}

		//普通のキューブがゼロになったら >　勝利
		if (normalCubeNoDisplay == 0) {
			UnityChanMove unitychan = (UnityChanMove)GameObject.Find ("/Player/unitychan").GetComponent<UnityChanMove>();
			Ranking ranking = (Ranking)GameObject.Find ("/GameManager").GetComponent<Ranking>();

			if (unitychan.StageClear) {
				return;
			}
				
			unitychan.GameClear();
			ranking.GetRanking ((int)Time.time);
			ranking.RankingSort ();	
			ranking.enabled = true;
		}

	}

}