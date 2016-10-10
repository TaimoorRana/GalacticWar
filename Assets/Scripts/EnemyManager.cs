using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	[SerializeField]
	GameObject bullet;
	[SerializeField]
	Transform bulletSpawnLocation;
	float shootInterval;
	ScoreManager score;

	// Use this for initialization
	void Start () {
		shootInterval = Random.Range(1.0f,3.0f);
		InvokeRepeating("LaunchProjectile", shootInterval, shootInterval);
		score = GameObject.FindObjectOfType<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void LaunchProjectile(){
		if (gameObject.activeSelf) {
			GameObject bulletCopy = Instantiate (bullet);
			bulletCopy.GetComponent<Bullet> ().turnOffInsanity ();
			bulletCopy.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -0.2f));
			bulletCopy.transform.position = bulletSpawnLocation.position;
			bulletCopy.transform.Rotate (180, 0, 0);

		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Bullet") {
			gameObject.SetActive (false);
			score.addScore (100);
		} else if (coll.gameObject.tag == "Player") {
			GameObject.FindObjectOfType<PlayerManager> ().DownGradeWeapon ();
		}
	}


}
