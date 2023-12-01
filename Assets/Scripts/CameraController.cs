using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 40;
    [SerializeField] float cutoff = 0.1f;
    float zoomValue;
    float spinValue;

    [SerializeField] Slider zoomSlider;
    [SerializeField] Slider spinSlider;

    void Update()
    {
        float horizontalKeyValue = -Input.GetAxis("Horizontal");
        float verticalKeyValue = Input.GetAxis("Vertical");

        zoomValue = RecalibrateSlider(zoomSlider, cutoff);
        spinValue = RecalibrateSlider(spinSlider, cutoff);

        // Zooms in and out based on vertical input
        transform.position += Vector3.forward * speed * Time.deltaTime * verticalKeyValue;
        transform.position -= Vector3.forward * speed * Time.deltaTime * zoomValue;

        // Rotates around y-axis based on key input and slider
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalKeyValue);
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime * spinValue);
    }

    float RecalibrateSlider(Slider slider, float cutoff)
    // Returns 0 at -cutoff and cutoff, but minValue and maxValue unchanged
    {
        if (slider.value < -cutoff)
        {
            return -(slider.value + cutoff) / (slider.minValue + cutoff);
        }
        else if (slider.value > cutoff)
        {
            return (slider.value - cutoff) / (slider.maxValue - cutoff);
        }
        else
        {
            return 0;
        }
    }
}
