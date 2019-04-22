using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {

	private Rigidbody rb;

	private float xSpeed, currentSpeed;
	public float speed = 1.0f;
	public float jumpForce;
	private int colorCount, rCount, bCount, yCount;

	public static bool blueOn, redOn, yelOn = false;
	private bool canCollect;
	public static int collectCount = 0;

	private float r, g, b, alpha;
	private Renderer rend;

	public static bool grounded = true;
	public bool onGround, onButton, onRock, onColor;
	private GameObject deathScreen;
	public float minY = -4f;
	private bool dead = false;

	public PhysicMaterial noFriction;
	public PhysicMaterial normalFriction;
	private Collider col;

	private GameObject colorCountDisplay;
	Text colorCountText;
	private GameObject collectDisplay;
	Text collectText;
	public int collectTotal;

	public static int deathCount;

	//---------Audio
	public AudioClip[] jump;
	public AudioClip[] land;
	public AudioClip[] glide;
	public AudioClip[] bump;
	public AudioClip[] rockGlide;
	public AudioClip[] coinGet;

	public float minVol, maxVol;

	private float tipAmt;
	private bool tipping;

	private Camera mainCamera;

	private float muteCount;

	private Animator anim;
	private GameObject bugeChild;

	private GameObject coin;
	private Animator coinAnim;
	public static int sitCoin;

	void Start () {
		r = 0.5f; b = 0.5f; g = 0.5f; alpha = 1f;
		collectCount = 0;
		rb = GetComponent<Rigidbody> ();
		Cursor.visible = false;
		col = GetComponent<Collider>();

		colorCountDisplay = GameObject.FindGameObjectWithTag ("textOnBuge");
		colorCountText = colorCountDisplay.GetComponent<Text> ();

		collectDisplay = GameObject.FindGameObjectWithTag ("textOnGoal");
		collectText = collectDisplay.GetComponent<Text> ();
		collectTotal = GameObject.FindGameObjectsWithTag ("collectable").Length;

		currentSpeed = Mathf.Abs (xSpeed);
		redOn = false; blueOn = false; yelOn = false;
		mainCamera = Camera.main;

		bugeChild = GameObject.FindGameObjectWithTag ("bugeChild");
		anim = bugeChild.GetComponent<Animator>();
		canCollect = true;

		coin = GameObject.FindGameObjectWithTag ("coinHolder");
		
		deathScreen = GameObject.FindGameObjectWithTag ("deathScreen");
		deathScreen.SetActive (false);
	}

	void Update () {
		if ((transform.eulerAngles.x > 88.7f && transform.eulerAngles.x < 88.8f) || (transform.eulerAngles.x > 271.2f && transform.eulerAngles.x < 271.3f)) {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x + tipAmt, transform.eulerAngles.y, transform.eulerAngles.z);
			print ("ready");
			tipAmt= tipAmt - 10;
		} else {
			transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			tipAmt=0;
		}

		if (fungusTrigger.hit == false){
			xSpeed = Input.GetAxis ("Horizontal") * speed;
		} else {
			xSpeed = 0;
		}
		rb.velocity = new Vector3 (xSpeed, rb.velocity.y, rb.velocity.z);
		bugeChild.GetComponent<Renderer> ().material.color = new Color (r, g, b, alpha);

		if (Input.GetButtonDown ("Jump") && grounded && fungusTrigger.hit == false) {
			AudioSource.PlayClipAtPoint (jump [Random.Range (0, jump.Length)], mainCamera.transform.position, maxVol);
			rb.velocity = new Vector3 (rb.velocity.x, jumpForce, rb.velocity.z);
		}

		if (Input.GetButtonDown ("Jump") && dead) {
			deathCount++;
			Debug.Log("falls = " + deathCount);
			Destroy (gameObject);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		if (gameObject.transform.position.y < minY) {
			deathScreen.SetActive (true);
			dead = true;
		}

		float fillVal = 1.0f;
		float lessFill = 0.2f;
		if (redOn) {
			
			if (!blueOn && !yelOn) {
				r = fillVal;
				g = lessFill;
				b = lessFill;
			}
			if (blueOn && !yelOn) {
				r = fillVal;
				g = lessFill;
				b = fillVal;
			}
			if (yelOn && !blueOn) {
				r = fillVal;
				g = (fillVal/2);
				b = 0;
			}
			if (yelOn && blueOn) {
				r = 0.5f;
				g = 0.5f;
				b = 0.5f;
				redOn = false; blueOn = false; yelOn = false;
				colorCount = 0; rCount = 0; bCount = 0; yCount = 0;
			}

		}

		if (blueOn) {
			
			if (!redOn && !yelOn) {
				r = lessFill;
				g = lessFill;
				b = fillVal;
			}
			if (redOn && !yelOn) {
				r = fillVal;
				g = lessFill;
				b = fillVal;
			}
			if (yelOn && !redOn) {
				r = lessFill;
				g = fillVal;
				b = lessFill;
			}
			if (redOn && yelOn) {
				r = 0.5f;
				g = 0.5f;
				b = 0.5f;
				redOn = false; blueOn = false; yelOn = false;
				colorCount = 0; rCount = 0; bCount = 0; yCount = 0;
			}

		}

		if (yelOn) {

			if (!redOn && !blueOn) {
				r = fillVal;
				g = fillVal;
				b = lessFill;
			}
			if (redOn && !blueOn) {
				r = fillVal;
				g = (fillVal/2);
				b = 0;
			}
			if (blueOn && !redOn) {
				r = lessFill;
				g = fillVal;
				b = lessFill;
			}
			if ((redOn && blueOn)) {
				r = 0.5f;
				g = 0.5f;
				b = 0.5f;
				redOn = false; blueOn = false; yelOn = false;
				colorCount = 0; rCount = 0; bCount = 0; yCount = 0;
			}

		}

		if (colorCount > 3) {
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
			redOn = false; blueOn = false; yelOn = false;
			colorCount = 0; rCount = 0; bCount = 0; yCount = 0;
		}

		colorCountText.text = colorCount + "/2".ToString ();
		collectText.text = collectCount + "/" + collectTotal.ToString ();
		colorCountText.color = bugeChild.GetComponent<Renderer> ().material.color;
		
		if(currentSpeed > 0.9f && currentSpeed < 1f && grounded)
			AudioSource.PlayClipAtPoint (glide [Random.Range (0, glide.Length)], mainCamera.transform.position, Random.Range (minVol, maxVol));

		if (onGround || onButton || onRock || onColor){
			grounded = true;
		} else {
			grounded = false;
		}

		if (Input.GetButtonDown ("Restart")){
			SceneManager.LoadScene ("Level 1");
			pickUpGoal.timer = 0;
			playerMovement.deathCount = 0;
			deathScreen.SetActive (false);
		}

		/*if (Input.GetButtonDown ("QuickFix")){
			if (SceneManager.GetActiveScene().name == "Level 1")
				SceneManager.LoadScene ("Level 1");			
		}*/

		if (Input.GetButtonDown ("Mute"))
			muteCount++;

		if (muteCount > 1)
			muteCount = 0;

		if (muteCount == 1) {
			minVol = 0; maxVol = 0;
		} else {
			minVol = 0.1f; maxVol = 0.2f;
		}

		anim.SetFloat("Speed", Mathf.Abs(xSpeed));
		//Debug.Log("canCollect = " + canCollect);
		Debug.Log("Grounded = " + grounded);
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("blueC") && canCollect) {
			if ((colorCount < 2) || (yCount >= 2) || (rCount >= 2)) {
				canCollect = false;
				StartCoroutine (CollectDisable());
				blueOn = true;

				if (bCount < 1) {
					colorCount++;
					bCount++;
				}
			} else {
				colorCount = 0;
				rCount = 0; bCount = 0; yCount = 0;
			}
			print ("Entered: redOn = " + redOn + ", blueOn = " + blueOn + ", yelOn = " + yelOn + ", colorCount = " + colorCount);
			print ("Entered: rCount = " + rCount + ", bCount = " + bCount + ", yCount = " + yCount + ", colorCount = " + colorCount);
		}

		if (other.CompareTag ("redC") && canCollect) {
			if ((colorCount < 2) || (yCount >= 2) || (bCount >= 2)) {
				canCollect = false;
				StartCoroutine (CollectDisable());
				redOn = true;

				if (rCount < 1) {
					colorCount++;
					rCount++;
				}
			} else {
				colorCount = 0;
				rCount = 0; bCount = 0; yCount = 0;
			}
			print ("Entered: redOn = " + redOn + ", blueOn = " + blueOn + ", yelOn = " + yelOn + ", colorCount = " + colorCount);
			print ("Entered: rCount = " + rCount + ", bCount = " + bCount + ", yCount = " + yCount + ", colorCount = " + colorCount);
		}

		if (other.CompareTag ("yelC") && canCollect) {
			if ((colorCount < 2) || (bCount >= 2) || (rCount >= 2)) {
				canCollect = true;
				StartCoroutine (CollectDisable());
				yelOn = true;

				if (yCount < 1) {
					colorCount++;
					yCount++;
				}
			} else {
				colorCount = 0;
				rCount = 0; bCount = 0; yCount = 0;
			}
			print ("Entered: redOn = " + redOn + ", blueOn = " + blueOn + ", yelOn = " + yelOn + ", colorCount = " + colorCount);
			print ("Entered: rCount = " + rCount + ", bCount = " + bCount + ", yCount = " + yCount + ", colorCount = " + colorCount);
		}

		if (colorCount == 0) {
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
			redOn = false; blueOn = false; yelOn = false;
		}

		if (other.CompareTag ("ground"))
			onGround = true;
		if (other.CompareTag ("rock"))
			onRock = true;
		if (other.CompareTag ("button"))
			onButton = true;
		if (other.CompareTag ("colorGround"))
			onColor = true;

		if (other.CompareTag ("collectable") && canCollect) {
			canCollect = false;
			StartCoroutine (CollectDisable());
			collectCount++;
			//print (collectCount);
		}

		if (other.CompareTag ("coin") && canCollect){
			coinAnim = coin.GetComponent<Animator>();
			//coinAnim.SetBool("hit", true);
			sitCoin+=10;
			Destroy(other.gameObject);
			canCollect = false;
			StartCoroutine (coinCollectDisable());
			Debug.Log("sitCoin = " + sitCoin);
		}
	}

	void OnTriggerExit (Collider other) {

		if (other.CompareTag ("ground"))
			onGround = false;
		if (other.CompareTag ("rock"))
			onRock = false;
		if (other.CompareTag ("button"))
			onButton = false;
		if (other.CompareTag ("colorGround")){
			onColor = false;
		}

	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.CompareTag ("ground")) {
			if (!grounded) {
				AudioSource.PlayClipAtPoint (land [Random.Range (0, 3)], mainCamera.transform.position, Random.Range (minVol, maxVol));
				Debug.Log ("hey");
				col.material = noFriction;
				other.gameObject.GetComponent<Collider> ().material = noFriction;
			} else {
				other.gameObject.GetComponent<Collider> ().material = normalFriction;
				//AudioSource.PlayClipAtPoint (land [Random.Range (0, land.Length)], transform.position, Random.Range (0.7f, 1f));
			}
		} else {
			AudioSource.PlayClipAtPoint (land [Random.Range (0, 3)], mainCamera.transform.position, Random.Range (minVol, maxVol));
		}
		anim.SetBool("flinch", true);
	}

	void OnCollisionStay(Collision other){
		if (other.gameObject.CompareTag ("rock") && currentSpeed > 0.9f && currentSpeed < 1f) {
			AudioSource.PlayClipAtPoint (rockGlide [Random.Range (0, rockGlide.Length)], mainCamera.transform.position, Random.Range (minVol, maxVol));
		}

		if (other.gameObject.CompareTag ("ground")/*|| other.gameObject.CompareTag ("colorGround")*/){
			//onGround = true;
		}
	}

	IEnumerator CollectDisable(){
		yield return new WaitForSeconds (0.5f);
		canCollect = true;
	}

	IEnumerator coinCollectDisable(){
		yield return new WaitForSeconds (0.1f);
		canCollect = true;
	}
}