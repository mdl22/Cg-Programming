using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCameraController : MonoBehaviour
{
    [SerializeField] Slider panLeftRightSlider;
    [SerializeField] Slider panUpDownSlider;
    [SerializeField] Slider zoomSlider;

    public void PanAlongXAxis()
    {
        transform.position = new Vector3(
            -panLeftRightSlider.value, transform.position.y, transform.position.z);
    }

    public void PanAlongYAxis()
    {
        transform.position = new Vector3(
            transform.position.x, -panUpDownSlider.value, transform.position.z);
    }

    public void Zoom()
    {
        Camera.main.orthographicSize = zoomSlider.value;
    }
}
