using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighLightObject : MonoBehaviour, IHighlightable
{

    [Header("高亮顏色（Emission 顏色）")]
    public Color highlightColor;
    public float emissionIntensity;

    private Renderer[] renderers;

    private Material[] originalMats;
    private Material[] highlightMats;

    bool isHighLighted;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalMats = new Material[renderers.Length];
        highlightMats = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            originalMats[i] = renderers[i].material;

            var mat = new Material(originalMats[i]);
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", highlightColor * emissionIntensity);
            highlightMats[i] = mat;
        }
    }

    public void HighLight(bool toggle)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = toggle ? highlightMats[i] : originalMats[i];
        }
        GuideLine guideLine = FindFirstObjectByType<GuideLine>();
        if(toggle)
        {
            guideLine.StartGuide(this.transform);
        }
        else
        {
            guideLine.StopGuide();
        }
    }
}

