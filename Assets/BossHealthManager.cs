using UnityEngine;
using System.Collections;

public class BossHealthManager : MonoBehaviour {

	[SerializeField] GameObject health;
	[SerializeField] Transform[] healthLocations;
	int bossHealth = 0;
	// Use this for initialization
	void Start () {
		addHealth ();
	}

	// Update is called once per frame
	void Update () {

	}

	void addHealth(){
		foreach(Transform trans in healthLocations){
			bossHealth++;
			GameObject healthCopy = Instantiate (health) as GameObject;
			healthCopy.transform.parent = healthLocations [bossHealth - 1];
			healthCopy.transform.position = healthLocations [bossHealth - 1].position;
		}
	}

	public void removeHealth(){
		if (bossHealth > 0) {
			Destroy(healthLocations [bossHealth - 1].GetChild(0).gameObject);
			bossHealth--;
		}
	}
}
