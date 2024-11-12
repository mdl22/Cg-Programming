using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraController : MonoBehaviour
{
    [SerializeField] float minOrthographicSize;
    [SerializeField] float scaling = 0.0002f;

    float startOrthographicSize;
    float currentSeparation;
    float separation;

    void Start()
    {
        startOrthographicSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            separation = (Input.GetTouch(1).position - Input.GetTouch(0).position).magnitude;

            if (separation != currentSeparation)
            {
                // zoom in and out based on change in separation between touches
                Camera.main.orthographicSize =
                    minOrthographicSize * scaling * (separation - currentSeparation);

                if (Camera.main.orthographicSize > startOrthographicSize)
                {
                    Camera.main.orthographicSize = minOrthographicSize;
                }

                currentSeparation = separation;
            }
        }
        else if (Input.touchCount == 3)
        {
            /*newPosition = _inputs.UI.Point.ReadValue<Vector2>();
            changeInPosition = newPosition - currentPosition;
            currentPosition = newPosition;

            if (Mathf.Abs(changeInPosition.x) > Mathf.Abs(changeInPosition.y))
            {
                transform.Rotate(Vector3.down * changeInPosition.x, Space.World);
            }
            else
            {
                transform.Rotate(Vector3.right * changeInPosition.y, Space.World);
            }*/
        }
    }
}
