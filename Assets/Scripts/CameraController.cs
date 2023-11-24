using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 40;

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.B))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
