using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	DistanceJoint2D joint;
	TextMesh timer;
	float time;
	Rigidbody2D rigibody;
	public float gravity;
	public float gravityIni;
	Vector3 posIni;
	Procedural_Manager procedural;
	public float force;
	private float velocity;
	private int dir;
	public float fastForward;

	void Start () {
		joint = GetComponent<DistanceJoint2D> ();
		timer = GameObject.Find ("Timer").GetComponent < TextMesh> ();
		time = 2.00f;
		timer.text = time.ToString ("F2");
		rigibody = GetComponent<Rigidbody2D> ();
		posIni = transform.position;
		rigibody.gravityScale = gravity;
		gravityIni = gravity;
		procedural = GameObject.Find ("Procedural_Manager").GetComponent<Procedural_Manager> ();
		dir = -1;
		velocity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (dir == -1 && rigibody.velocity.y >= 0) {
			if (velocity == 0) {
				velocity = rigibody.velocity.x;
			} else {
				if (rigibody.velocity.x < 0)
					rigibody.velocity = new Vector2 (-velocity, 0);
				else
					rigibody.velocity = new Vector2 (velocity, 0);
			}
			dir = 1;
		} else if (dir == 1 && rigibody.velocity.y <= 0) {
			dir = -1;
		}
		if (joint.enabled) {
			/*if (Input.touchCount > 0) {
				joint.enabled = false;
				rigibody.gravityScale = 5;
			}*/
			if (Input.GetKeyUp (KeyCode.Space) || Input.touchCount > 0) {
				joint.enabled = false;
				rigibody.AddForce (rigibody.velocity.normalized * force);
				rigibody.gravityScale = gravity/4f;
			}
		} else {
			Time.timeScale = fastForward;
		}
	}

	public void Restart(bool lose){
		if (lose) {
			gravity = gravityIni;
		}
		Time.timeScale = 1;
		rigibody.gravityScale = gravity;
		transform.position = posIni;
		rigibody.velocity = new Vector2 (0, 0);
		joint.enabled = true;
		time = 2;
		velocity = 0;
		timer.text = time.ToString("F2");
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.name.Contains ("Limit")) {
			procedural.Restart();
		}
		if (other.name == "Win") {
			procedural.NextLevel();
			timer.text = time.ToString("F2");
			Time.timeScale = 1;
			velocity = 0;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.name == "Win") {
			time -= Time.deltaTime;
			timer.text = time.ToString("F2");
			Time.timeScale = 1;
			if (time <= 0){
				procedural.NextLevel();
			}
		}
	}
}
