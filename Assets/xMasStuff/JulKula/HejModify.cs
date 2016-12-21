using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HejModify : MonoBehaviour {


    Renderer rend;
    float radius;
    Vector3 centre;

    
	void Start () {

        rend = GetComponent<Renderer>();

        radius = gameObject.GetComponent<SphereCollider>().radius;
        centre = gameObject.transform.position;

        rend.material.SetFloat("_Radius", radius);
        rend.material.SetVector("_Centre", centre);


    }

    void InitializePosition()
    {
        
        centre = gameObject.transform.position;
        rend.material.SetVector("_Centre", centre);

    }

    void LateUpdate()
    {
        centre = gameObject.transform.position;
        rend.material.SetVector("_Centre", centre);
    }
	
}
