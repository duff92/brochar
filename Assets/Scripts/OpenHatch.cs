using UnityEngine;
using System.Collections;

public class OpenHatch : MonoBehaviour {

    private bool activeHatch = false;
    public GameObject hinge;
	// Update is called once per frame
	void Update () {

        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Began");
                RaycastHit hit;
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.transform.gameObject.tag == "Hatch")
                    {
                        activeHatch= true;
                    }
                }
            }
            if(Input.GetTouch(0).phase == TouchPhase.Moved && activeHatch)
            {
                Debug.Log("Moved");
                float deltaTouchX = Input.GetTouch(0).deltaPosition.x;
                hinge.transform.Rotate(new Vector3(0, 0, -1) * deltaTouchX * 0.5f);

                if (hinge.transform.eulerAngles.z < 240)
                {
                    if(deltaTouchX > 0)
                    {
                        hinge.transform.eulerAngles = new Vector3(0, 0, 240);
                    }
                    else
                    {
                        hinge.transform.eulerAngles = new Vector3(0, 0, 359);
                    }
                }
             

                //this.transform.RotateAround(this.transform.position, new Vector3(0,0,1), Input.GetTouch(0).deltaPosition.x * 10);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                activeHatch = false;
                Debug.Log("Touch ended");
            }
       }
    }
}