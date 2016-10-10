using UnityEngine;
using System.Collections;

public class Modes : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadNormalMode(){
		PlayerPrefs.SetInt ("insanity", 0);
		Application.LoadLevel("Main");

	}

	public void loadInsanityMode(){
		PlayerPrefs.SetInt ("insanity", 1);
		Application.LoadLevel("Main");
	}
}
