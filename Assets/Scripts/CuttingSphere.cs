using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingSphere : MonoBehaviour
{
    public GameObject other;

    void Update()
    {
        if(other != null)
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_CuttingSphereRadius",
                other.GetComponent<SphereCollider>().radius);

            GetComponent<Renderer>().sharedMaterial.SetMatrix("_InverseModelMatrix",
                other.GetComponent<Renderer>().worldToLocalMatrix);
        }
    }
}
