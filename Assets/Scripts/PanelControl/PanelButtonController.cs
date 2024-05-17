using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelButtonController : MonoBehaviour
{
    List<GameObject> models = new List<GameObject>();

    [SerializeField] Button[] modelButtons;
    [SerializeField] Button controlsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button areasButton;
    [SerializeField] Button backButton;

    [SerializeField] Image areasPanel;
    [SerializeField] Image controlsPanel;

    [SerializeField] TextMeshProUGUI panelTitleText;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    void Start()
    {
        foreach (Transform child in transform)
        {
            models.Add(child.gameObject);
        }

        for (int i = 0; i < modelButtons.Length; i++)
        {
            int index = i;

            modelButtons[index].GetComponent<Button>().onClick.AddListener(() =>
                { ActivateModel(index); });
        }
        exitButton.GetComponent<Button>().onClick.AddListener(() => { ActivateModel(-1); });
    }

    void ActivateModel(int index)
    {
        bool exit = false;
        if (index < 0)
        {
            index = 0;
            exit = true;
        }

        foreach (Button button in modelButtons)
        {
            button.gameObject.SetActive(true);
            button.interactable = true;
        }

        string buttonName = modelButtons[index].name.Split()[0];

        if (buttonName == "HippocampusBoolean")
        {
            modelButtons[index].gameObject.SetActive(false);
        }
        modelButtons[index].interactable = false;

        foreach (GameObject model in models)
        {
            if (model.name == buttonName)
            {
                model.SetActive(true);
            }
            else
            {
                model.SetActive(false);
            }
        }

        ClosePanels(exit ? false : true);
    }

    public void ClosePanels(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        controlsButton.gameObject.SetActive(!active);
        areasPanel.gameObject.SetActive(false);
        areasButton.gameObject.SetActive(active);

        ResetAreasPanel(true);
    }

    public void ResetAreasPanel(bool active)
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
}
