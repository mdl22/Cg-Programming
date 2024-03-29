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
    [SerializeField] Button resetButton;
    [SerializeField] Button areasButton;
    [SerializeField] Button backButton;

    public void ResetPanelText()
    {
        panelTitleText.gameObject.SetActive(true);
        panelListText.gameObject.SetActive(true);

        areaTitleText.gameObject.SetActive(false);
        areaDescriptionText.gameObject.SetActive(false);

        backButton.gameObject.SetActive(false); 
    }

    public void ResetPanel()
    {
        areasPanel.gameObject.SetActive(false); 
        areasButton.gameObject.SetActive(true); 

        ResetPanelText();
    }
}
