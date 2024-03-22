using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnRegion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clickText;
    [SerializeField] TextMeshProUGUI areaName;
    [SerializeField] TextMeshProUGUI areaDescription;
    [SerializeField] Button resetButton;

    Texture2D texture;
    [SerializeField] Texture2D mask;
    [SerializeField] Texture2D[] emissionMaps;
    [SerializeField] TextAsset maskTable;

    Dictionary<int, string[]> areas = new Dictionary<int, string[]>();

    void Start()
    {
        texture = GetComponent<Renderer>().material.mainTexture as Texture2D;

        foreach (string line in maskTable.text.Split("\n"))
        {
            string[] fields = line.Split("\t");
            if (line.Length > 0 && Char.IsDigit(fields[0][0]))
            {
                areas.Add(1 << Convert.ToInt32(fields[0]), new string[] {fields[2], fields[3]});
            }
        }
        /*foreach (KeyValuePair<int, string[]> element in areas)
        {
            Debug.Log(element.Key);
            Debug.Log(element.Value[0]);
            Debug.Log(element.Value[1]);
        }*/
//Renderer.material.SetTexture("_MainTex", m_MainTexture);

        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ClearText);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Color32 pixelColor =
                    mask.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                if (areas.ContainsKey(pixelColor.r))
                {
                    areaName.text = areas[pixelColor.r][0];
                    areaDescription.text = areas[pixelColor.r][1];
                    /*rChannel.text = pixelColor.r.ToString();
                    gChannel.text = pixelColor.g.ToString();
                    bChannel.text = pixelColor.b.ToString();
                    Debug.Log(pixelColor.r);
                    Debug.Log(areas[pixelColor.r][0]);
                    Debug.Log(areas[pixelColor.r][1]);*/
                }
                else
                {
                    areaDescription.text = areaName.text = "";
                }
            }
        }
    }

    void ClearText()
    {
        areaName.text = "";
        areaDescription.text = "";
    }
}
