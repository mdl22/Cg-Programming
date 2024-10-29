using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour
{
    Inputs _inputs;
    Vector2 currentPosition;
    Vector2 newPosition;
    Vector2 changeInPosition;

    void Start()
    {
        _inputs = new Inputs();
        _inputs.UI.Enable();
    }

    void Update()
    {
        if (_inputs.UI.Click.WasPressedThisFrame())
        {
            currentPosition = _inputs.UI.Point.ReadValue<Vector2>();
        }

        if (_inputs.UI.Click.IsPressed())
        {
            newPosition = _inputs.UI.Point.ReadValue<Vector2>();
            changeInPosition = newPosition - currentPosition;
            currentPosition = newPosition;

            if (Mathf.Abs(changeInPosition.x) > Mathf.Abs(changeInPosition.y))
            {
                transform.Rotate(Vector3.down * changeInPosition.x, Space.World);
            }
            else
            {
                transform.Rotate(Vector3.right * changeInPosition.y, Space.World);
            }
        }
    }
}
