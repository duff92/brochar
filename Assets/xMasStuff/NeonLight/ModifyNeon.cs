using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ModifyNeon : MonoBehaviour
{


    Renderer rend;
    float radius;
    Vector3 centre;


    void Start()
    {

        rend = GetComponent<Renderer>();
        radius = rend.material.GetFloat("_Radius");
        centre = rend.material.GetVector("_Centre");


    }

    void InitializePosition()
    {
        if (gameObject.GetComponent<MeshFilter>().mesh.name == "Cylinder")
        {
            centre = gameObject.transform.position;
            rend.material.SetVector("_Centre", centre);
        }
        else
        {
            Debug.Log("NOT A CYLINDER!!!");
        }


    }

    void LateUpdate()
    {
        centre = gameObject.transform.position;
        rend.material.SetVector("_Centre", centre);
    }

}

