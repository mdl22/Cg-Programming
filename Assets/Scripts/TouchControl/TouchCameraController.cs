using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCameraController : MonoBehaviour
{
    [SerializeField] float minOrthographicSize = 0.02f;
    [SerializeField] float maxOrthographicSize = 0.1f;
    [SerializeField] float scaling = 0.0002f;

    float currentSeparation = 0;
    float separation;

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

                if (Camera.main.orthographicSize > maxOrthographicSize)
                {
                    Camera.main.orthographicSize = minOrthographicSize;
                }

                currentSeparation = separation;
            }
        }
        else if (Input.touchCount == 3)
        {
            /*public void PanAlongXAxis()
            {
                transform.position = new Vector3(
                    -panLeftRightSlider.value, transform.position.y, transform.position.z);
            }

            public void PanAlongYAxis()
            {
                transform.position = new Vector3(
                    transform.position.x, -panUpDownSlider.value, transform.position.z);
            }*/
        }
    }
}
