using UnityEngine;
using System.Collections;
using DG.Tweening;

public class backAndForthTween : MonoBehaviour {

	public Transform pointA;
	public Transform pointB;

	public float percent = 0;

	public float numberOfSeconds;
	public float direction =-1;
	public int colorID; //r0, b1, y2, g3, o4, p5
	public PathType pathType = PathType.CatmullRom;
	public Vector3[] waypoints = new[] {
		new Vector3(0, 0, 0),
		new Vector3(0, 0, 0)
	};

	// Update is called once per frame
	void Update () {
		if((colorID == 0 && playerMovement.redOn && playerMovement.yelOn == false && playerMovement.yelOn == false)||
		(colorID == 1 && playerMovement.blueOn && playerMovement.redOn == false && playerMovement.yelOn == false)||
		(colorID == 2 && playerMovement.yelOn && playerMovement.blueOn == false && playerMovement.redOn == false)||
		(colorID == 3 && playerMovement.yelOn && playerMovement.blueOn && playerMovement.redOn == false)||
		(colorID == 4 && playerMovement.yelOn && playerMovement.redOn && playerMovement.blueOn == false)||
		(colorID == 5 && playerMovement.blueOn && playerMovement.redOn && playerMovement.yelOn == false)){
			///set position for the cube as an interpolation
			//transform.position = Vector3.Lerp(pointA.position, pointB.position, percent);
			//transform.rotation = Quaternion.Slerp (pointA.rotation, pointB.rotation, percent);
			Move();
			//percent = percent + Time.deltaTime /numberOfSeconds;
			//above moves it one direction

			/*percent+=Time.deltaTime / numberOfSeconds * direction;
			
			if (percent > 1) {
				direction= -1;
			}
			if (percent < 0) {
				direction = 1;
			}*/
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

	void Move (){
		Tween t = gameObject.transform.DOPath(waypoints, 3, pathType)
		.SetOptions(true)
		.SetLookAt(0.001f);
		// Then set the ease to Linear and use infinite loops
		t.SetEase(Ease.Linear).SetLoops(-1);

	}
}
