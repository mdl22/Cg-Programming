using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnRegion : MonoBehaviour
{
    [SerializeField] GameObject other;

    [SerializeField] Image areasPanel;
    [SerializeField] TextMeshProUGUI panelListText;
    [SerializeField] TextMeshProUGUI areaTitleText;
    [SerializeField] TextMeshProUGUI areaDescriptionText;

    public Material material;
    Texture2D texture;
    [SerializeField] Texture2D mask;
    [SerializeField] Texture2D[] emissionMaps;
    [SerializeField] TextAsset maskTable;

    Dictionary<int, string[]> areas = new Dictionary<int, string[]>();
    Dictionary<int, Texture2D> maps = new Dictionary<int, Texture2D>();

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");

        texture = material.mainTexture as Texture2D;

        panelListText.text = "";
        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))
            {
                int rChannel = 1 << Convert.ToInt32(fields[0]);
                areas.Add(rChannel, new string[] {fields[2], fields[3]});
                maps.Add(rChannel, emissionMaps[maps.Count]);

                panelListText.text = string.Concat(
                    string.Concat(panelListText.text, fields[2]), "\n\n");
            }
        }
//Renderer.material.SetTexture("_MainTex", m_MainTexture);
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

                int areasKey = 1 << pixelColor.r;
                if (areas.ContainsKey(areasKey))
                {
                    material.SetTexture("_EmissionMap", emissionMaps[0]);

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
