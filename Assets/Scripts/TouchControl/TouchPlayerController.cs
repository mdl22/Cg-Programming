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
                deltaPosition = startPosition - touch.position;

                // move to world origin using pivot offset
                transform.position += customPivot.position;

                if (Math.Abs(deltaPosition.x) > Math.Abs(deltaPosition.y))
                {
                    // rotate horizontally (parent object rotation exchanges y- and z-axes)
                    transform.localEulerAngles = new Vector3(
                        transform.localEulerAngles.x, 0, deltaPosition.x);
                }
                else
                {
                    // rotate vertically (parent object rotation exchanges y- and z-axes)
                    transform.localEulerAngles = new Vector3(
                        deltaPosition.y, 0, transform.localEulerAngles.y);
                }

                // move back to position
                transform.position -= customPivot.position;
            }
        }
    }
}
