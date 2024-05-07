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

    public void ResetAreasPanel(bool active = true)
    {
        // get material from ClickOnArea script if attached to the child ("default" GameObject)
        // of the first - and only - active child of brain GameObject
        if (GetComponentInChildren<ClickOnArea>() != null)
        {
            if (active)
            {
                GetComponentInChildren<ClickOnArea>().material.SetColor("_EmissionColor",
                    new Color32(0, 0, 0, 0));           // Black
            }
            else
            {
                GetComponentInChildren<ClickOnArea>().material.SetColor("_EmissionColor",
                    new Color32(127, 159, 187, 0));     // Blue
            }
        }
        else
        {
            areasButton.gameObject.SetActive(false);
        }

        panelTitleText.gameObject.SetActive(active);
        panelListText.gameObject.SetActive(active);

        areaTitleText.gameObject.SetActive(!active);
        areaDescriptionText.gameObject.SetActive(!active);

        backButton.gameObject.SetActive(!active);
    }

    public void ClosePanels(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        areasPanel.gameObject.SetActive(false);
        areasButton.gameObject.SetActive(active);

        ResetAreasPanel();
    }
}
