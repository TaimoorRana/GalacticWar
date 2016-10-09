using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
	[SerializeField] GameObject health;
	[SerializeField] Transform[] healthLocations;
	int playerHealth = 0;
	PlayerManager player;
	// Use this for initialization
	void Start () {
		addHealth ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addHealth(){
		if (playerHealth < 3) {
			playerHealth++;
			GameObject healthCopy = Instantiate (health) as GameObject;
			healthCopy.transform.parent = healthLocations [playerHealth - 1];
			healthCopy.transform.position = healthLocations [playerHealth - 1].position;
		}
	}

	public void removeHealth(){
		if (playerHealth > 1) {
			Destroy(healthLocations [playerHealth - 1].GetChild(0).gameObject);
			playerHealth--;
		}
	}
}
