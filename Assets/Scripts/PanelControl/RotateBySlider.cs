using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panSlider;
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
        other.transform.localEulerAngles = new Vector3(
            verticalSlider.value, -horizontalSlider.value, 0);
    }

    void ResetSliders()
    {
        panSlider.value = verticalSlider.value = horizontalSlider.value = 0;
        zoomSlider.value = zoomSlider.minValue;
        windowSlider.value = windowSlider.minValue;
    }
}
