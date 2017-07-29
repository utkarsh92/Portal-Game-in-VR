using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public GameObject otherPortal;
	public int throwSpeed;
	AudioSource audio;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//print ("something hits!");
		if (other.gameObject.tag == "Player") {
			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward;

			float rotDiff = Quaternion.Angle(transform.rotation, otherPortal.transform.rotation);
			Debug.Log ("rotDiff: " + rotDiff);
			rotDiff += 180;
			Vector3 cross = Vector3.Cross (transform.forward, otherPortal.transform.forward);
			if (cross.y < 0) {
				rotDiff = -rotDiff;
			}
			other.transform.Rotate(Vector3.up, rotDiff);

			audio = otherPortal.GetComponent<AudioSource> ();
			audio.Play ();

		}else if (other.gameObject.tag == "Pickable") {
			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward;
			other.transform.rotation = otherPortal.transform.rotation;
			other.GetComponent<Rigidbody> ().AddForce (otherPortal.transform.forward);
			audio = otherPortal.GetComponent<AudioSource> ();
			audio.Play ();
		}
	}

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Pickable") {
			other.transform.position = otherPortal.transform.position + otherPortal.transform.forward;
			other.transform.rotation = otherPortal.transform.rotation;
			other.GetComponent<Rigidbody> ().AddForce (otherPortal.transform.forward * throwSpeed);
			audio = otherPortal.GetComponent<AudioSource> ();
			audio.Play ();
		}
	}
}
