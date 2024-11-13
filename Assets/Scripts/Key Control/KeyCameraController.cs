using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCameraController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float panLimit = 10;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = speed * Time.deltaTime;
        float horizontalKeyValue = -Input.GetAxis("Horizontal");
        float verticalKeyValue = Input.GetAxis("Vertical");

        // Zoom in and out based on up/down arrow key input
        transform.position += Vector3.forward * distance * verticalKeyValue;

        if (transform.position.z > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (transform.position.z < startPosition.z)
        {
            transform.position = startPosition;
        }

        // Pan along x-axis based on left/right arrow key input
        transform.position += Vector3.right * distance * horizontalKeyValue;

        if (Mathf.Abs(transform.position.x) > panLimit)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * panLimit,
                transform.position.y, transform.position.z);
        }
    }
}
