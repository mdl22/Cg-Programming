using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCutaway : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float size = 5;
    float originalSize;

    [SerializeField] GameObject other;

    void Start()
    {
        originalSize = other.transform.localScale.x;
    }

    void Update()
    {
        if(other != null)
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_CutterRadius",
                other.GetComponent<CapsuleCollider>().radius);

            GetComponent<Renderer>().sharedMaterial.SetMatrix("_InverseModelMatrix",
                other.GetComponent<Renderer>().worldToLocalMatrix);

            float distanceChange = speed * Time.deltaTime;
            Vector3 scale = other.transform.localScale;

            // Enlarge window if "+" key pressed
            if (Input.GetKey(KeyCode.Equals))
            {
                scale.z = scale.x += distanceChange;
                if (scale.x > size)
                {
                    scale.z = scale.x -= distanceChange;
                }
            }
            // Shrink window if "-" key pressed
            else if (Input.GetKey(KeyCode.Minus))
            {
                scale.z = scale.x -= distanceChange;
                if (scale.x < 0)
                {
                    scale.z = scale.x += distanceChange;
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                scale.z = scale.x = originalSize;
            }

            other.transform.localScale = scale;
        }
    }
}
