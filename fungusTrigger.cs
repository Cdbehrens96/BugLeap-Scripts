using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class fungusTrigger : MonoBehaviour {

	public Flowchart flowchart;
	public GameObject dialog;
	public static bool hit;
	public GameObject looker;
	private int falls;
	//public GameObject lookLight;

	void Update () {
		hit = flowchart.GetBooleanVariable("hit");
		falls = PlayerPrefs.GetInt("level1falls");

		flowchart.SetIntegerVariable("falls", falls);
		Debug.Log("hit = " + hit);

		if (Input.GetButtonDown("Cancel")){
			Debug.Log("cancel1");
			if (hit == true){
				Debug.Log("cancel2");
				dialog.SetActive(false);
				hit = false;
				flowchart.SetBooleanVariable("hit", false);
			}
		}
	}

	void OnCollisionEnter(UnityEngine.Collision other){

		if (other.gameObject.CompareTag ("intercom")) {
			hit = true;
			Debug.Log("kwoongus");
			StartCoroutine (Toggle());
			//flowchart.SetBooleanVariable ("hit", true);
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.CompareTag ("wait") && !hit) {
			looker.GetComponent<Animator>().SetBool("look", true);
			StartCoroutine (SetOff());
			//lookLight.SetActive(true);
		}
	}

	IEnumerator Toggle() {
		yield return new WaitForSeconds (0.3f);
		dialog.SetActive(true);
	}

	IEnumerator SetOff() {
		yield return new WaitForSeconds (0.25f);
		looker.GetComponent<Animator>().SetBool("look", false);
	}
}
