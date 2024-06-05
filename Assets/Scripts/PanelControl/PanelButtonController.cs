using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelButtonController : MonoBehaviour
{
    [SerializeField] GameObject modelButtonsParent;

    [SerializeField] Button controlsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button areasButton;
    [SerializeField] Button backButton;

    [SerializeField] Image areasPanel;
    [SerializeField] Image controlsPanel;
    [SerializeField] Image FimbriaFornixPanel;
    [SerializeField] Image SharedInputOutputPanel;

    [SerializeField] TextMeshProUGUI panelTitleText;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    List<GameObject> models = new List<GameObject>();
    Button[] modelButtons;

    void Start()
    {
        foreach (Transform child in transform)
        {
            models.Add(child.gameObject);
        }

        modelButtons = modelButtonsParent.GetComponentsInChildren<Button>();

        foreach (Button button in modelButtons)
        {
            button.onClick.AddListener(() => { ActivateModel(button, true); });
        }
        exitButton.GetComponent<Button>().onClick.AddListener(() =>
            { ActivateModel(modelButtons[0], false); });
    }

    void ActivateModel(Button clickedButton, bool stayOpen)
    {
        string clicked = clickedButton.name.Split()[0];

        foreach (Button button in modelButtons)
        {
            bool notClicked = clicked != button.name.Split()[0];

            button.gameObject.SetActive(notClicked ||
                clicked != "ThreeQuarterBoolean" && clicked != "HippocampusBoolean");
            button.interactable = notClicked;
        }

        foreach (GameObject model in models)
        {
            model.SetActive(model.name == clicked);
        }

        ClosePanels(stayOpen);
    }

    public void ClosePanels(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        controlsButton.gameObject.SetActive(!active);

        areasPanel.gameObject.SetActive(false);
        areasButton.gameObject.SetActive(active);

        FimbriaFornixPanel.gameObject.SetActive(false);
        SharedInputOutputPanel.gameObject.SetActive(false);

        ResetAreasPanel(true);
    }

    public void ResetAreasPanel(bool active)
    {
        // get material from ClickOnArea script if attached to the child ("default" GameObject)
        // of the first - and only - active child of brain GameObject
        if (GetComponentInChildren<ClickOnArea>() != null)
        {
            GetComponentInChildren<ClickOnArea>().material.SetColor("_EmissionColor", active ?
                new Color32(0, 0, 0, 0) : new Color32(127, 159, 187, 0));
        }       //             black                        blue
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
