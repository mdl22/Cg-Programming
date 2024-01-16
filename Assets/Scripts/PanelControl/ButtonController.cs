using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Button resetButton;
    Vector3 originalPos;
    Quaternion originalRot;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ResetTransform);

        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void ResetTransform()
    {
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
