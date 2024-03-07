using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchReset : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetTouch(0).tapCount == 2)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
