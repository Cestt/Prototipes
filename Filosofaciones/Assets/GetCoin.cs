using UnityEngine;
using System.Collections;

public class GetCoin : MonoBehaviour {

	Personaje pj;
	void Start(){
		pj = GameObject.Find ("Personaje").GetComponent<Personaje> ();
	}

	/*void Update(){
		transform.position = pj.gameObject.transform.position;
	}*/

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name.Contains ("Coin")) {
			pj.coins++;
			pj.coinsText.text = "" + pj.coins;
			Destroy (other.gameObject);
		} else if (other.name.Contains ("Enemy")) {
			Application.LoadLevel ("main");
		}
	}
}
