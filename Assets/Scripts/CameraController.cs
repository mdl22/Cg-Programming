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
        zoomValue = FindObjectOfType<PlayerController>().RecalibrateSlider(zoomSlider, cutoff);
        spinValue = FindObjectOfType<PlayerController>().RecalibrateSlider(spinSlider, cutoff);

        // Zoomd in and out based on key input and slider
        if (Input.GetKey(KeyCode.F))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.B))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        transform.position += Vector3.forward * speed * Time.deltaTime * zoomValue;

        // Rotates around y-axis based on key input and slider
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * spinValue);
    }
}
