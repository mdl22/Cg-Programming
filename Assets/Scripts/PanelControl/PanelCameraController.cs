using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCameraController : MonoBehaviour
{
    [SerializeField] Slider zoomSlider;
    [SerializeField] Slider panSlider;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        zoomSlider.value = zoomSlider.minValue = startPosition.z;
        zoomSlider.maxValue = 0;
    }

    void Update()
    {
        // zoom in and out based on a slider
        transform.position = new Vector3(
            transform.position.x, transform.position.y, zoomSlider.value);

        // Pan along x-axis based on a slider
        transform.position = new Vector3(
            -panSlider.value, transform.position.y, transform.position.z);
    }
}
