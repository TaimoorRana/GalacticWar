using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	int score = 0;
	Text text;
	bool sinActive = false;
	EnemyProducer enemyProducer;
	int scoreMultiplier = 1;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		enemyProducer = GameObject.FindObjectOfType<EnemyProducer> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = score.ToString();
	}

	public void addScore(int points){
		
		sinActive = enemyProducer.sin;
		if (sinActive) {
			this.score += (points * 2 * scoreMultiplier);
		} else {
			this.score += points * scoreMultiplier;
		}
	}

	public void doubleScore(){
		scoreMultiplier *= 2;
	}
}
