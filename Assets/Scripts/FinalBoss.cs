using UnityEngine;
using System.Collections;

public class FinalBoss : EnemyManager {
	[SerializeField] GameObject bullet;
	[SerializeField] float rotationSpeed;
	[SerializeField] GameObject core;

	public float speed;
	float shootInterval;
	float yBoundary,xBoundary, currentY, currentX;
	bool goingDown = true, goingRight = true;
	bool coreCreated = false;
	[SerializeField] Transform[] bulletSpawnLocations;

	int weakPoint1Hits = 0, weakPoint2Hits = 0, coreHits = 0;

	// Use this for initialization
	void Start () {
		shootInterval = Random.Range(3f,5f);
		yBoundary = Camera.main.orthographicSize;
		xBoundary =  (Screen.width / Screen.height) * yBoundary;
		InvokeRepeating("LaunchProjectile", shootInterval, shootInterval);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,Time.deltaTime * rotationSpeed));
		//MoveEnemiesVertically ();
	}

	public void HitWeakPoint( string WeakPointName){

		switch (WeakPointName) {
		case "WeakPoint1":
			if (weakPoint1Hits < 2)
				weakPoint1Hits++;
			break;
		case "WeakPoint2":
			if (weakPoint2Hits < 2)
				weakPoint2Hits++;
			break;
		case "Core(Clone)":
 			coreHits++;
			if (coreHits > 1)
				gameObject.SetActive (false);
			break;
		}

		if (weakPoint1Hits + weakPoint2Hits >= 4 && !coreCreated) {
			GameObject coreCopy = Instantiate (core) as GameObject;
			coreCopy.transform.parent = transform;
			coreCopy.transform.position = transform.position;
			coreCreated = true;
			Destroy(coreCopy,5f);
			// reset core health after 5 seconds
			Invoke ("resetHealth", 5f);
		}	
	}

	void resetHealth(){
		weakPoint1Hits = 0;
		weakPoint1Hits = 0;
		coreCreated = false;
	}


	void  LaunchProjectile(){
		if (gameObject.activeSelf) {
			foreach (Transform trans in bulletSpawnLocations) {
				GameObject bulletCopy = Instantiate (bullet);
				bulletCopy.GetComponent<Rigidbody2D> ().AddForce(trans.up * 0.2f);
				bulletCopy.transform.position = trans.position;
				bulletCopy.transform.rotation = trans.rotation;
				bulletCopy.transform.Rotate (180, 0, 0);
			}
		}

	}



	void MoveEnemiesVertically(){

		if (goingDown) {
			currentY -= Time.deltaTime * speed;
			if (currentY < -yBoundary + transform.position.y)
				goingDown = false;
			
		} else {
			currentY += Time.deltaTime * speed;
			if (currentY >= yBoundary - transform.position.y)
				goingDown = true;

		}
		transform.position = new Vector3 (transform.position.x, currentY, 0);
	}



}
