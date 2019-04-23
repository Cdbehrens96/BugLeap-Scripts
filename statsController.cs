using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class statsController : MonoBehaviour
{
    public static int levelTime;
    //public static int levelNum;

    private GameObject cleared, endTime, fallCount;

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode){
        DontDestroyOnLoad (transform.gameObject);
    }

    public static void GoalHit(float currentLevelTime, int levelNum){
        
		GameObject cleared = GameObject.FindGameObjectWithTag ("cleared");
		GameObject endTime = GameObject.FindGameObjectWithTag ("endTime");
		GameObject fallCount = GameObject.FindGameObjectWithTag ("fallCount");

        Scene scene = SceneManager.GetActiveScene();

        int minutes = Mathf.FloorToInt(currentLevelTime / 60F);
    	int seconds = Mathf.FloorToInt(currentLevelTime - minutes * 60);

        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        //string[] levelFalls;
        
        //levelTime[1] = "test"; //currentLevelTime.ToString()
        Debug.Log("Time: " + niceTime + " / Level ID : " + levelNum);
        
        endTime.GetComponent<Text>().text = niceTime;
		cleared.GetComponent<Text>().text = scene.name + " Cleared";
		fallCount.GetComponent<Text>().text = playerMovement.deathCount.ToString();

        //StatsHold(levelNum, fallCount.GetComponent<Text>().text);
        
        if(levelNum == 1){
            PlayerPrefs.SetInt("level1falls", playerMovement.deathCount);
            PlayerPrefs.SetString("level2time", niceTime);
        }

        if(levelNum == 2){
            PlayerPrefs.SetInt("level2falls", playerMovement.deathCount);
            PlayerPrefs.SetInt("level2mins", minutes);
            PlayerPrefs.SetString("level2time", niceTime);
        }

        if(levelNum == 3){
            PlayerPrefs.SetInt("level3falls", playerMovement.deathCount);
            PlayerPrefs.SetInt("level3mins", minutes);
            PlayerPrefs.SetString("level3time", niceTime);
        }

        playerMovement.deathCount = 0;
        pickUpGoal.timer = 0;
        levelTime = minutes;
    }

    /*public static void StatsHold(int levelNum, string fallCount){
        levelFalls[levelNum] = fallCount;
    }*/

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

}

/*

Carmine:

var scores = new List<scoreHolder>();
var score = new scoreHolder();
score.time = "123123";
score.collected = 5;

scores.Add(score);

var json = JsonUtility.ToJson(scores);
PlayerPrefs.SetString("scores", json);


scores = JsonUtility.FromJson<List<scoreHolder>>(PlayerPrefs.GetString("scores"));


 */