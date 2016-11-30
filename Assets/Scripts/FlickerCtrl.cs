using UnityEngine;
using System.Collections;

public class FlickerCtrl : MonoBehaviour {
	private ParticleSystem flame;
	private Light light;
	// Use this for initialization
	void Start () {
		flame = gameObject.GetComponent<ParticleSystem> ();
		light = gameObject.GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		light.enabled = (flame.particleCount%2 == 0);
	}
}
