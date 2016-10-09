using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	ScoreManager score;
	// Use this for initialization
	void Start () {
		Destroy(gameObject,5.0f);
		score = GameObject.FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (gameObject);
		if(coll.gameObject.tag == "Enemy"){
			Debug.Log ("Bullet collided");
			coll.gameObject.SetActive(false);
			score.addScore (100);
		}else if(coll.gameObject.tag == "Player"){
			if (coll.gameObject.GetComponent<PlayerManager> ().weaponLevel > 1) {
				coll.gameObject.GetComponent<PlayerManager> ().DownGradeWeapon ();
			} else {
				coll.gameObject.SetActive (false);
				coll.gameObject.GetComponent<Renderer> ().enabled = false;
			}
		}
	}
}
