using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reset : MonoBehaviour
{
    [SerializeField] Image areasPanel;
    [SerializeField] TextMeshProUGUI panelTitleText;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;
    [SerializeField] Button areasButton;
    [SerializeField] Button backButton;

    public void ResetPanel(bool reset = true)
    {
        // get material from ClickOnRegion script attached child (default gameobject)
        // of first and only active child of GameObject
        Material material = GetComponentInChildren<ClickOnRegion>().material;

        if (reset)
        {
            material.SetColor("_EmissionColor", new Color32(0, 0, 0, 0));       // Black
        }
        else
        {
            material.SetColor("_EmissionColor", new Color32(127, 159, 187, 0)); // Blue
        }

        panelTitleText.gameObject.SetActive(reset);
        panelListText.gameObject.SetActive(reset);

        areaTitleText.gameObject.SetActive(!reset);
        areaDescriptionText.gameObject.SetActive(!reset);

        backButton.gameObject.SetActive(!reset); 
    }

    public void ClosePanel()
    {
        areasPanel.gameObject.SetActive(false); 
        areasButton.gameObject.SetActive(true); 

        ResetPanel();
    }
}
