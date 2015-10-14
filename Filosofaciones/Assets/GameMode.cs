using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

	public float vel;
	GameObject player;
	GameObject rope;
	GameObject cam;
	GameObject left;
	GameObject right;
	public int state;

	void Start(){
		rope = GameObject.Find ("Cuerda");
		player = GameObject.Find ("Personaje");
		left = GameObject.Find("Left");
		right = GameObject.Find("Right");
		cam = GameObject.Find ("Main Camera");
		state = 0;
	}

	void Update(){
		if (state == 0) {

			float posX = -999;
			if (Input.touchCount > 0)
				posX = Camera.main.ScreenToWorldPoint(Input.touches [0].position).x;
			else if (Input.GetMouseButton (0))
				posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			if (posX != -999){
				if (posX < 0){
					vel = 0;
				}else{
					vel = 1;
				}
				Destroy (transform.GetChild(1).gameObject);
				Destroy (transform.GetChild(0).gameObject);
				state = 1;
			}
		} else {
			player.transform.position -= new Vector3 (0, vel * Time.deltaTime, 0);
			left.transform.position -= new Vector3 (0, vel * Time.deltaTime, 0);
			right.transform.position -= new Vector3 (0, vel * Time.deltaTime, 0);
			rope.transform.position -= new Vector3 (0, vel * Time.deltaTime, 0);
			cam.transform.position = new Vector3 (0, rope.transform.position.y - 3.18f, -10);
		}
	}
}
