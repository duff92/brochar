using UnityEngine;
using System.Collections;
using Vuforia;

public class TrailMovement : MonoBehaviour {
    public ImageTargetBehaviour trigger;
    public GameObject hinge;
    public float speed = 0.5f;
    
    private bool activated = false;
    private Vector3[] targets = new Vector3[4];
    private int index = 0;
    private ParticleSystem particleSystem;

    void Start()
    {

        Vector3 parentScale = transform.parent.transform.localScale;
        //Debug.Log(parentScale);

        float xScale = parentScale.x;
        float zScale = parentScale.z;
        particleSystem = this.gameObject.GetComponent<ParticleSystem>();
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
        if ((trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED) != activated )
        {
            activated = (trigger.CurrentStatus == ImageTargetBehaviour.Status.TRACKED);
        }
        if(activated && hinge.transform.eulerAngles.z == 0)
        {
            if(!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
            else
            {
                float step = speed * Time.fixedDeltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, targets[index], step);
                if (this.transform.position == targets[index])
                {
                    if (index < targets.Length - 1)
                    {
                        index++;
                    }
                    else
                    {
                        //If back to first position, reset index of targets
                        index = 0;
                    }
                }
            }
        }
        if(hinge.transform.localEulerAngles.z > 240 && hinge.transform.localEulerAngles.z < 359)
        {
            Destroy(this.gameObject);
        }
    }
}
