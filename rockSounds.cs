using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockSounds : MonoBehaviour {

	public AudioClip[] rockLand;

	void OnCollisionEnter (Collision other){ 
		if (other.gameObject.CompareTag ("ground"))
			AudioSource.PlayClipAtPoint (rockLand [Random.Range (0, rockLand.Length)], transform.position, Random.Range (0.7f, 1f));
	}
}
