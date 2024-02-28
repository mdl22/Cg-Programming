using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputs : MonoBehaviour
{
    [SerializeField] Transform customPivot;
    Inputs _inputs;
    Vector2 currentPos;
    Vector2 previousPos;
    Vector2 deltaPos;

    void Start()
    {
        _inputs = new Inputs();
        _inputs.UI.Enable();
    }

    void Update()
    {
        if (_inputs.UI.Click.WasPressedThisFrame())
        {
            previousPos = _inputs.UI.Point.ReadValue<Vector2>();
            Debug.Log(previousPos);
        }

        if (_inputs.UI.Click.IsPressed())
        {
            currentPos = _inputs.UI.Point.ReadValue<Vector2>();
            deltaPos = currentPos - previousPos;

            // rotate horizontally
                transform.RotateAround(customPivot.position, Vector3.down, deltaPos.x);

            // rotate vertically
                transform.RotateAround(customPivot.position, Vector3.right, deltaPos.y);

            previousPos = currentPos;
            Debug.Log(currentPos);
        }
    }
}
