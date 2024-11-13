using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyReset : MonoBehaviour
{
    Vector3 originalPos;
    Quaternion originalRot;

    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void Update()
    {
        if (Input.GetButton("Reset"))
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }
}
