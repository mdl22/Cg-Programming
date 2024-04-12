using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Button exitButton;
    [SerializeField] Button resetButton;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panAcrossSlider;
    [SerializeField] Slider panUpDownSlider;
    [SerializeField] Slider zoomSlider;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(ResetSliders);
        resetButton.GetComponent<Button>().onClick.AddListener(ResetSliders);
    }

    public void RotateObject()
    {
        transform.eulerAngles = new Vector3(verticalSlider.value, -horizontalSlider.value, 0);
    }

    void ResetSliders()
    {
        panUpDownSlider.value = panAcrossSlider.value =
            verticalSlider.value = horizontalSlider.value = 0;
        zoomSlider.value = zoomSlider.minValue;
    }
}
