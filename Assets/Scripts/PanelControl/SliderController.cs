using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Transform customPivot;
    [SerializeField] Button exitButton;
    [SerializeField] Button resetButton;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;
    [SerializeField] Slider panLeftRightSlider;
    [SerializeField] Slider panUpDownSlider;
    [SerializeField] Slider zoomSlider;

    Vector3 originalScale;

    void Start()
    {
        exitButton.GetComponent<Button>().onClick.AddListener(ResetSliders);
        resetButton.GetComponent<Button>().onClick.AddListener(ResetSliders);

        originalScale = transform.localScale;
    }

    void Update()
    {
        //transform.eulerAngles = new Vector3(0, -horizontalSlider.value, 0);
        //transform.rotation = Quaternion.Euler(-verticalSlider.value, 0, 0) * transform.rotation;

          //transform.rotation = transform.parent.rotation * transform.rotation;
        //transform.Rotate(-Vector3.up * transform.eulerAngles.y);
        //transform.Rotate(-Vector3.up * horizontalSlider.value);

        //transform.Rotate(transform.right * transform.eulerAngles.x);
        //transform.Rotate(transform.right * verticalSlider.value);
    }

    public void Rotate()
    {
        //transform.RotateAround(customPivot.position, Vector3.up, -horizontalSlider.value);
        //transform.rotation = Quaternion.identity;
        //transform.rotation = Quaternion.Euler(verticalSlider.value, 0, 0) * transform.rotation * Quaternion.Euler(0, -horizontalSlider.value, 0);
        transform.eulerAngles = new Vector3(-verticalSlider.value, -horizontalSlider.value, 0);
    }

    public void RotateHorizontally()
    {
        transform.Rotate(-Vector3.up * transform.eulerAngles.y, Space.World);
        transform.Rotate(-Vector3.up * horizontalSlider.value, Space.World);
        /*transform.eulerAngles = new Vector3(0, -horizontalSlider.value, 0);
        transform.rotation = transform.rotation * Quaternion.Euler(-verticalSlider.value, 0, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, startRotation.y, 0);
        transform.Rotate(-transform.up * horizontalSlider.value);

        transform.rotation = Quaternion.identity;
        transform.eulerAngles = new Vector3(verticalSlider.value, 0, 0);
        transform.Rotate(-transform.up * horizontalSlider.value);
        transform.Rotate(Vector3.right * verticalSlider.value, Space.World);
        transform.RotateAround(transform.position, Vector3.zero, -horizontalSlider.value);
        transform.RotateAround(transform.position,
            new Vector3(0, -verticalSlider.value, 0), -horizontalSlider.value);
        transform.RotateAround(transform.position,
            new Vector3(horizontalSlider.value, 0, 0), verticalSlider.value);

        transform.Rotate(0, -horizontalSlider.value, 0);
        transform.Rotate(verticalSlider.value, 0, 0, Space.World);*/
    }

    public void RotateVertically()
    {
        transform.Rotate(transform.right * transform.eulerAngles.x);
        transform.Rotate(transform.right * verticalSlider.value);
        /*transform.eulerAngles = new Vector3(0, -horizontalSlider.value, 0);
        transform.rotation = Quaternion.Euler(verticalSlider.value, 0, 0) * transform.rotation;
        transform.eulerAngles = new Vector3(startRotation.x, transform.eulerAngles.y, 0);
        transform.Rotate(transform.right * verticalSlider.value);*/
    }

    public void Zoom()
    {
        transform.localScale = originalScale * zoomSlider.value;
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
