using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 40;
    [SerializeField] Slider zoomSlider;
    [SerializeField] Slider spinSlider;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        zoomSlider.value = zoomSlider.minValue = startPosition.z;
        zoomSlider.maxValue = 0;
    }

    void Update()
    {
        float distance = speed * Time.deltaTime;
        float angle = turnSpeed * Time.deltaTime;
        float horizontalKeyValue = -Input.GetAxis("Horizontal");
        float verticalKeyValue = Input.GetAxis("Vertical");

        // Zooms in and out based on key input and slider
        if (verticalKeyValue != 0)
        {
            transform.position += Vector3.forward * distance * verticalKeyValue;
            if (transform.position.z > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else if (transform.position.z < startPosition.z)
            {
                transform.position = startPosition;
            }
            zoomSlider.value = transform.position.z;
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x, transform.position.y, zoomSlider.value);
        }

        // Rotates around y-axis based on key input
        transform.Rotate(Vector3.up, angle * horizontalKeyValue);
    }
}
