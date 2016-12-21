using UnityEngine;
using System.Collections;

public class LeverToggle : MonoBehaviour {

	private bool leverSwitch = false;
	private float degree;
	public Transform rotateRound;

	void Update(){

//		if ( Input.GetTouch(0).phase == TouchPhase.Began ){
		if ( Input.GetMouseButtonUp(0) ){
			RaycastHit hit;

			Vector2 clickedPos = Input.mousePosition;
			//Vector2 clickedPos = Input.GetTouch (0).position;

			Ray ray = Camera.main.ScreenPointToRay(clickedPos);

			if (Physics.Raycast(ray, out hit, 100)){
				if (hit.transform.gameObject.tag == "Lever"){
					leverSwitch = !leverSwitch;
				}
			}
		}
			
		if (leverSwitch) {
			degree = -30f;
		} else {
			degree = 30f;		
		}
		rotateRound.rotation = Quaternion.Slerp (rotateRound.rotation, Quaternion.Euler (degree, 0, 0), 0.5f);
	}
		
}
