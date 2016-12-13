using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YuleBallCtrl : MonoBehaviour {

	public CrystalHatch Hatch;
	float timer = Random.Range(0.5f, 5f);
	Vector3 startPosition;

	// Use this for initialization
	void Start () {

		startPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (Hatch.hatchOpen) {

			timer -= Time.deltaTime;

			if (timer <= 0f) {
				transform.position = startPosition;
				GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);
				timer = Random.Range (1f, 5f);
			
			}
			
		}
		
	}
}
