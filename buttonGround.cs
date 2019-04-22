using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonGround : MonoBehaviour {
	
	public float r, g, b;
	private float alpha;

	// Use this for initialization
	void Start () {
		alpha = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Renderer> ().material.color = new Color (r, g, b, alpha);
		if (buttonPress.pressed) {
			gameObject.GetComponent<BoxCollider> ().isTrigger = false;
			alpha = 1;
		} else {
			gameObject.GetComponent<BoxCollider> ().isTrigger = true;
			//make transparent
			alpha = 0.2f;
		}
	}
}
