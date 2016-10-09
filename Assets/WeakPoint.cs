using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {
	FinalBoss boss;
	// Use this for initialization
	void Start () {
		boss = transform.parent.gameObject.GetComponent<FinalBoss> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Bullet") {
			Debug.Log ("got hit");
			boss.HitWeakPoint (gameObject.name);
		}

	}
}
