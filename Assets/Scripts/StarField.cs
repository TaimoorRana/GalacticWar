using UnityEngine;
using System.Collections;

public class StarField : MonoBehaviour {
	public float scrollSpeed;
	Material material;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		material.mainTextureOffset += new Vector2 (0, Time.deltaTime * scrollSpeed);
	}
}
