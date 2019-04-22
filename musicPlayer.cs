using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicPlayer : MonoBehaviour {

	public AudioClip easy;
	public AudioClip medium;
	public AudioClip hard;
	private AudioSource source;

	private string sceneName;
	private Scene currentScene;

	void Awake (){
		source = gameObject.GetComponent<AudioSource> ();
		currentScene = SceneManager.GetActiveScene ();
		sceneName = currentScene.name;
		if (sceneName == "level1")
			source.PlayOneShot (easy, 1);
		if (sceneName == "level2")
			source.PlayOneShot (easy, 1);
		if (sceneName == "level3")
			source.PlayOneShot (medium, 1);
		if (sceneName == "level4")
			source.PlayOneShot (medium, 1);
		if (sceneName == "level5")
			source.PlayOneShot (hard, 1);
		if (sceneName == "level6")
			source.PlayOneShot (easy, 1);
		//DontDestroyOnLoad (gameObject);
	}

}
