using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackpadPlayerController : MonoBehaviour
{
    [SerializeField] Transform customPivot;
    Vector2 mousePos;
    Vector2 previousMousePos;
    Vector2 deltaPos;

    void Start()
    {
        mousePos = Input.mousePosition;
    }

    void Update()
    {
        previousMousePos = mousePos;
        mousePos = Input.mousePosition;
        deltaPos = mousePos - previousMousePos;

         // rotate horizontally
            transform.RotateAround(customPivot.position, Vector3.down, deltaPos.x);

        // rotate vertically
            transform.RotateAround(customPivot.position, Vector3.right, deltaPos.y);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"(U, V) = ({hit.textureCoord.x}, {hit.textureCoord.y})");
            }
        }
    }
}
