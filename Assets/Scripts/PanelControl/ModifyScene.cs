using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifyScene : MonoBehaviour
{
    [SerializeField] Image areasPanel;
    [SerializeField] Image controlsPanel;
    [SerializeField] TextMeshProUGUI panelTitleText;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;
    [SerializeField] Button areasButton;
    [SerializeField] Button backButton;

    public void ResetPanel(bool active = true)
    {
        // get material from ClickOnRegion script attached to the child ("default" gameobject)
        // of the first, and only, active child of brain GameObject
        Material material = GetComponentInChildren<ClickOnArea>().material;

        if (active)
        {
            material.SetColor("_EmissionColor", new Color32(0, 0, 0, 0));       // Black
        }
        else
        {
            material.SetColor("_EmissionColor", new Color32(127, 159, 187, 0)); // Blue
        }

        panelTitleText.gameObject.SetActive(active);
        panelListText.gameObject.SetActive(active);

        areaTitleText.gameObject.SetActive(!active);
        areaDescriptionText.gameObject.SetActive(!active);

        backButton.gameObject.SetActive(!active);
    }

    public void ClosePanel(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        areasPanel.gameObject.SetActive(false);
        areasButton.gameObject.SetActive(active);

        ResetPanel();
    }
}
