using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] GameObject modelButtons;

    [SerializeField] Button exitButton;
    [SerializeField] Button resetButton;

    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panLeftRightSlider;
    [SerializeField] Slider panUpDownSlider;
    [SerializeField] Slider zoomSlider;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(ResetSliders);
        resetButton.GetComponent<Button>().onClick.AddListener(ResetSliders);

        foreach (Button button in modelButtons.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(ResetSliders);
        }
    }

    public void Rotate()
    {
        transform.eulerAngles = new Vector3(-verticalSlider.value, -horizontalSlider.value, 0);
    }

    public void ResetSliders()
    {
        SetMidpoint(horizontalSlider);
        SetMidpoint(verticalSlider);
        SetMidpoint(panLeftRightSlider);
        SetMidpoint(panUpDownSlider);

        zoomSlider.value = zoomSlider.maxValue;
    }

    void SetMidpoint(Slider slider)
    {
        slider.value = (slider.minValue + slider.maxValue) / 2;
    }
}
