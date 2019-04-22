using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	private Vector3 bugePos;
	private GameObject buge;
	public float xDis = -0.15f;
	public float yDis = 0.15f;
	public float zDis = -0.3f;

	// Use this for initialization
	void Start () {
		buge = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		bugePos = buge.transform.position;
		gameObject.transform.position = new Vector3 (bugePos.x + xDis, bugePos.y + yDis, bugePos.z + zDis);
	}
}
