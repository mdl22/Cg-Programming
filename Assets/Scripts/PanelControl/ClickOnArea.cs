using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnArea : MonoBehaviour
{
    [SerializeField] GameObject other;

    [SerializeField] Image areasPanel;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    [HideInInspector] public Material material;
    //Texture2D texture;
    [SerializeField] Texture2D mask;
    [SerializeField] Texture2D[] emissionMaps;
    [SerializeField] TextAsset maskTable;

    Dictionary<string, string[]> areas = new Dictionary<string, string[]>();
    Dictionary<string, Texture2D> maps = new Dictionary<string, Texture2D>();

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        Texture2D texture = material.mainTexture as Texture2D;

        panelListText.text = "";
        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))
            {
                areas.Add(fields[0], new string[] {fields[2], fields[3]});
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
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Color32 pixelColor =
                    mask.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);
                    /*rChannel.text = pixelColor.r.ToString();
                    gChannel.text = pixelColor.g.ToString();
                    bChannel.text = pixelColor.b.ToString();
                    Debug.Log(pixelColor.r);
                    Debug.Log(areas[pixelColor.r][0]);
                    Debug.Log(areas[pixelColor.r][1]);*/

                string areasKey = pixelColor.r.ToString();
                if (areas.ContainsKey(areasKey))
                {
Debug.Log((pixelColor.r, pixelColor.r.ToString()));
                    material.SetTexture("_EmissionMap", maps[areasKey]);

                    areaTitleText.text = areas[areasKey][0];
                    areaDescriptionText.text = areas[areasKey][1];

                    other.GetComponent<Reset>().ResetPanel(false);
                }
                else
                {
                    other.GetComponent<Reset>().ResetPanel();
                }
            }
        }
    }
}
