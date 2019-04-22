using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactusPot : MonoBehaviour {

	public float r, g, b;
	private float alpha;

	private Color colorStart, colorEnd;

	private Renderer rend;

	void Start () {
		gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		alpha = 0.2f;
	}

	void Update () {
		gameObject.GetComponent<Renderer> ().material.color = new Color (r, g, b, alpha);
		if (playerMovement.yelOn && !playerMovement.redOn && !playerMovement.blueOn) {
			alpha = 1;
			gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		} else {
			alpha = 0.2f;
			gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		}
	}
}
