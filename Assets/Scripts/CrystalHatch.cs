using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHatch : MonoBehaviour {
	public GameObject[] fragments;
	public bool sequential;
	private int counter = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0) && !sequential) {
			foreach (GameObject fragment in fragments) {
				Rigidbody rb = fragment.GetComponent<Rigidbody> ();
				rb.isKinematic = false;
				Vector3 direction = new Vector3 (Random.value-0.5f, Random.value, Random.value-0.5f)*100;
				Vector3 rotation = new Vector3 (Random.value-0.5f, Random.value-0.5f, Random.value-0.5f)*2000;
				rb.AddForce(direction);
				rb.AddTorque(rotation);
			}
		}
		if (Input.GetMouseButtonUp (0) && sequential) {
			GameObject fragment = fragments [counter];
			Rigidbody rb = fragment.GetComponent<Rigidbody> ();
			rb.isKinematic = false;
			Vector3 direction = new Vector3 (Random.value-0.5f, Random.value, Random.value-0.5f)*200;
			Vector3 rotation = new Vector3 (Random.value-0.5f, Random.value-0.5f, Random.value-0.5f)*2000;
			rb.AddForce(direction);
			rb.AddTorque(rotation);
			counter++;
		}
	}
}
