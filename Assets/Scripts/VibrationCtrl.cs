using UnityEngine;
using System.Collections;
using Vuforia;

public class VibrationCtrl : MonoBehaviour
{
	private bool vibration = false;
	public ImageTargetBehaviour trigger;
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if ((trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED) != vibration) {
			vibration = (trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED);
			Handheld.Vibrate ();
		}
	}
}
