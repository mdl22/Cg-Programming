using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInputs : MonoBehaviour
{
    [SerializeField] Button controlButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button ZoomInButton;
    [SerializeField] Button ZoomOutButton;
    [SerializeField] Button resetButton;

    Vector3 startPosition;
    Quaternion startRotation;

    Vector2 currentPosition;
    Vector2 newPosition;
    Vector2 changeInPosition;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(Reset);
        resetButton.GetComponent<Button>().onClick.AddListener(Reset);

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!controlButton.gameObject.activeSelf &&
                !EventSystem.current.IsPointerOverGameObject())
            {
                newPosition = Input.mousePosition;
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

    public void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
