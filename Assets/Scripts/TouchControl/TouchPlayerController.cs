using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayerController : MonoBehaviour
{
    [SerializeField] Transform customPivot;

    Vector2 startPosition;
    Vector2 deltaPosition;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                deltaPosition = touch.position - startPosition;

                // move to world origin using pivot offset
                transform.position += customPivot.position;

                if (Math.Abs(deltaPosition.x) > Math.Abs(deltaPosition.y))
                {
                    // rotate horizontally (parent object rotation exchanges y- and z-axes)
                    transform.localEulerAngles = new Vector3(
                        transform.localEulerAngles.x, 0, deltaPosition.y);
                }
                else
                {
                    // rotate vertically
                    transform.localEulerAngles = new Vector3(
                        deltaPosition.x, 0, transform.localEulerAngles.y);
                }

                // move back to position
                transform.position -= customPivot.position;
            }
        }
    }
}
