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
    [SerializeField] TextMeshProUGUI uCoordinate;
    [SerializeField] TextMeshProUGUI vCoordinate;

    void Start()
    {
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
                uCoordinate.text = hit.textureCoord.x.ToString("#0.000000");
                vCoordinate.text = hit.textureCoord.y.ToString("#0.000000");
            }
        }
    }

    void ClearText()
    {
        uCoordinate.text = "";
        vCoordinate.text = "";
    }
}
