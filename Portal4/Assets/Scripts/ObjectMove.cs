using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {

	public int speed = 1;
	int flag = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (flag == 0) {
			if (transform.position.z < 10.0f) {
				transform.Translate (Vector3.forward * Time.deltaTime * speed);
			} else {
				flag = 1;
			}
		} else {
			if (transform.position.z > -10.0f) {
				transform.Translate (Vector3.back * Time.deltaTime * speed);
			} else {
				flag = 0;
			}
		}
	}
}
