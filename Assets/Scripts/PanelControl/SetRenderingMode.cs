using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderingMode : MonoBehaviour
{
    [SerializeField] Material opaqueMaterial;
    [SerializeField] Material transparentMaterial;

    public void SetMode(int mode)
    {
        Renderer renderer = GetComponent<Renderer>();

        renderer.material = mode == 0 ? opaqueMaterial : transparentMaterial;

        Color alpha = renderer.material.color;
        alpha.a = mode == 0 ? 1 : 0.25f;
        renderer.material.color = alpha;
    }
}
