using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchReset : MonoBehaviour
{
    float startOrthographicSize;
    Vector3 startPosition;
    Quaternion startRotation;

    void Start()
    {
        startOrthographicSize = Camera.main.orthographicSize;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetTouch(0).tapCount == 2)
        {
            Camera.main.orthographicSize = startOrthographicSize;
            transform.position = startPosition;
            transform.rotation = startRotation;
        }
    }
}
