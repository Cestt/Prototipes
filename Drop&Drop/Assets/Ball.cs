using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public bool rebota = true;

	public float vel;
	private Rigidbody rigi;
	private GameObject cam;
	private GenerateBall generate;
	public bool isSlow;
	public Vector3 rigiVel;
	private float difCam;
	public float velDif; 

	void Start(){
		isSlow = false;
		rigi = GetComponent<Rigidbody> ();
		cam = GameObject.Find ("Main Camera");
		generate = GameObject.Find ("Main Camera").transform.FindChild ("BallGenerate").GetComponent<GenerateBall> ();
		//vel = 10;

		float rand = Random.value;
		if (name != "Start" && rand < 0.2f) {
			vel = 9.5f;
			GetComponent<SpriteRenderer> ().color = Color.green;
			generate.GetComponent<SpriteRenderer> ().color = Color.green;
		} else if (name != "Start" && rand > 0.8f) {
			vel = 6.5f;
			GetComponent<SpriteRenderer> ().color = Color.red;
			generate.GetComponent<SpriteRenderer> ().color = Color.red;
		} else {
			generate.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	void Update(){
		rigiVel = rigi.velocity;
		if (rigi.velocity.y > -velDif && rigi.velocity.y < velDif) {
			/*if (vel > 0)
				Debug.Log ("Ahora : " + vel);*/
			isSlow = true;
		} else {
			isSlow = false;
		}
		if (!rebota && rigi.velocity.y > 0 && transform.position.y > -2.5f) {
			cam.transform.position =  Vector3.MoveTowards(cam.transform.position,new Vector3(0,transform.position.y+2.5f,-10),0.12f);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Limit") {
			generate.numBall--;
			if (generate.numBall == 0) {
				Application.LoadLevel("main");
				if (generate.count > generate.record)
					PlayerPrefs.SetInt("Record",generate.count);
			}
			if (rebota){
				if (generate.count > generate.record)
					PlayerPrefs.SetInt("Record",generate.count);
				Application.LoadLevel("main");
			}
			else
				Destroy (gameObject);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "Start") {
			difCam = cam.transform.position.y - transform.position.y;
			rebota = false;
			rigi.velocity = new Vector3(0,vel,0);
			generate.timeGame += 0.05f;
			collision.gameObject.GetComponent<SphereCollider>().enabled = false;
			generate.count++;
		}
		if (rebota && (collision.gameObject.name.Contains("Ball") )) {
			if (collision.gameObject.GetComponent<Ball>().isSlow){
				difCam = cam.transform.position.y - transform.position.y;
				rebota = false;
				rigi.velocity = new Vector3(0,vel,0);
				generate.timeGame += 0.05f;
				generate.count++;
			}
		}
	}
}
