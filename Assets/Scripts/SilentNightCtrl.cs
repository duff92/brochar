using UnityEngine;
using System.Collections;

public class SilentNightCtrl : MonoBehaviour {
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
				GetComponent<AudioSource> ().Play ();
			} else {
				GetComponent<AudioSource> ().Stop ();
			}
		}
	}
}
