using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] Camera mainCamera;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider spinSlider;

    public Button resetButton;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetSliders);
    }

    public void RotateObject()
    {
        other.transform.localEulerAngles = new Vector3(
            verticalSlider.value, 0, horizontalSlider.value);
    }

    public void RotateCamera()
    {
        mainCamera.transform.localEulerAngles = new Vector3(0, -spinSlider.value, 0);
    }

    void ResetSliders()
    {
        horizontalSlider.value = verticalSlider.value = 0;
    }
}
