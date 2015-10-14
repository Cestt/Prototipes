using UnityEngine;
using System.Collections;

public class Cuerda : MonoBehaviour {

	public float velDesc = 5;
	GameObject left;
	GameObject right;
	GameMode mode;
	GameObject pj;
	// Use this for initialization
	float size;
	void Start () {
		left = GameObject.Find("Left");
		right = GameObject.Find("Right");
		size = Vector3.Distance (left.transform.position, right.transform.position);
		mode = GameObject.Find ("GameMode").GetComponent<GameMode> ();
		pj = GameObject.Find ("Personaje");
	}
	
	// Update is called once per frame
	void Update () {
		if (mode.state == 0)
			return;
		float posX = -999;
		if (Input.touchCount > 0)
			posX = Camera.main.ScreenToWorldPoint(Input.touches [0].position).x;
		else if (Input.GetMouseButton (0))
			posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		if (posX != -999) {
			if (posX < 0){
				if (mode.vel == 0)
					left.transform.position = new Vector3(left.transform.position.x,
				                                      left.transform.position.y-(Time.deltaTime*velDesc),0);
				else{
					left.transform.position = new Vector3(left.transform.position.x,
					                                      left.transform.position.y-(Time.deltaTime*velDesc),0);
					right.transform.position = new Vector3(right.transform.position.x,
					                                       right.transform.position.y+(Time.deltaTime*velDesc),0);
				}
			}else{
				if (mode.vel == 0)
					right.transform.position = new Vector3(right.transform.position.x,
				                                      right.transform.position.y-(Time.deltaTime*velDesc),0);
				else{
					left.transform.position = new Vector3(left.transform.position.x,
					                                      left.transform.position.y+(Time.deltaTime*velDesc),0);
					right.transform.position = new Vector3(right.transform.position.x,
					                                       right.transform.position.y-(Time.deltaTime*velDesc),0);
				}
			}
			transform.eulerAngles = new Vector3(0,0,Vector3.Angle(right.transform.position - left.transform.position,new Vector3(1,0,0)));
			if (right.transform.position.y < left.transform.position.y)
				transform.eulerAngles*=-1;
			transform.position = ((right.transform.position- left.transform.position)/2)+ left.transform.position;
			transform.localScale = new Vector3(2*Vector3.Distance (left.transform.position, right.transform.position)/size,1,1);
			float nuevaY = (pj.transform.position.x - left.transform.position.x) * (right.transform.position.y - left.transform.position.y);
			nuevaY /= (right.transform.position.x - left.transform.position.x);
			nuevaY += left.transform.position.y + 0.05f + 0.26f;
			if (pj.transform.position.y - nuevaY > 0.01f)
				pj.transform.position = new Vector3 (pj.transform.position.x, nuevaY, 0);
		}

	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.name == "Personaje") {
			float nuevaY = (pj.transform.position.x - left.transform.position.x) * (right.transform.position.y - left.transform.position.y);
			nuevaY /= (right.transform.position.x - left.transform.position.x);
			nuevaY += left.transform.position.y + 0.05f + 0.26f;
			pj.transform.position = new Vector3 (pj.transform.position.x, nuevaY, 0);
			//Debug.Log (nuevaY);
		}
	}
}
