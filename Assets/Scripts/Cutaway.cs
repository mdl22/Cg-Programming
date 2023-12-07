using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutaway : MonoBehaviour
{
    [SerializeField] float windowSpeed = 10;

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

            Vector3 scale = other.transform.localScale;
            float distanceChange = windowSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.Equals))
            {
                if (scale.x + distanceChange < windowSlider.maxValue)
                {
                    windowSlider.value = scale.z = scale.x += distanceChange;
                }
             }

            if (Input.GetKey(KeyCode.Minus))
            {
                if (scale.x - distanceChange > 0)
                {
                    windowSlider.value = scale.z = scale.x -= distanceChange;
                }
            }

            scale.z = scale.x = windowSlider.value;

            other.transform.localScale = scale;
        }
    }
}
