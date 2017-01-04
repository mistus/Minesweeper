using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private IEnumerator coroutine;
	float speed = 5f;
	float angle;
	float angleZ;

	GameObject Elf;
	GameObject UnityChan;
	Vector3 direction;
	Quaternion unitychanRotation;


	void Start () {
		Elf = GameObject.Find ("/Player/unitychan/Elf");
		UnityChan = GameObject.Find ("/Player/unitychan");
		coroutine = TimeDelay (2f);
		StartCoroutine(coroutine);

		Vector3 relative = UnityChan.transform.InverseTransformPoint (Elf.transform.position);
		angle = Mathf.Atan2 (relative.x, relative.z) * Mathf.Rad2Deg;

		unitychanRotation = UnityChan.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {


		direction = new Vector3 (0, 0, 1);
		direction =  unitychanRotation * direction;


		Quaternion target = Quaternion.Euler(0, angle, 0);
		direction = target * direction;
		transform.position	+= direction * Time.deltaTime * speed;

	}


	IEnumerator TimeDelay(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy (gameObject);
	}
}
