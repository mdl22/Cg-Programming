using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ClickOnArea : MonoBehaviour
{
    [SerializeField] GameObject modelButtons;

    [SerializeField] Button controlExitButton;
    [SerializeField] Button resetButton;
    [SerializeField] Button areasExitButton;
    [SerializeField] Button backButton;

    [SerializeField] Image areasPanel;

    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    [SerializeField] Texture2D mask;
    [SerializeField] Texture2D[] emissionMaps;
    [SerializeField] TextAsset maskTable;

    [SerializeField] float flashPeriod;

    Material material;

    Dictionary<string, string[]> areas = new Dictionary<string, string[]>();
    Dictionary<string, Texture2D> maps = new Dictionary<string, Texture2D>();

    byte emissionIntensity = 0xBF;
    int bitPosition;            // starting from the most significant bit in bit string
    float elapsedTime;
    string bitString = "";

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        SetUpEmissionMaps();

        controlExitButton.GetComponent<Button>().onClick.AddListener(() =>
            { SetEmissionColor(0, true); });
        resetButton.GetComponent<Button>().onClick.AddListener(() =>
            { SetEmissionColor(0, true); });
        areasExitButton.GetComponent<Button>().onClick.AddListener(() =>
            { SetEmissionColor(0, true); });
        backButton.GetComponent<Button>().onClick.AddListener(() =>
            { SetEmissionColor(0, true); });

        foreach (Button button in modelButtons.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() => { SetEmissionColor(0, true); });
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && areasPanel.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isOverUI = EventSystem.current.IsPointerOverGameObject();
            if (!isOverUI && Physics.Raycast(ray, out RaycastHit hit))
            {
                Color32 pixelColour =
                    mask.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

/*int maxValue = pixelColour.g > pixelColour.r ? pixelColour.g : pixelColour.r;
maxValue = pixelColour.b > maxValue ? pixelColour.b : maxValue;*/
                bitString = Convert.ToString(pixelColour.g, 2);
Debug.Log(bitString);
                if (bitString.Split('1').Length == 1)   // no emission map
                {
                    SetEmissionColor(0);

                    GetComponentInParent<PanelButtonController>().ResetAreasPanel(true);
                }
                else
                {
                    string areasKey = (1 << bitString.Length - 1).ToString();

                    if (areas.ContainsKey(areasKey))
                    {
                        material.SetTexture("_EmissionMap", maps[areasKey]);
                        SetEmissionColor(emissionIntensity);
            
                        areaTitleText.text = areas[areasKey][0];
                        areaDescriptionText.text = areas[areasKey][1];
                        if (areas[areasKey][2] != "N/A")
                        {
                            areaDescriptionText.text = string.Concat(areaDescriptionText.text,
                                "\n\n", "Parent region: ", areas[areasKey][2].ToLower());
                        }

                        GetComponentInParent<PanelButtonController>().ResetAreasPanel(false);
                    }
                }
            }
        }

        if (bitString.Split('1').Length > 2)    // area has parent area
        {
            for (int bit = bitPosition; bit < bitString.Length; bit++)
            {
                if (bitString[bit] == '1')
                {
                    material.SetTexture("_EmissionMap",
                        maps[(1 << bitString.Length - 1 - bit).ToString()]);
                    SetEmissionColor(bit == 0 ? (byte) emissionIntensity : (byte) 0x7F);

                    bitPosition = bit;
                    break;
                }
                bitPosition = 0;    // reset as least significant bit is '0' and
            }                       // elapsed time is less than flash period

            if ((elapsedTime += Time.deltaTime) >= flashPeriod)
            {
                elapsedTime = 0;
                if (++bitPosition >= bitString.Length)
                {
                    bitPosition = 0;
                }
            }
        }
    }

    void SetUpEmissionMaps()
    {
        areas.Clear();
        maps.Clear();

        panelListText.text = "";
        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))  // ignore EOF and header
            {
                //        value,                   name, description, parent region
                areas.Add(fields[0], new string[] {fields[2], fields[3], fields[4]});
                maps.Add(fields[0], emissionMaps[maps.Count]);

                panelListText.text = string.Concat(
                    string.Concat(panelListText.text, fields[2]), "\n\n");
            }
        }
    }

    void SetEmissionColor(byte intensity, bool resetBitString = false)
    {
        if (resetBitString)
        {
            bitString = "";
        }
        material.SetColor("_EmissionColor", new Color32(intensity, intensity, intensity, 0));
    }
}
