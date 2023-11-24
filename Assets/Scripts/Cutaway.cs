using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Renderer))]
public class Cutaway : MonoBehaviour
{
    public GameObject other;

    [SerializeField] float apertureSpeed = 10;

    void Update()
    {
        if(other != null)
        {
            GetComponent<Renderer>().sharedMaterial.SetFloat("_CutterRadius",
                other.GetComponent<CapsuleCollider>().radius);

            GetComponent<Renderer>().sharedMaterial.SetMatrix("_InverseModelMatrix",
                other.GetComponent<Renderer>().worldToLocalMatrix);

            Vector3 scale = other.transform.localScale;

            float distanceChange = apertureSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.Equals))
            {
                if (scale.x + distanceChange < scale.y / 2)
                {
                    scale.z = scale.x += distanceChange;
                }
                other.transform.localScale = scale;
            }

            if (Input.GetKey(KeyCode.Minus))
            {
                if (scale.x - distanceChange > 0)
                {
                    scale.z = scale.x -= distanceChange;
                }
                other.transform.localScale = scale;
            }
        }
    }
}
