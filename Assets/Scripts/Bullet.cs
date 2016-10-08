using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (gameObject);
		if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player" ){
			Debug.Log ("Bullet collided");
			coll.gameObject.SetActive(false);

		}
	}
}
