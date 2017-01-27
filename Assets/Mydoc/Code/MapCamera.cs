using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {
	public Camera Map_Camera;

	void Start () {
		Map_Camera.enabled = false;
	}
	

	void Update () {
		if(Input.GetKeyDown(KeyCode.M)){
			if (Map_Camera.enabled) {
				Map_Camera.enabled = false;
			} else {
				Map_Camera.enabled = true;
			}

		}
	}
		
}
