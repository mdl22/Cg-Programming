using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCutaway : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] Slider windowSlider;

    void Update()
    {
        if(other != null)
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_CutterRadius",
                other.GetComponent<CapsuleCollider>().radius);

            GetComponent<Renderer>().sharedMaterial.SetMatrix("_InverseModelMatrix",
                other.GetComponent<Renderer>().worldToLocalMatrix);

            other.transform.localScale = new Vector3(
                windowSlider.value, other.transform.localScale.y, windowSlider.value);
        }
    }
}
