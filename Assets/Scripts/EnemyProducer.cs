using UnityEngine;
using System.Collections;

public class EnemyProducer : MonoBehaviour {
	
	[SerializeField] float padding;

	public GameObject enemyGroup;
	public float angleSpeed = 5f;
	public float speed;
	public bool sin = false;
	public bool produceEnemies = true;

	EnemyFormation enemyFormation;

	Transform enemyFormationTransform;
	float yBoundary,xBoundary, currentY, currentX;
	float angle = 0;
	bool goingDown = true, goingRight = true;



	// Use this for initialization
	void Start () {
		// Create enemyformation
		enemyGroup = Instantiate (enemyGroup);
		enemyGroup.transform.parent = transform;
		enemyFormation = enemyGroup.GetComponent<EnemyFormation>();
		yBoundary = Camera.main.orthographicSize;
		enemyFormationTransform = enemyGroup.transform;
		xBoundary =  (Screen.width / Screen.height) * yBoundary;
	}
	
	// Update is called once per frame
	void Update () {
		
		// check if all enemies are dead, if it is the case, create new enemies
		if (enemyFormation.hasEnemy ()) {
			MoveEnemiesVertically ();

		} 




		if(!sin)
			enemyFormationTransform.position = new Vector3 (enemyFormationTransform.position.x,currentY,enemyFormationTransform.position.z);
		else
			enemyFormationTransform.position = new Vector3 (currentX,currentY,enemyFormationTransform.position.z);


	}


	void MoveEnemiesVertically(){

		if (goingDown) {
			currentY -= Time.deltaTime * speed;
			if (currentY < -yBoundary + enemyFormation.getLowestShipYPosition () + padding )
				goingDown = false;
		} else {
			currentY += Time.deltaTime * speed;
			if (currentY >= yBoundary - enemyFormation.getHighestShipYPosition () - padding)
				goingDown = true;

		}

		if (sin) {
			if (goingRight) {
				angle += Time.deltaTime * angleSpeed;
				currentX += Mathf.Sin (angle);
				if (currentX >= xBoundary) {
					goingRight = false;
				}
			} else {
				angle -= Time.deltaTime * angleSpeed;
				currentX += Mathf.Sin (angle);
				if (currentX < -xBoundary) {
					goingRight = true;
				}
			}	
		}
	}

	void GetLastEnemyPosition(){
		if (enemyFormation.FindLastEnemyPosition () != Vector2.zero) {
			
		}
	}

}
