using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject LeftPortal;
	public GameObject RightPortal;
	public GameObject mainCamera;
	public float speed = 3.0f;
	bool moveforward = false;
	//bool clickmove = false;
	bool SingleClick = false;
	bool Doubleclick = false;
	bool Standing = true;
	CharacterController cc;
	//Rigidbody rigidbody;
	float lastclicktime = 0.0f;
	float catchtime = 0.25f;
	int flag = 0;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		audio = GetComponent<AudioSource> ();
		flag = 0;
		//rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		SingleClick = false;

		if(Input.GetKeyDown (KeyCode.Mouse0)){
			if(Time.time - lastclicktime < catchtime){
				Doubleclick = true;
				//print("Double Click!");
				//print("done:" + (Time.time - lastclicktime).ToString());
			}else{
				Doubleclick = false;
				//print("Single Click!");
				//print("miss:" + (Time.time - lastclicktime).ToString());
			}
			lastclicktime = Time.time;
			SingleClick = true;
		}

		if (Doubleclick) {
			moveforward = false;
			Standing = true;
			Doubleclick = false;
			//print ("shoot!");

			if (flag == 0) {
				//print ("Left Portal!");
				shootPortal (LeftPortal);
				flag = 1;
			} else if (flag == 1) {
				//print ("Right Portal!");
				shootPortal (RightPortal);
				flag = 0;
			} else {
				print ("Invalid flag value!");
			}

		} else {
			if (Standing) {
				if (SingleClick) {
					moveforward = true;
					Standing = false;
				}
			} else {
				moveforward = true;
				//Doubleclick = false;

				if (SingleClick) {
					moveforward = false;
					Standing = true;
				}
			}
		}

		if(moveforward)
		{
			Vector3 forward = mainCamera.transform.TransformDirection (Vector3.forward);
			cc.SimpleMove (forward * speed);
			//rigidbody.transform.Translate(forward * 0.05f);
		}
	}

	void shootPortal(GameObject portal){
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		Ray ray = mainCamera.GetComponent<Camera> ().ScreenPointToRay (new Vector3 (x, y));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			portal.transform.position = hit.point;
			portal.transform.rotation = Quaternion.LookRotation (hit.normal);
			audio.Play ();
		}
	}

}
