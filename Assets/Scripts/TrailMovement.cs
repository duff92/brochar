using UnityEngine;
using System.Collections;

public class TrailMovement : MonoBehaviour {

    public Vector3[] targets;
    public Transform trail;
    public float speed = 0.5f;
    public int laps = 2;

    public float padding = 0.1f;

    //Should be changed when image tracking starts on the target
    public bool activated = false;

    private int index = 0;
    private int currentLap = 1;

    void Start()
    {

        Vector3 parentScale = transform.parent.transform.localScale;
        //Debug.Log(parentScale);
        TrailRenderer TR = gameObject.GetComponent<TrailRenderer>();

        float xScale = parentScale.x + padding;
        float zScale = parentScale.z + padding;

        //Find corners uses TransformPoint that goes from local to world coordinates
        targets[0] = transform.TransformPoint(new Vector3(-xScale, 0.1f, zScale));
        targets[1] = transform.TransformPoint(new Vector3(xScale, 0.1f, zScale));
        targets[3] = transform.TransformPoint(new Vector3(-xScale, 0.1f, -zScale));
        targets[2] = transform.TransformPoint(new Vector3(xScale, 0.1f, -zScale));
        
        //Debug.Log(targets[0] +" : "+ targets[1] + " : " + targets[2] + " : " + targets[3]);
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
