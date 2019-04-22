using UnityEngine;
using System.Collections;

public class backAndForth : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;

	public float percent = 0;

	public float numberOfSeconds;
	public float direction =-1;
	public int colorID; //r0, b1, y2, g3, o4, p5

	// Update is called once per frame
	void Update () {
		if((colorID == 0 && playerMovement.redOn && playerMovement.yelOn == false && playerMovement.yelOn == false)||
		(colorID == 1 && playerMovement.blueOn && playerMovement.redOn == false && playerMovement.yelOn == false)||
		(colorID == 2 && playerMovement.yelOn && playerMovement.blueOn == false && playerMovement.redOn == false)||
		(colorID == 3 && playerMovement.yelOn && playerMovement.blueOn && playerMovement.redOn == false)||
		(colorID == 4 && playerMovement.yelOn && playerMovement.redOn && playerMovement.blueOn == false)||
		(colorID == 5 && playerMovement.blueOn && playerMovement.redOn && playerMovement.yelOn == false)){
			///set position for the cube as an interpolation
			transform.position = Vector3.Lerp(pointA.position, pointB.position, percent);
			transform.rotation = Quaternion.Slerp (pointA.rotation, pointB.rotation, percent);

			//percent = percent + Time.deltaTime /numberOfSeconds;
			//above moves it one direction

			percent+=Time.deltaTime / numberOfSeconds * direction;
			
			if (percent > 1) {
				direction= -1;
			}
			if (percent < 0) {
				direction = 1;
			}
		} else {
			transform.position = transform.position;
		}

		//GetComponent<Renderer>().material.color = Color.Lerp (Color.red, Color.blue, percent);
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.CompareTag ("Player")) {
			other.collider.transform.SetParent(transform);
		}
	}

	void OnCollisionExit (Collision other){
		if (other.gameObject.CompareTag ("Player")) {
			other.collider.transform.SetParent(null);
		}
	}
}
