using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchReset : MonoBehaviour
{
    Vector3 originalPosition;
    Quaternion originalRotation;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetTouch(0).tapCount == 2)
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
