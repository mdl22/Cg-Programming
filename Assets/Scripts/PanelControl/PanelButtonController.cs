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
        backButton.GetComponent<Button>().onClick.AddListener(() => { ResetAreasPanel(true); });

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

        FimbriaFornixPanel.gameObject.SetActive(false);
        SharedInputOutputPanel.gameObject.SetActive(false);

        ClosePanels(stayOpen);
    }

    public void ClosePanels(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        controlsButton.gameObject.SetActive(!active);

        areasPanel.gameObject.SetActive(false);
        areasButton.gameObject.SetActive(active);

        ResetAreasPanel(true);
    }

    public void ResetAreasPanel(bool active, int dimming = 8)
    {
        // get material from ClickOnArea script if attached to the child ("default" GameObject)
        // of the first - and only - active child of brain GameObject
        if (GetComponentInChildren<ClickOnArea>() != null)
        {
            //Material material = GetComponentInChildren<ClickOnArea>().material;
            //Texture2D texture = material.mainTexture as Texture2D;
//var mip1Data = m_Texture2D.GetPixelData<Color32>(1);

            GetComponentInChildren<ClickOnArea>().material.SetColor(
                "_EmissionColor", new Color32(
                (byte) (127 >> dimming), (byte) (159 >> dimming), (byte) (187 >> dimming), 0));
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
