using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasMove : MonoBehaviour {

	private Vector3 bugePos;
	private GameObject buge;
	public float canvasHeight;

	// Use this for initialization
	void Start () {
		buge = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		bugePos = buge.transform.position;
		gameObject.transform.position = new Vector3 (bugePos.x, bugePos.y + canvasHeight, bugePos.z);
	}
}
