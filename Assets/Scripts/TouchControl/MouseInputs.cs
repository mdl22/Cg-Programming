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
    float startOrthographicSize;
    bool clicked;

    Vector2 currentPosition;
    Vector2 newPosition;
    Vector2 dist;

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
                if (clicked)
                {
                    newPosition = Input.mousePosition;
                }
                else
                {
                    newPosition = currentPosition;
                    clicked = true;
                }
                dist = newPosition - currentPosition;
                currentPosition = newPosition;

                if (Input.GetMouseButton(0))
                {
                    transform.Rotate(Mathf.Abs(dist.x) > Mathf.Abs(dist.y) ?
                        Vector3.down * dist.x : Vector3.right * dist.y, Space.World);
                }
                else
                {
                    Camera.main.transform.position -= Mathf.Abs(dist.x) > Mathf.Abs(dist.y) ?
                        new Vector3(dist.x * 2*scale, 0, 0) : new Vector3(0, dist.y * scale, 0);
                }
            }
        }
        else
        {
            clicked = false;
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
