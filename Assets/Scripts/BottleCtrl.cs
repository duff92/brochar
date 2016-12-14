﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BottleCtrl : MonoBehaviour {
	private AudioSource clonk;
	public GameObject target;
	public ImageTargetBehaviour trigger;
	public GameObject bottle;
    public OpenHatch2 hatch;
	private bool swiping;
	private Vector2 swipe;
	private bool thrown = false;
	private bool visible;
	private bool clonked = false;

	void Start(){
		clonk = gameObject.GetComponent<AudioSource> ();
	}

	void Update(){
		if ((trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED) != visible) {
			visible = (trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED);
            this.ActivateBottle(hatch.hatchOpen);
		}
        if(visible && hatch.hatchOpen && !hatch.activeHatch) {
            Debug.Log("Throw script");
		    if (transform.position.z < -10) {
			    Reset ();
		    }
		    if(Input.GetKey(KeyCode.T) && !thrown) {
			    Throw (target.transform.position);
		    }else if(Input.GetKey(KeyCode.R) && thrown){
			    Reset ();
		    }
		    if (Input.touchCount > 0) {
			    if (Input.GetTouch (0).phase == TouchPhase.Began) {
                    swipe = Input.GetTouch(0).position;
			    } else if ((Input.GetTouch (0).phase == TouchPhase.Ended) && swipe.y != 0f) {
				    float deltaTouchY = Input.GetTouch(0).position.y - swipe.y;
				    if (deltaTouchY > (Screen.height / 3.0f) && !thrown) {
                        Throw (target.transform.position);
				    }
			    }
		    }
        }
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			if (contact.otherCollider.gameObject.name == "cylinderbocken") {
				//do nothing
			} else if(!clonked){
				clonked = true;
				clonk.Play ();
			}
		}
	}

	public void Reset(){
		thrown = false;
		clonked = false;
		transform.localPosition = new Vector3 (-0.09f, -0.13f, 0.48f);
        transform.localEulerAngles = new Vector3(-19.42f, 81.5f, -16.056f);
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
	}

    public void ActivateBottle(bool state)
    {
        if (visible && state)
        {
            bottle.SetActive(true);
        }
        else
        {
            bottle.SetActive(false);
        }
    }

	public void Throw(Vector3 target){
		thrown = true;
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 direction = target - transform.position;
		float force = direction.magnitude;
		direction = direction.normalized;
		direction.z = 0.5f;
		gameObject.GetComponent<Rigidbody> ().AddForce (direction * force * 100);
		// test
	}
}
