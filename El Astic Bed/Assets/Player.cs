using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool touchBed = false;

	private Rigidbody rigi;
	public Sprite[] sprites;
	// Use this for initialization
	GameObject cam;
	float speed = 5;
	void Start(){
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range (0,sprites.Length)];
		rigi = GetComponent<Rigidbody> ();
		cam = GameObject.Find ("Main Camera");
	}
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space)) {
			if (rigi.velocity.y == 0){
				rigi.velocity = new Vector3(0,speed,0);
				speed += 1.5f;
				rigi.useGravity = true;
			}else if (rigi.velocity.y < 0 ){
				if (touchBed){
					rigi.velocity = new Vector3(0,speed,0);
					speed += 1.5f;
				}else{
					GameObject.Find("Bed").SetActive(false);
				}
			}
		}
		if (transform.position.y > 0) {
			cam.transform.position = new Vector3(0,transform.position.y, -10);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Bed") {
			touchBed = true;
		} else if (other.name == "Limit") {
			Application.LoadLevel("main");
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name == "Bed") {
			touchBed = false;
		}
	}
}
