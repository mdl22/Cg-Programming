using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float turnSpeed = 40;
    float horizontalInput;
    float verticalInput;

    [SerializeField] Transform customPivot;
    //[SerializeField] Slider horizontalSlider;
    //[SerializeField] Slider verticalSlider;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Rotates around y-axis based on horizontal input
        transform.RotateAround(customPivot.position, Vector3.up,
            Time.deltaTime * turnSpeed * horizontalInput);

        // Rotates around x-axis based on vertical input
        transform.RotateAround(customPivot.position, Vector3.right,
            Time.deltaTime * turnSpeed * verticalInput);

        // Rotates around y-axis based on horizontal input
        //transform.RotateAround(customPivot.position, Vector3.up, horizontalSlider.value);

        // Rotates around x-axis based on vertical input
        //transform.RotateAround(customPivot.position, Vector3.right, verticalSlider.value);
    }
}
