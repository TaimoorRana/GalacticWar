using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {


	[SerializeField] Transform leftBulletPosition, rightBulletPosition,centerBulletPosition,angleLeftBulletPosition,angleRightBulletPosition;
	[SerializeField] Object bullet;
	[SerializeField] float speed = 0.3f;
	Animator anim;
	float screenRatio;
	float cameraWidth;
	public float playerSizeMargin = 1.6f;
	EnemyFormation enemyFormation;
	bool weaponUpgradPerformed = false;
	public int weaponLevel = 1;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		speed = 0.3f;
		screenRatio = (float)Screen.width /(float)Screen.height;
		cameraWidth = Camera.main.orthographicSize * screenRatio;

	}

	// Update is called once per frame
	void Update () {
		AnimationController ();
		PlayerInputs ();
		if (enemyFormation == null) {
			enemyFormation = GameObject.FindObjectOfType<EnemyFormation> ();
		}


		

	}

	private void AnimationController(){
		if (Input.GetKey (KeyCode.A)) {
			anim.SetBool ("Turn Right", false);
			anim.SetBool ("Turn Left", true);
			anim.SetBool ("Left To Center", false);
			anim.SetBool ("Right To Center", true);
		} else if(Input.GetKeyUp (KeyCode.A)){
			anim.SetBool ("Left To Center", true);

		} else if (Input.GetKey (KeyCode.D)) {
			anim.SetBool ("Turn Left", false);
			anim.SetBool ("Turn Right", true);
			anim.SetBool ("Right To Center", false);
			anim.SetBool ("Left To Center", true);
		}else if(Input.GetKeyUp (KeyCode.D)){
			anim.SetBool ("Right To Center", true);

		} else if (Input.GetKeyDown (KeyCode.C)) {
			anim.SetBool ("Return to Center", !anim.GetBool ("Return to Center"));
		} else {
			anim.SetBool ("Turn Left", false);
			anim.SetBool ("Turn Right", false);
			anim.SetBool ("Left To Center", true);
			anim.SetBool ("Right To Center", true);
		}
	}

	void PlayerInputs (){
		// Shooting

		if (Input.GetKeyDown (KeyCode.Space)) {
			switch (weaponLevel) {
			case(1):
				GameObject centerBullet = GameObject.Instantiate (bullet) as GameObject;
				centerBullet.transform.position = centerBulletPosition.position;
				centerBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f, 0.2f));
				break;
			case(2):
				GameObject leftBullet = GameObject.Instantiate (bullet) as GameObject;
				GameObject rightBullet = GameObject.Instantiate (bullet) as GameObject;

				leftBullet.transform.position = leftBulletPosition.position;
				rightBullet.transform.position = rightBulletPosition.position;
				leftBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f, 0.2f));
				rightBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 0.2f));
				break;

			case(3):
				leftBullet = GameObject.Instantiate (bullet) as GameObject;
				rightBullet = GameObject.Instantiate (bullet) as GameObject;
				centerBullet = GameObject.Instantiate (bullet) as GameObject;

				leftBullet.transform.position = angleLeftBulletPosition.position;
				leftBullet.transform.rotation = angleLeftBulletPosition.rotation;
				rightBullet.transform.position = angleRightBulletPosition.position;
				rightBullet.transform.rotation = angleRightBulletPosition.rotation;
				leftBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-0.1f, 0.2f));
				rightBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0.1f, 0.2f));
				centerBullet.transform.position = centerBulletPosition.position;
				centerBullet.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0f, 0.2f));
				break;

			}




		}

		// Movements
		if (Input.GetKey (KeyCode.W)) {
			transform.position += new Vector3 (0, speed, 0);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position += new Vector3 (0, -speed, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += new Vector3 (-speed, 0, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += new Vector3 (speed, 0, 0);
		}

		if (transform.position.y <= -Camera.main.orthographicSize + playerSizeMargin) {
			transform.position = new Vector3(transform.position.x,-Camera.main.orthographicSize + playerSizeMargin,transform.position.z);
		}


		
		if (transform.position.x <= -cameraWidth + playerSizeMargin) {
			transform.position = new Vector3(-cameraWidth + playerSizeMargin,transform.position.y,transform.position.z);
		}

		if (transform.position.x > cameraWidth - playerSizeMargin) {
			transform.position = new Vector3(cameraWidth - playerSizeMargin,transform.position.y,transform.position.z);
		}
	}

	public void upgradeWeapon(){
		
		if (weaponLevel < 3 && !weaponUpgradPerformed) {
			weaponLevel++;
			weaponUpgradPerformed = true;
			Debug.Log ("level upgrade " + weaponLevel);
		}

	}

	public void DownGradeWeapon(){

		if (weaponLevel > 0) {
			weaponLevel--;
			GameObject.FindObjectOfType<PlayerHealthManager> ().removeHealth ();
			Debug.Log ("level downgrade " + weaponLevel);
		}

	}

	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log ("Power up Collision");
		if (coll.gameObject.tag == "PowerUp") {
			Debug.Log ("Power up Collision");
			Destroy (coll.gameObject);
			weaponUpgradPerformed = false;
			upgradeWeapon ();
			GameObject.FindObjectOfType<PlayerHealthManager> ().addHealth ();
		}else if (coll.gameObject.tag == "Bullet"){
			if (weaponLevel > 1) {
				DownGradeWeapon ();
			} else {
				DownGradeWeapon ();
				gameObject.SetActive (false);
				Application.LoadLevel("MainMenu");
			}
		}
	}


}
