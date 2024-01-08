using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchReset : MonoBehaviour
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
        if (Input.GetTouch(0).tapCount == 2)
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }
}
