using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFormation : MonoBehaviour {
	[SerializeField] GameObject BossGameObject;
	float lowestShip, highestShip;
	Transform[] allChildren;
	List<Transform> enemyShips;
	Transform originalPosition;
	Vector2 lastEnemyPosition;



	void Start () {
		allChildren =  GetComponentsInChildren<Transform> ();
		enemyShips = new List<Transform> ();
		searchLowestAndHighestShipPosition ();
		originalPosition = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
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
			}
		}
	}

	public float getLowestShipYPosition(){
		return lowestShip;
	}

	public float getHighestShipYPosition(){
		return highestShip;
	}

	public bool hasEnemy(){
		if (GetComponentsInChildren<EnemyManager> ().Length >= 1)
			return true;

		return false;
	}

	public void activateEnemies(){
		transform.position = originalPosition.position;
		foreach(Transform t in enemyShips){
			t.gameObject.SetActive (true);
		}
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
}
