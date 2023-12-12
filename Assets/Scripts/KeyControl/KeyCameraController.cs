using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCameraController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 40;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = speed * Time.deltaTime;
        float angle = turnSpeed * Time.deltaTime;
        float horizontalKeyValue = -Input.GetAxis("Horizontal");
        float verticalKeyValue = Input.GetAxis("Vertical");

        // Zooms in and out based on arrow key input
        transform.position += Vector3.forward * distance * verticalKeyValue;
        if (transform.position.z > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (transform.position.z < startPosition.z)
        {
            transform.position = startPosition;
        }

        // Rotates around y-axis based on arrow key input
        transform.Rotate(Vector3.up, -angle * horizontalKeyValue);
    }
}
