using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 40;
    [SerializeField] Transform customPivot;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;

    void Update()
    {
        float angle = speed * Time.deltaTime;

        // Rotates around y-axis based on key input
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(customPivot.position, Vector3.up, angle);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(customPivot.position, Vector3.down, angle);
        }
        // Rotates around x-axis based on key input
        else if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(customPivot.position, Vector3.right, angle);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(customPivot.position, Vector3.left, angle);
        }
    }

    /*void OnGUI()
    {
        float angle = speed * Time.deltaTime;

        Event e = Event.current;

        if (e.isKey)
        {
            switch (e.keyCode) 
            {
                case KeyCode.W:
                    transform.RotateAround(customPivot.position, Vector3.right, angle);
                    break;
                case KeyCode.S:
                    transform.RotateAround(customPivot.position, Vector3.left, angle);
                    break;
                case KeyCode.A:
                    transform.RotateAround(customPivot.position, Vector3.up, angle);
                    break;
                case KeyCode.D:
                    transform.RotateAround(customPivot.position, Vector3.down, angle);
                    break;
            }
        }
    }*/
}
