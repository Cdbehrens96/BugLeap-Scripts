using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour {

	private bool sceneChnge = false;
	private string homeScene = "";
	private float fadeSpeed = 0.5f;
	private AudioSource bgm;

	private GameObject[] musicObj;

	public AudioClip lab, intro, puzzle;

    private static BGMusic _instance;

    public static BGMusic Instance{
        get { return _instance; }
    }

	// Use this for initialization

    private void Awake (){
        if(_instance == null){
            _instance = this;
        }
    }

	void OnEnable(){
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {

		Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

		DontDestroyOnLoad (transform.gameObject);

		bgm = this.GetComponent<AudioSource>();

		musicObj = GameObject.FindGameObjectsWithTag ("music");
		if(musicObj != null){
			Destroy (musicObj [1]);
		}

		homeScene = SceneManager.GetActiveScene ().name;

		//audio = GetComponent<AudioSource> ();
	}

    public void ChangeMusic(){
        Debug.Log("TUNE CHANGE");
        if (SceneManager.GetActiveScene().name == "lab")
			StartCoroutine(fadeDown(1f, 0f, 1f, 1));

        if (SceneManager.GetActiveScene().name == "Level 3")
			StartCoroutine(fadeDown(1f, 0f, 1f, 4));
    }

	IEnumerator fadeDown (float start, float end, float time, int levelID){
        float timer = 0.0f;
        bgm.volume = 1f;

        while (timer < time){
            bgm.volume = Mathf.Lerp(start, end, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }

        bgm.volume = end;
        if (levelID==1)
            StartCoroutine(fadeUp(intro, 0f, 1f, 4f));

        if (levelID==4){
            StartCoroutine(fadeUp(puzzle, 0f, 1f, 4f));
        }

        if (levelID==4){
            StartCoroutine(fadeUp(puzzle, 0f, 1f, 4f));
        }
    }

	IEnumerator fadeUp (AudioClip clip, float start, float end, float time){
        //yield return new WaitForSeconds(2f);
        float timer = 0.0f;
        Debug.Log("nowBack");

        bgm.Stop();
        bgm.clip = clip;
        bgm.Play();

        while (timer < time){
            bgm.volume = Mathf.Lerp(start, end, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }

        bgm.volume = end;
    }

	void OnDisable(){
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
