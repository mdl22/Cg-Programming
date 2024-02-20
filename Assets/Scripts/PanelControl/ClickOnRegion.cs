using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnRegion : MonoBehaviour
{
    [SerializeField] Texture2D mainTexture;
    [SerializeField] Button resetButton;
    [SerializeField] TextMeshProUGUI uText;
    [SerializeField] TextMeshProUGUI vText;
    [SerializeField] TextMeshProUGUI rText;
    [SerializeField] TextMeshProUGUI gText;
    [SerializeField] TextMeshProUGUI bText;
    [SerializeField] TextMeshProUGUI uCoordinate;
    [SerializeField] TextMeshProUGUI vCoordinate;
    [SerializeField] TextMeshProUGUI rValue;
    [SerializeField] TextMeshProUGUI gValue;
    [SerializeField] TextMeshProUGUI bValue;

    Texture2D texture;

    void Start()
    {
        Button button = resetButton.GetComponent<Button>();
        button.onClick.AddListener(ClearText);

        texture = GetComponent<Renderer>().material.mainTexture as Texture2D;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                uCoordinate.text = hit.textureCoord.x.ToString("#0.000000");
                vCoordinate.text = hit.textureCoord.y.ToString("#0.000000");

                Color pixelColor =
                    texture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                rValue.text = pixelColor.r.ToString("#0.000000");
                gValue.text = pixelColor.g.ToString("#0.000000");
                bValue.text = pixelColor.b.ToString("#0.000000");
            }
        }
    }

    void ClearText()
    {
        uCoordinate.text = "";
        vCoordinate.text = "";
        rValue.text = "";
        gValue.text = "";
        bValue.text = "";
    }
}
