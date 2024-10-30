using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseInputs : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Button resetButton;

    float startOrthographicSize;
    Vector3 startPosition;
    Quaternion startRotation;

    Inputs _inputs;

    Vector2 currentPosition;
    Vector2 newPosition;
    Vector2 changeInPosition;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(Reset);
        resetButton.GetComponent<Button>().onClick.AddListener(Reset);

        startOrthographicSize = Camera.main.orthographicSize;
        startPosition = transform.position;
        startRotation = transform.rotation;

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

    public void Reset()
    {
        Camera.main.orthographicSize = startOrthographicSize;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
