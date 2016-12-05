using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCtrl : MonoBehaviour {

	void Update(){
		
	}

	void OnCollisionEnter(Collision collision) {

	}

	public void Reset(){
		transform.localPosition = new Vector3 (-0.15f, -0.13f, 0.48f);
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public void Throw(Vector3 target){
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 direction = target - transform.position;
		float force = direction.magnitude;
		direction = direction.normalized;
		direction.z = 0.5f;
		gameObject.GetComponent<Rigidbody> ().AddForce (direction * force * 100);
		// test
	}
}
