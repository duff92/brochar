using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHatch : MonoBehaviour {
	public GameObject[] fragments;
	public bool sequential;
	public bool hatchOpen = false;
	private int counter = 0;

	public Transform Yuleballs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Began");
                RaycastHit hit;
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.transform.gameObject.tag == "crystalExploding")
                    {
                        if (!sequential)
                        {
                            foreach (GameObject fragment in fragments)
                            {
                                Rigidbody rb = fragment.GetComponent<Rigidbody>();
                                rb.isKinematic = false;
                                Vector3 direction = new Vector3(Random.value - 0.5f, Random.value, Random.value - 0.5f) * 100;
                                Vector3 rotation = new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 2000;
                                rb.AddForce(direction);
                                rb.AddTorque(rotation);

                                hatchOpen = true;

                            }
                        }
                        if (sequential)
                        {
                            GameObject fragment = fragments[counter];
                            Rigidbody rb = fragment.GetComponent<Rigidbody>();
                            rb.isKinematic = false;
                            Vector3 direction = new Vector3(Random.value - 0.5f, Random.value, Random.value - 0.5f) * 200;
                            Vector3 rotation = new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 2000;
                            rb.AddForce(direction);
                            rb.AddTorque(rotation);
                            counter++;
                        }
                    }
                }
            }
		    if (hatchOpen) {

			    for (int i = 0; i < Yuleballs.childCount; i++) {
				    Yuleballs.GetChild (i).gameObject.GetComponent<Rigidbody> ().useGravity = true;
			    }
		    }
        }
	}
}
