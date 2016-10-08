using UnityEngine;
using System.Collections;

public class FinalBoss : EnemyManager {
	[SerializeField] GameObject bullet;
	[SerializeField] Transform[] bulletSpawnLocations;
	[SerializeField] float rotationSpeed;
	public float speed;

	float shootInterval;
	float yBoundary,xBoundary, currentY, currentX;
	bool goingDown = true, goingRight = true;

	// Use this for initialization
	void Start () {
		shootInterval = Random.Range(0.5f,1.0f);
		yBoundary = Camera.main.orthographicSize;
		xBoundary =  (Screen.width / Screen.height) * yBoundary;
		InvokeRepeating("LaunchProjectile", shootInterval, shootInterval);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,Time.deltaTime * rotationSpeed));
		//MoveEnemiesVertically ();
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
		transform.position = new Vector3 (transform.position.x, currentY, transform.position.z);
	}

}
