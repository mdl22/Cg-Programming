using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelButtonController : MonoBehaviour
{
    [SerializeField] GameObject modelButtonsParent;

    [SerializeField] Button controlsButton;
    [SerializeField] Button ExitButton;
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
        // on exit, activate WholeBrain model, make WholeBrain button non-interactable
        ExitButton.GetComponent<Button>().onClick.AddListener(() =>
            { ActivateModel(modelButtons[0], false); });
    }

    void ActivateModel(Button clickedButton, bool keepPanelsOpen)
    {
        string clicked = clickedButton.name.Split()[0];

        foreach (Button button in modelButtons)
        {
            bool notClicked = clicked != button.name.Split()[0];

            button.gameObject.SetActive(notClicked || !clicked.Contains("Boolean"));
            button.interactable = notClicked;
        }

        foreach (GameObject model in models)
        {
            model.SetActive(model.name == clicked);
        }

        FimbriaFornixPanel.gameObject.SetActive(false);
        SharedInputOutputPanel.gameObject.SetActive(false);

        ClosePanels(keepPanelsOpen);
    }

    public void ClosePanels(bool active)
    {
        controlsPanel.gameObject.SetActive(active);
        controlsButton.gameObject.SetActive(!active);

        areasPanel.gameObject.SetActive(false);
        foreach (Transform child in transform)
        {
           if (child.gameObject.activeSelf)
           {
               areasButton.gameObject.SetActive(
                   active && !child.gameObject.name.Contains("Boolean") );
               break;
           }
        }

        ResetAreasPanel(true);
    }

    public void ResetAreasPanel(bool active)
    {
        panelTitleText.gameObject.SetActive(active);
        panelListText.gameObject.SetActive(active);

        areaTitleText.gameObject.SetActive(!active);
        areaDescriptionText.gameObject.SetActive(!active);

        backButton.gameObject.SetActive(!active);
    }
}
