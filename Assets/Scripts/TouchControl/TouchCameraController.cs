using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraController : MonoBehaviour
{
    [SerializeField] float scaling = 0.1f;
    static float previousSeparation = 0;
    float separation;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            separation = (Input.GetTouch(1).position - Input.GetTouch(0).position).magnitude;

            if (separation != previousSeparation)
            {
                // zoom in and out based on separation between a pair of touches
                transform.position +=
                    Vector3.forward * scaling * (separation - previousSeparation);

                if (transform.position.z > 0)
                {
                    transform.position =
                        new Vector3(transform.position.x, transform.position.y, 0);
                }
                else if (transform.position.z < startPosition.z)
                {
                    transform.position = startPosition;
                }

                previousSeparation = separation;
            }
        }
    }
}
