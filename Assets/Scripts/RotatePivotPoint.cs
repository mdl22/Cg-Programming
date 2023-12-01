using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePivotPoint : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] Transform customPivot;
    [SerializeField] Slider horizontalSlider;
    [SerializeField] Slider verticalSlider;

    public void RotateToSliderValue()
    {
	other.transform.RotateAround(customPivot.position, Vector3.up,
            (horizontalSlider.value - other.transform.rotation.y) / Mathf.Rad2Deg);
    }
}
