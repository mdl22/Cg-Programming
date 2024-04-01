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

    public void ResetPanelText(bool boolean = true)
    {
        panelTitleText.gameObject.SetActive(boolean);
        panelListText.gameObject.SetActive(boolean);

        areaTitleText.gameObject.SetActive(!boolean);
        areaDescriptionText.gameObject.SetActive(!boolean);

        backButton.gameObject.SetActive(!boolean); 
    }

    public void ResetPanel()
    {
        areasPanel.gameObject.SetActive(false); 
        areasButton.gameObject.SetActive(true); 

        ResetPanelText();
    }
}
