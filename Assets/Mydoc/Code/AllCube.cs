using UnityEngine;
using System.Collections;

public class AllCube : MonoBehaviour {

	public int noCheckCube = 0;
	public int landmines = 5;
	Cube Cubes;
	string Path;
	int normalCubeNoDisplay = 0;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		updateCubeStatus ();

	}

	//更新方塊狀態(更新文字用)
	void updateCubeStatus(){
		
		noCheckCube=0;
		landmines = 5;
		normalCubeNoDisplay = 0;
		for (int i = 1; i < 6; i++) {
			for (int j = 1; j < 6; j++) {


				Path = "/Cubes/Cubes" + i + "/Cube" + j + "/";
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

		if (normalCubeNoDisplay == 0) {
			UnityChanMove unitychaN = (UnityChanMove)GameObject.Find ("/Player/unitychan").GetComponent<UnityChanMove>();

			if (unitychaN.StageClear) {
				return;
			}

			//普通方塊歸零 >勝利
			unitychaN.GameClear();

		}

	}

}
