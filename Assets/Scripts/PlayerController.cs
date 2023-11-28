using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float turnSpeed = 40;
    [SerializeField] float cutoff = 0.1f;
    float horizontalKeyValue;
    float verticalKeyValue;
    float horizontalSliderValue;
    float verticalSliderValue;

    [SerializeField] Transform customPivot;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;

    void Update()
    {
        horizontalKeyValue = -Input.GetAxis("Horizontal");
        verticalKeyValue = Input.GetAxis("Vertical");

        horizontalSliderValue = -RecalibrateSlider(horizontalSlider, cutoff);
        verticalSliderValue = -RecalibrateSlider(verticalSlider, cutoff);

        // Rotates around y-axis based on horizontal input and slider
        transform.RotateAround(customPivot.position, Vector3.up,
            turnSpeed * Time.deltaTime * (horizontalKeyValue + horizontalSliderValue));

        // Rotates around x-axis based on vertical input and slider
        transform.RotateAround(customPivot.position, Vector3.right,
            turnSpeed * Time.deltaTime * (verticalKeyValue + verticalSliderValue));
    }

    public float RecalibrateSlider(Slider slider, float cutoff)
    // Returns 0 at -cutoff and cutoff, but maxValue and minValue unchanged
    {
        return Math.Abs(slider.value) > cutoff ?
            (slider.value - cutoff) / (slider.maxValue - cutoff) : 0;
    }
}
