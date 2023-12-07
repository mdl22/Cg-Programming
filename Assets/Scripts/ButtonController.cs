using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Vector3 originalPos;
    Quaternion originalRot;

    public Button resetButton;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetTransform);

        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void ResetTransform()
    {
        //horizontalSlider.value = verticalSlider.value = 0;

        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
