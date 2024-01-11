using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPlayerController : MonoBehaviour
{
    [SerializeField] float speed = 40;
    [SerializeField] Transform customPivot;

    void Update()
    {
        float angle = speed * Time.deltaTime;

        // rotate around y-axis based on key input
        if (Input.GetButton("Rotate Left"))
        {
            transform.RotateAround(customPivot.position, Vector3.up, angle);
        }
        else if (Input.GetButton("Rotate Right"))
        {
            transform.RotateAround(customPivot.position, Vector3.down, angle);
        }
        // rotate around x-axis based on key input
        else if (Input.GetButton("Rotate Up"))
        {
            transform.RotateAround(customPivot.position, Vector3.right, angle);
        }
        else if (Input.GetButton("Rotate Down"))
        {
            transform.RotateAround(customPivot.position, Vector3.left, angle);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"(U, V) = ({hit.textureCoord.x}, {hit.textureCoord.y})");
            }
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
