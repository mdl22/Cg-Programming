using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform customPivot;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider spinSlider;
    [SerializeField] Slider zoomSlider;
    [SerializeField] Slider windowSlider;

    public Button resetButton;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetSliders);
    }

    public void RotateObject()
    {
        // move to world origin using pivot offset
        other.transform.position += customPivot.position;

        other.transform.localEulerAngles = new Vector3(
            verticalSlider.value, 0, horizontalSlider.value);

        // move back to position
        other.transform.position -= customPivot.position;
    }

    public void RotateCamera()
    {
        mainCamera.transform.localEulerAngles = new Vector3(0, spinSlider.value, 0);
    }

    void ResetSliders()
    {
        spinSlider.value = verticalSlider.value = horizontalSlider.value = 0;
        zoomSlider.value = zoomSlider.minValue;
        windowSlider.value = windowSlider.minValue;
    }
}
