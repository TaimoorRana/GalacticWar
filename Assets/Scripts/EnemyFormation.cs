using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFormation : MonoBehaviour {
	[SerializeField] GameObject BossGameObject;
	[SerializeField] int totalWaves;
	[SerializeField] GameObject powerUp;
	bool powerUpDropped = false;
	float lowestShip, highestShip, leftestShip, rightestShip;
	Transform[] allChildren;
	List<Transform> enemyShips;
	Transform originalPosition;
	Vector2 lastEnemyPosition;

	bool producing = false;



	void Start () {
		allChildren =  GetComponentsInChildren<Transform> ();
		enemyShips = new List<Transform> ();
		searchLowestAndHighestShipPosition ();
		originalPosition = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasEnemy () && totalWaves > 0 && !producing) {
			totalWaves--;
			Invoke ("activateEnemies", 3f);
			producing = true;
			if(GameObject.FindObjectOfType<PlayerManager>().weaponLevel < 3)
			givePowerUp();
		}
			
	}

	void searchLowestAndHighestShipPosition(){

		// find direct children only
		foreach (Transform trans in allChildren) {
			if (trans.parent == this.transform) {
				enemyShips.Add (trans);
			}
		}

		// initialize values for lowest and highest ship
		lowestShip = enemyShips [0].position.y;
		highestShip = enemyShips [0].position.y;

		// fint the lowest and highest ship y position
		foreach (Transform trans in enemyShips){
			if (trans.parent == transform) {
				if (trans.position.y < lowestShip)
					lowestShip = trans.position.y;
				if (trans.position.y > highestShip)
					highestShip = trans.position.y;
				if (trans.position.x < leftestShip)
					leftestShip = trans.position.x;
				if (trans.position.x > rightestShip)
					rightestShip = trans.position.x;		

			}
		}
	}

	public float getLowestShipYPosition(){
		return lowestShip;
	}

	public float getHighestShipYPosition(){
		return highestShip;
	}

	public float getLeftestShipXPosition(){
		return leftestShip;
	}

	public float getRightestShipXPosition(){
		return rightestShip;
	}

	public bool hasEnemy(){
		if (GetComponentsInChildren<EnemyManager> ().Length >= 1) {
			producing = false;
			return true;
		}

		return false;
	}

	public void activateEnemies(){
		

		if(totalWaves < 0)
			return;
		
		if (totalWaves > 0) {
			
			transform.position = originalPosition.position;
			foreach (Transform t in enemyShips) {
				t.gameObject.SetActive (true);
			}

		} else {
			GameObject boss = Instantiate (BossGameObject) as GameObject;
			boss.transform.parent = this.transform;
			boss.transform.position = new Vector3 (boss.transform.position.x, boss.transform.parent.position.y, boss.transform.position.z);
		}
		producing = false;

	}

	public Vector2 FindLastEnemyPosition(){
		if (GetComponentsInChildren<EnemyManager> ().Length != 0) {
			foreach (Transform trans in allChildren) {
				if (trans.parent == this.transform) {
					lastEnemyPosition = trans.position;
					break;
				}
			}
		} else {
			return lastEnemyPosition;
		}

		return Vector2.zero;
	}

	public void givePowerUp(){
		GameObject powerUpCopy = Instantiate (powerUp);
		powerUpCopy.transform.position = FindLastEnemyPosition ();
		powerUpDropped = true;
	}
}
