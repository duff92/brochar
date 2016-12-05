using UnityEngine;
using System.Collections;
using Vuforia;

public class MusicCtrl : MonoBehaviour {
	private bool playing = false;
	public ImageTargetBehaviour trigger;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED) != playing) {
			playing = (trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED);
			if (playing) {
				GetComponent<AudioSource> ().Play ();
			} else {
				GetComponent<AudioSource> ().Stop ();
			}
		}
	}
}
