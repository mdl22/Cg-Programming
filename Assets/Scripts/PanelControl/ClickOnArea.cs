using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void SetUpEmissionMaps()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        Texture2D texture = material.mainTexture as Texture2D;

        areas.Clear();
        maps.Clear();

        panelListText.text = "";
        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))
            {
                areas.Add(fields[0], new string[] {fields[2], fields[3], fields[4]});
                maps.Add(fields[0], emissionMaps[maps.Count]);

                panelListText.text = string.Concat(
                    string.Concat(panelListText.text, fields[2]), "\n\n");
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && areasPanel.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out RaycastHit hit) && !isOverUI)
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Color32 pixelColor =
                    mask.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                Debug.Log((pixelColor.r, pixelColor.g, pixelColor.b));

                string areasKey = pixelColor.g.ToString();
                if (Convert.ToInt32(areasKey) > 64)
                {
                    areasKey = "64";
                }
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

                    GetComponentInParent<PanelButtonController>().ResetAreasPanel(false);
                }
                else
                {
                    GetComponentInParent<PanelButtonController>().ResetAreasPanel(true);
                }
            }
        }
    }
}
