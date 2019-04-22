using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMusic : MonoBehaviour {

	private GameObject[] musicObj;
	public AudioClip easy, medium, hard;

	private Scene currentScene;

	private float muteCount;
	private Camera mainCamera;
	private float vol;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);

	}

	void Start (){
		musicObj = GameObject.FindGameObjectsWithTag ("music");
		if(musicObj != null){
			Destroy (musicObj [1]);
		}
		currentScene = SceneManager.GetActiveScene ();
		print ("sceneName= " + SceneManager.GetActiveScene().name);
		mainCamera = Camera.main;

		/*if (SceneManager.GetActiveScene().name == "level1" || SceneManager.GetActiveScene().name == "level2")
			source.clip = easy;

		if (SceneManager.GetActiveScene().name == "level3" || SceneManager.GetActiveScene().name == "level4")
			source.clip = medium;

		if (SceneManager.GetActiveScene().name == "level5" || SceneManager.GetActiveScene().name == "level6")
			//source.clip = hard;*/

		//AudioSource.PlayClipAtPoint (medium, mainCamera.transform.position, vol);
	}

	void Update () {

		/*if (Input.GetButtonDown ("Mute"))
			muteCount++;

		if (muteCount > 1)
			muteCount = 0;

		if (muteCount == 1) {
			vol = 0;
		} else {
			vol = 1;
		}*/
	}
}
