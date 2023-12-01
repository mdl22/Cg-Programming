using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float turnSpeed = 40;

    [SerializeField] Transform customPivot;

    void Update()
    {
        // Rotates around y-axis based on key input
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(customPivot.position, Vector3.right,
                turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(customPivot.position, Vector3.left,
                turnSpeed * Time.deltaTime);
        }

        // Rotates around x-axis based on key input
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(customPivot.position, Vector3.up,
                turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(customPivot.position, Vector3.down,
                turnSpeed * Time.deltaTime);
        }
    }
}
