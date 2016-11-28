using UnityEngine;
using System.Collections;

public class SnowCtrl : MonoBehaviour {
	private bool playing = false;
	public Renderer trigger;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (trigger.enabled != playing) {
			playing = trigger.enabled;
			if (playing) {
				GetComponent<ParticleSystem> ().Play ();
			} else {
				GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}
}
