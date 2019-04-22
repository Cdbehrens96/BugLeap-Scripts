using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPress : MonoBehaviour {

	private Animator anim;
	private Renderer rend;

	public static bool pressed;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		//otherGround.GetComponent<Renderer> ().material.color = new Color (r, g, b, alpha);
	}

	void OnCollisionStay (Collision other) {
		pressed = true;
		anim.SetBool ("Pressed", true);
	}

	void OnCollisionExit (Collision other) {
		pressed = false;
		anim.SetBool ("Pressed", false);
	}
}
