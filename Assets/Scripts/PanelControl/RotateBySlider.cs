using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    //[SerializeField] GameObject other;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panAcrossSlider;
    [SerializeField] Slider panUpDownSlider;
    [SerializeField] Slider zoomSlider;

    public Button resetButton;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetSliders);
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
