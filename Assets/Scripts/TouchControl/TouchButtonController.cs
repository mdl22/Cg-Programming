using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float minOrthographicSize;
    [SerializeField] float sizeChangeSpeed;

    float startOrthographicSize;
    public bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    void Start()
    {
        startOrthographicSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        if (buttonPressed)
        {
            float sizeChange = sizeChangeSpeed * Time.deltaTime;

            if (EventSystem.current.currentSelectedGameObject.name == "ZoomIn Button")
            {
                if ((Camera.main.orthographicSize -= sizeChangeSpeed) < minOrthographicSize)
                {
                    Camera.main.orthographicSize = minOrthographicSize;
                }
            }
            else
            {
                if ((Camera.main.orthographicSize += sizeChangeSpeed) > startOrthographicSize)
                {
                    Camera.main.orthographicSize = startOrthographicSize;
                }
            }
        }
    }
}
