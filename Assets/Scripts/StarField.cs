using UnityEngine;
using System.Collections;

public class StarField : MonoBehaviour {
	public float scrollSpeed;
	Material material;
	bool scroll = true;
	ParticleSystem particle;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
		particle = GetComponentInChildren<ParticleSystem> ();
		Invoke ("ScrollSwitch", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(scroll)
			material.mainTextureOffset += new Vector2 (0, Time.deltaTime * scrollSpeed);
	}

	public void ScrollSwitch(){
		scroll = !scroll;
		if (scroll) {
			particle.Play ();
		} else {
			particle.Pause ();
		}
	}
}
