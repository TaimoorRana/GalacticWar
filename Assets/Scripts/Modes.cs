using UnityEngine;
using System.Collections;

public class Modes : MonoBehaviour {
	public static bool insanity = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadNormalMode(){
		DontDestroyOnLoad (this);
		Application.LoadLevel("Main");
	}

	public void loadInsanityMode(){
		insanity = true;
		DontDestroyOnLoad (this);
		Application.LoadLevel("Main");
	}
}
