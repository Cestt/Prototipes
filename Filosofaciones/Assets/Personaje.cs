using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour {

	public int coins;
	// Use this for initialization
	public TextMesh coinsText;
	void Start () {
		coins = 0;
		coinsText = GameObject.Find ("CoinsText").GetComponent<TextMesh> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "muro") {
			Application.LoadLevel("main");
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.name.Contains ("Coin")) {
			coins++;
			coinsText.text = "" + coins;
			Destroy (other.gameObject);
		} else if (other.name.Contains ("Enemy")) {
			Application.LoadLevel("main");
		}
	}
}
