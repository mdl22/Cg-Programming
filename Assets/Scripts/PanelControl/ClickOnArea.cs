using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ClickOnArea : MonoBehaviour
{
    [SerializeField] Image areasPanel;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    [HideInInspector] public Material material;
    [SerializeField] Texture2D mask;
    [SerializeField] Texture2D[] emissionMaps;
    [SerializeField] TextAsset maskTable;

    Dictionary<string, string[]> areas = new Dictionary<string, string[]>();
    Dictionary<string, Texture2D> maps = new Dictionary<string, Texture2D>();

    void Start()
    {
        SetUpEmissionMaps();
/*for (int i = 1; i < 256; i++)
{
//Debug.Log((System.Convert.ToString(i, 2), System.Convert.ToString(i, 2).Split('1').Length-1));
string num = System.Convert.ToString(i, 2);
int msb = 1 << (int) Mathf.Log(i, 2);
string num2 = System.Convert.ToString(i - msb, 2);
int msb2 = 1 << (int) Mathf.Log(i - msb, 2);
Debug.Log((num, msb, num2, msb2));
}*/
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && areasPanel.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool isOverUI = EventSystem.current.IsPointerOverGameObject();
            if (!isOverUI && Physics.Raycast(ray, out RaycastHit hit))
            {
                Color32 pixelColor =
                    mask.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                int numSetBits = System.Convert.ToString(pixelColor.g, 2).Split('1').Length - 1;
Debug.Log(numSetBits);

                switch (numSetBits)
                {
                    case 0:
                        IdentifyArea(pixelColor);
                        break;
                    case 1:
                        IdentifyArea(pixelColor, 0);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
        }
    }

    void IdentifyArea(Color32 pixelColor)
    {
        GetComponentInParent<PanelButtonController>().ResetAreasPanel(true);
    }

    void IdentifyArea(Color32 pixelColor, int dimming)
    {
        int mostSigBit = 1 << (int) Mathf.Log(pixelColor.g, 2);

        /*int maxValue = pixelColor.g > pixelColor.r ? pixelColor.g : pixelColor.r;
        maxValue = pixelColor.b > maxValue ? pixelColor.b : maxValue;
        string areasKey = maxValue == 0 ?
            "0" : (1 << (int) Mathf.Log(maxValue, 2)).ToString();*/
                string areasKey = pixelColor.g == 0 ? "0" : mostSigBit.ToString();
Debug.Log((pixelColor.r, pixelColor.g, pixelColor.b, "|", areasKey));

        if (areas.ContainsKey(areasKey))
        {
            material.SetTexture("_EmissionMap", maps[areasKey]);

            areaTitleText.text = areas[areasKey][0];
            areaDescriptionText.text = areas[areasKey][1];
            if (areas[areasKey][2] != "N/A")
            {
                areaDescriptionText.text = string.Concat(areaDescriptionText.text,
                    "\n\n", "Parent region: ", areas[areasKey][2].ToLower());
            }

            GetComponentInParent<PanelButtonController>().ResetAreasPanel(false, dimming);
        }
        else
        {
            GetComponentInParent<PanelButtonController>().ResetAreasPanel(true);
        }
    }

    void SetUpEmissionMaps()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        areas.Clear();
        maps.Clear();

        panelListText.text = "";
        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            // ignore: EOF and header
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))
            {
                //        value,                   name, description, parent region
                areas.Add(fields[0], new string[] {fields[2], fields[3], fields[4]});
                maps.Add(fields[0], emissionMaps[maps.Count]);

                panelListText.text = string.Concat(
                    string.Concat(panelListText.text, fields[2]), "\n\n");
            }
        }
    }
}
