using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public Vector2 dir;
	public float speed;
	private float dist;
	public float distIni;
	// Use this for initialization
	private Vector3 posIni;

	void Start () {
		dir = new Vector2(0,0);
		speed = 0;
		dist = 1;
		distIni = 1;
		posIni = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
		float movX = dir.x * speed * Time.deltaTime;
		float movY = dir.y * speed * Time.deltaTime;
		transform.position = new Vector3 (transform.position.x + movX,
		                                 transform.position.y + movY, 0);
		dist -= Vector2.SqrMagnitude (new Vector2 (movX, movY));
		if (dist <= 0) {
			dist = distIni*2;
			dir *= -1;
		}
	}

	public void Restart(bool lose){
		transform.position = posIni;
		dist = 1;
		if (lose) {
			dir = new Vector2 (0, 0);
			speed = 0;
			//distIni = 0;
		}
	}
}
