using UnityEngine;
using System.Collections;

public class GoatCtrl : MonoBehaviour {
	public BottleCtrl molotov;
	public AudioSource rudolph;

	// Use this for initialization
	void Start () {

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
		rudolph.Stop ();
		Destroy (molotov);
	}

	// Update is called once per frame
	void Update () {

	}
}
