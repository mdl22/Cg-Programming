using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Renderer))]
public class ShadingInWorldSpace : MonoBehaviour
{
    public GameObject other;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

   void Update()
    {
        if(other != null)
        {
            rend.sharedMaterial.SetVector("_Point", other.transform.position);
        }
    }
}
