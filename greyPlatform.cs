using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greyPlatform : MonoBehaviour {
	
	public float r, g, b;
	private float alpha;

	private Color colorStart, colorEnd;

	private Renderer rend;

	void Start () {
		gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		alpha = 1;
	}

	void Update () {
		gameObject.GetComponent<Renderer> ().material.color = new Color (r, g, b, alpha);
		if (!playerMovement.redOn && !playerMovement.blueOn && !playerMovement.yelOn) {
			alpha = 1;
			gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		} else {
			alpha = 0.2f;
			gameObject.GetComponent<BoxCollider> ().isTrigger = true;
		}
	}
}
