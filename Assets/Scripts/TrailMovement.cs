using UnityEngine;
using System.Collections;

public class TrailMovement : MonoBehaviour {

    public Transform[] targets;
    public Transform trail;
    public float speed = 2.0f;
    public int laps = 2;

    private int index = 0;
    private int currentLap = 1;

    void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        trail.position = Vector3.MoveTowards(trail.position, targets[index].position, step);
        
        if(trail.position == targets[index].position)
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
                }
            }
        }

    }
}
