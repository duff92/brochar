using UnityEngine;
using System.Collections;

public class GoatCtrl : MonoBehaviour {
	public BottleCtrl molotov;
	private bool thrown = false;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;

	}

	void OnCollisionEnter(){
		ParticleSystem[] flame = gameObject.GetComponentsInChildren<ParticleSystem> ();
		for (int i = 0; i < flame.Length; i++) {
			flame [i].Play ();
		}
		AudioSource[] audio = gameObject.GetComponentsInChildren<AudioSource> ();
		for (int i = 0; i < audio.Length; i++) {
			audio [i].Play ();
		}
		Destroy (molotov);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.T) && !thrown) {
				thrown = true;
				molotov.Throw (transform.position);
		}else if(Input.GetKey(KeyCode.R) && thrown){
			thrown = false;
			molotov.GetComponent<BottleCtrl> ().Reset ();
		}
	}
}
