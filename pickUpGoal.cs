using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pickUpGoal : MonoBehaviour {

	private Animator anim;
	public GameObject player;
	private bool entered;
	public int goal;
	public int collectCounter;
	private float alpha;
	public GameObject goalObj;
	private string sceneName;
	private Scene currentScene;
	public string nextLevel;

	private GameObject endBox;
	private bool canProgress;

	public static float timer;
	private string niceTime;

	public int levelNum;
	public static int levelID;

	private BGMusic BGM;

	void Start () {
		Time.timeScale = 1f;
		goal = GameObject.FindGameObjectsWithTag ("collectable").Length;
		anim = gameObject.GetComponent<Animator> ();
		alpha = 0.2f;
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;

		endBox = GameObject.FindGameObjectWithTag ("endBox");

	}

	void Update () {
		collectCounter = playerMovement.collectCount;
		goalObj.GetComponent<Renderer> ().material.color = new Color (1, 1, 1, alpha);
		if (collectCounter == goal){
			alpha = 1;
		}
		
        timer += Time.deltaTime;

		if (entered == true){
       		StartCoroutine(ScaleTime(1.0f, 0.0f, 1.0f));
			//to reverse it: StartCoroutine(ScaleTime(0.0f, 1.0f, 1.0f));

			//Non-LERP slow-mo
			/*timePct = new Vector3(Mathf.Lerp(1, 0, t), 0, 0);
			timePct-= Time.deltaTime;
			if (timePct < 0.0001f){
				timePct = 0;
			}*/
		}

		if (canProgress == true) {
			Debug.Log("2ay");
			if (Input.GetButtonDown ("Jump")){
				Debug.Log("3ay");
				SceneManager.LoadScene (nextLevel);
				BGM = FindObjectOfType<BGMusic>();
				BGM.ChangeMusic();
			}

			if (Input.GetButtonDown ("Jump")){
				Debug.Log("takeHOME");
				SceneManager.LoadScene ("lab");
				BGM = FindObjectOfType<BGMusic>();
				BGM.ChangeMusic();
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player") && collectCounter >= goal){
			entered = true;
			statsController.GoalHit(timer, levelNum);
			StartCoroutine (Drop ());
		}
	}

	IEnumerator Drop(){
		yield return new WaitForSeconds (0.1f);
		anim.SetBool ("entered", true);
		yield return new WaitForSeconds (0.2f);
		//SceneManager.LoadScene (nextLevel);
		Animator boxAnim;
		boxAnim = endBox.GetComponent<Animator>();
		boxAnim.SetBool ("entered", true);
		canProgress = true;
		Debug.Log("1ay");
	}

	IEnumerator ScaleTime(float start, float end, float time){
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;
		time = 2;
          
        while (timer < time){
            Time.timeScale = Mathf.Lerp (start, end, timer / time);
			float shininess = Mathf.Lerp(start, 0, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }
        
        Time.timeScale = end;
    }
}
