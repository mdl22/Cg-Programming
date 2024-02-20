using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickOnRegion : MonoBehaviour
{
    [SerializeField] Button resetButton;
    [SerializeField] TextMeshProUGUI uText;
    [SerializeField] TextMeshProUGUI vText;
    [SerializeField] TextMeshProUGUI rText;
    [SerializeField] TextMeshProUGUI gText;
    [SerializeField] TextMeshProUGUI bText;
    [SerializeField] TextMeshProUGUI uChannel;
    [SerializeField] TextMeshProUGUI vChannel;
    [SerializeField] TextMeshProUGUI rChannel;
    [SerializeField] TextMeshProUGUI gChannel;
    [SerializeField] TextMeshProUGUI bChannel;

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
                uChannel.text = hit.textureCoord.x.ToString("#0.000000");
                vChannel.text = hit.textureCoord.y.ToString("#0.000000");

                Color pixelColor =
                    texture.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y);

                rChannel.text = pixelColor.r.ToString("#0.000000");
                gChannel.text = pixelColor.g.ToString("#0.000000");
                bChannel.text = pixelColor.b.ToString("#0.000000");
            }
        }
    }

    void ClearText()
    {
        uChannel.text = "";
        vChannel.text = "";
        rChannel.text = "";
        gChannel.text = "";
        bChannel.text = "";
    }
}
