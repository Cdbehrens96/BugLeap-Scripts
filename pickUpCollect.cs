using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpCollect : MonoBehaviour {

	private Animator anim;
	public GameObject player;
	private bool entered;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player"))
			StartCoroutine (Drop ());
	}

	IEnumerator Drop(){
		yield return new WaitForSeconds (0.1f);
		entered = true;
		anim.SetBool ("entered", true);
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
	}
}
