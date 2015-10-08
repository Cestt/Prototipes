using UnityEngine;
using System.Collections;

public class Procedural_Manager : MonoBehaviour {

	Ball ball;
	Target target;
	TextMesh levelText;
	int level = 1;
	int next = 1;
	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball").GetComponent<Ball> ();
		target = GameObject.Find ("Target").GetComponent<Target>();
		levelText = transform.FindChild ("Level").GetComponent<TextMesh> ();
	}
	
	public void NextLevel(){
		level++;
		levelText.text = "Level\n" + level.ToString ();
		/*if (next == 1) {
			next++;
			target.dir = new Vector2 (1, 0);
			target.speed += 2;
		} else if (next == 2) {
			next++;
			target.dir = new Vector2 (0, 1);
		} else if (next == 3) {
			ball.gravity += 5;
			target.dir = new Vector2(0,0);
			next = 1;
		}*/
		ball.gravity += 5;
		ball.Restart(false);
		target.Restart (false);
	}

	public void Restart(){
		ball.Restart (true);
		target.Restart (true);
		level = 1;
		levelText.text = "Level\n" + level.ToString ();
		next = 1;
	}
}
