using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseInputs : MonoBehaviour
{
    [SerializeField] GameObject modelButtons;

    [SerializeField] Button controlButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button ZoomInButton;
    [SerializeField] Button ZoomOutButton;
    [SerializeField] Button resetButton;

    [SerializeField] float scale;

    Quaternion startRotation;
    Vector3 startPosition;
    Vector3 startCameraPosition;
    Vector3 currentMousePosition;
    float startOrthographicSize;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(Reset);
        resetButton.GetComponent<Button>().onClick.AddListener(Reset);

        foreach (Button button in modelButtons.GetComponentsInChildren<Button>())
        {            
            button.onClick.AddListener(Reset);
        }

        startPosition = transform.position;
        startRotation = transform.rotation;

        startCameraPosition = Camera.main.transform.position;
        startOrthographicSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (!controlButton.gameObject.activeSelf &&
                !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 dist = Input.mousePosition - currentMousePosition;
                currentMousePosition = Input.mousePosition;

                if (Input.GetMouseButton(0))
                {
                    transform.Rotate(Mathf.Abs(dist.x) > Mathf.Abs(dist.y) ?
                        Vector3.down*dist.x * 360/Screen.width :
                        Vector3.right*dist.y * 360/Screen.height, Space.World);
                }
                else if (Input.GetMouseButton(1))
                {
                    Camera.main.transform.position -= new Vector3(
                        dist.x * 2*scale/Screen.width, dist.y * scale/Screen.height, 0);
                }
            }
        }
        else
        {
            currentMousePosition = Input.mousePosition;
        }
    }

    public void Reset()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        Camera.main.transform.position = startCameraPosition;
        Camera.main.orthographicSize = startOrthographicSize;
    }
}
