using UnityEngine;
using System.Collections;

public class TrailMovement : MonoBehaviour {
    
    public float speed = 0.5f;
    public int laps = 2;
    public bool activated = false;
    
    private Vector3[] targets = new Vector3[4];
    private int index = 0;
    private int currentLap = 1;
    private ParticleSystem thisParticleSystem;

    void Start()
    {

        Vector3 parentScale = transform.parent.transform.localScale;
        //Debug.Log(parentScale);

        float xScale = parentScale.x;
        float zScale = parentScale.z;
        thisParticleSystem = this.gameObject.GetComponent<ParticleSystem>();
        Debug.Log(xScale + ", " + zScale);
        //Find corners uses TransformPoint that goes from local to world coordinates
        targets[0] = transform.TransformPoint(new Vector3(-2 * xScale, transform.position.y, 0.0f));
        targets[1] = transform.TransformPoint(new Vector3(-2 * xScale, transform.position.y, zScale));
        targets[2] = transform.TransformPoint(new Vector3(0.0f, transform.position.y, zScale));
        targets[3] = transform.TransformPoint(new Vector3(0.0f, transform.position.y, 0.0f));
        
        Debug.Log(targets[0] +" : "+ targets[1] + " : " + targets[2] + " : " + targets[3]);
    }
    void FixedUpdate()
    {
        if(activated)
        {
            if (!thisParticleSystem.isPlaying) {
                thisParticleSystem.Play();
            }
            else {
                float step = speed * Time.fixedDeltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, targets[index], step);

                if(activated && this.transform.position == targets[index])
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
                            //Destroy(this.gameObject);
                            thisParticleSystem.Stop();
                            thisParticleSystem.Clear();
                            //to restore the border
                            activated = false;
                            index = 0;
                            currentLap = 1;
                        }
                    }
                }
            }
        }

    }
}
