using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBounce : MonoBehaviour {

	private Rigidbody rb;
	private Collider col;
	private float xRad;
	public bool grounded = true;
	public LayerMask whatIsGround;
	public PhysicMaterial noFriction;
	public PhysicMaterial normalFriction;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		col = GetComponent<Collider>();
		xRad = col.bounds.extents.x;
	}


	void Update () {
		float xSpeed = Input.GetAxis ("Horizontal") * 1.5f;
		rb.velocity = new Vector3 (xSpeed, rb.velocity.y, 0);

		if(Input.GetButtonDown("Jump") && grounded){
			rb.AddForce(new Vector3(0f,400f,0f));
		}

		RaycastHit hit;
		grounded = Physics.SphereCast(transform.position, xRad, -Vector3.up, out hit, 1, whatIsGround);
		if (grounded) {
			//print (hit.collider.name);
			//print (hit.collider.tag);
			print(col.material.name);
			if (col.material.name != "normalFriction") {
				col.material = normalFriction;
				hit.collider.gameObject.GetComponent<Collider> ().material = normalFriction;
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("Wall")){
			if (!grounded) {
				col.material = noFriction;
				other.gameObject.GetComponent<Collider>().material = noFriction;
			}
		}
	}
}
