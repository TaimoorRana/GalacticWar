using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
	[SerializeField] GameObject health;
	[SerializeField] Transform[] healthLocations;
	int playerHealth = 0;
	PlayerManager player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerManager> ();
		addHealth ();
	}
	
	// Update is called once per frame
	void Update () {
		playerHealth = player.weaponLevel;
	}

	public void addHealth(){
		if (playerHealth < 3) {
			GameObject healthCopy = Instantiate (health) as GameObject;
			healthCopy.transform.parent = healthLocations [playerHealth];
			healthCopy.transform.position = healthLocations [playerHealth].position;
			playerHealth++;
		}
	}

	public void removeHealth(){
		if (playerHealth >= 0) {
			Destroy(healthLocations [playerHealth - 1].GetChild(0).gameObject);
			playerHealth--;
		}
	}
}
