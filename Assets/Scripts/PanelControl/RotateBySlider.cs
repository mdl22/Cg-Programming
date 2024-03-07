using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    //[SerializeField] GameObject other;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panSlider;
    [SerializeField] Slider zoomSlider;
    [SerializeField] Slider windowSlider;
    Quaternion startRotation;

    public Button resetButton;

    void Start()
    {
        startRotation = transform.rotation;

        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetSliders);
    }

    public void RotateObject()
    {
        //other.transform.localEulerAngles = new Vector3(
        //    verticalSlider.value, -horizontalSlider.value, 0);
        //transform.eulerAngles = new Vector3(verticalSlider.value, -horizontalSlider.value, 0);
        transform.rotation = startRotation;
        transform.RotateAround(Vector3.zero, Vector3.up, -horizontalSlider.value);
        transform.RotateAround(Vector3.zero, Vector3.left, verticalSlider.value);
    }

    void ResetSliders()
    {
        panSlider.value = verticalSlider.value = horizontalSlider.value = 0;
        zoomSlider.value = zoomSlider.minValue;
        windowSlider.value = windowSlider.minValue;
    }
}
