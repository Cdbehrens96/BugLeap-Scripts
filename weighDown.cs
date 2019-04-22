using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weighDown : MonoBehaviour {

	public bool weigh, lift = false;
	private float weight = 0;
	public float weightMin, weightMax;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//weight = Mathf.Clamp (weight, weightMin, weightMax);
		gameObject.transform.position = new Vector3 (transform.position.x, (transform.position.y + weight), transform.position.z);

		if (weigh == true)
			weight = weight - 0.0002f;

		if (lift == true)
			weight = weight + 0.0002f;

		if (gameObject.transform.position.y <= weightMin) {
			weight = 0;
			weigh = false;
		}

		if (gameObject.transform.position.y >= weightMax) {
			weight = 0;
			lift = false;
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player") && gameObject.transform.position.y > weightMin) {
			weight = weight - 0.01f;
			weigh = true;
			lift = false;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.CompareTag ("Player") && gameObject.transform.position.y < weightMax) {
			weight = weight + 0.01f;
			weigh = false;
			lift = true;
		}
	}
}
