using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float minOrthographicSize;
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
            if (EventSystem.current.currentSelectedGameObject.name == "ZoomIn Button")
            {
                Camera.main.orthographicSize = minOrthographicSize;
            }
            else
            {
                Camera.main.orthographicSize = startOrthographicSize;
            }
        }
    }
}
