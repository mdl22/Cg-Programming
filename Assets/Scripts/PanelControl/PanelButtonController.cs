using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelButtonController : MonoBehaviour
{
    GameObject[] models;
    [SerializeField] Button[] modelButtons;

    void Start()
    {
        models = GameObject.FindGameObjectsWithTag("Model");
        for (int i = 1; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }

        for (int i = 0; i < modelButtons.Length; i++)
        {
            int index = i;

            modelButtons[index].GetComponent<Button>().onClick.AddListener(() => { ActivateModel(index); });
        }
    }

    void ActivateModel(int index)
    {
        Debug.Log(index);
        foreach (Button button in modelButtons)
        {
            button.interactable = true;
        }
        modelButtons[index].interactable = false;

        foreach (GameObject model in models)
        {
            if (model.name == modelButtons[index].name.Split()[0])
            {
                model.SetActive(true);
            }
            else
            {
                model.SetActive(false);
            }
        }
    }
}
