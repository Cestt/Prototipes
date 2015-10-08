using UnityEngine;
using System.Collections;

public class GenerateBall : MonoBehaviour {

	public GameObject prefabBall;
	float vel = 5;
	public int numBall = 0;
	public float timeGame = 1;
	public int record;
	// Update is called once per frame
	TextMesh textRecord;
	public int count;
	TextMesh textCount;

	void Start(){
		count = 0;
		record = PlayerPrefs.GetInt ("Record", 0);
		textRecord = GameObject.Find ("Main Camera").transform.FindChild("Record").GetComponent<TextMesh> ();
		textRecord.text = "" + record;
		textCount = GameObject.Find ("Main Camera").transform.FindChild("Count").GetComponent<TextMesh> ();
	}
	void Update () {

		if ((Input.touchCount > 0 && Input.touches [0].phase == TouchPhase.Began)|| Input.GetKeyDown(KeyCode.Space)) {
			GameObject ball = (GameObject)Instantiate(prefabBall,transform.position,Quaternion.identity);
			//ball.GetComponent<Ball>().vel = vel;
			//vel += 2;
			//Time.timeScale += 0.1f;
			numBall++;
		}
		Time.timeScale = timeGame;
		textCount.text = "" + count;
	}


}
