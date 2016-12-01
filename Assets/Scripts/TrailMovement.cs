using UnityEngine;
using System.Collections;

public class TrailMovement : MonoBehaviour {

    public Transform trail;
    public float speed = 0.5f;
    public int laps = 2;

    public float padding = 0.1f;

    //Should be changed when image tracking starts on the target
    public bool activated = false;

    private Vector3[] targets = new Vector3[4];
    private int index = 0;
    private int currentLap = 1;

    void Start()
    {

        Vector3 parentScale = transform.parent.transform.localScale;
        //Debug.Log(parentScale);

        float xScale = parentScale.x;
        float zScale = parentScale.z;

       // Debug.Log(xScale + ", " + zScale);
        //Find corners uses TransformPoint that goes from local to world coordinates
        targets[0] = transform.TransformPoint(new Vector3(xScale, 0.1f, 0.0f));
        targets[1] = transform.TransformPoint(new Vector3(xScale, 0.1f, -zScale));
        targets[2] = transform.TransformPoint(new Vector3(0.0f, 0.1f, -zScale));
        targets[3] = transform.TransformPoint(new Vector3(0.0f, 0.1f, 0.0f));
        
        Debug.Log(targets[0] +" : "+ targets[1] + " : " + targets[2] + " : " + targets[3]);
    }
    void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        trail.position = Vector3.MoveTowards(trail.position, targets[index], step);
        
        if(activated && trail.position == targets[index])
        {
            if(index < targets.Length -1)
            {
                index++;
            }
            else
            {
                if(currentLap < laps)
                {
                    currentLap++;
                    index = 0;
                }
                else
                {
                    Destroy(this.gameObject);

                    //to restore the border
                    /*activated = false;
                    index = 0;
                    currentLap = 1;*/
                }
            }
        }

    }
}
