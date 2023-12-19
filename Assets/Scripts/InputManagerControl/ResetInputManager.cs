using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInputManager : MonoBehaviour
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
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }
}
