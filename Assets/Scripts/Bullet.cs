using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int insanity;
	int screenLapCounter;
	int screenHeight, screenWidth;
	Renderer renderer;
	Camera mainCamera;
	bool isWrappingX = false;
	bool isWrappingY = false;
	bool offInsanity = false;

	// Use this for initialization
	void Start () {
		insanity = PlayerPrefs.GetInt ("insanity");
		if (insanity == 0) {
			Destroy (gameObject, 5.0f);
		}
		screenLapCounter = 0;
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		renderer = GetComponent<Renderer> ();
		mainCamera = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {
		if (insanity == 1 && screenLapCounter < 6  && !offInsanity) {
			ScreenWrap ();

		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Destroy (gameObject);
	}

	public void turnOffInsanity(){
		offInsanity = true;
	}

	void ScreenWrap()
	{
		bool isVisible = renderer.isVisible;

		if(isVisible)
		{
			isWrappingX = false;
			isWrappingY = false;
			return;
		}

		if(isWrappingX && isWrappingY) {
			return;
		}
		screenLapCounter++;
		Camera cam = Camera.main;
		var viewportPosition = cam.WorldToViewportPoint(transform.position);
		var newPosition = transform.position;

		if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
		{
			newPosition.x = -newPosition.x;

			isWrappingX = true;
		}

		if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
		{
			newPosition.y = -newPosition.y;

			isWrappingY = true;
		}

		transform.position = newPosition;
	}


}
