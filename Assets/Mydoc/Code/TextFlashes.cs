using UnityEngine;
using System.Collections;

public class TextFlashes : MonoBehaviour {

	GameObject SystemText;
	MeshRenderer MeshRenderer;

	void Start () {
		SystemText = GameObject.Find ("/Text");
		MeshRenderer = SystemText.GetComponent<MeshRenderer> ();
		//使文字閃爍
		InvokeRepeating ("change",0,0.5f);
	}
	
	// Update is called once per frame
	void Update () {

		//Enter -> GameStart
		if (Input.GetKeyDown (KeyCode.KeypadEnter)) {
			Application.LoadLevel ("01_MainGame");
		}
		
	}

	//文字enable開關
	void change(){

		if (MeshRenderer.enabled) {
			MeshRenderer.enabled = false;
		} else {
			MeshRenderer.enabled = true;
		}
			
	}

}
