using System;
using UnityEngine;


public abstract class UsableItem : MonoBehaviour, IUsable, IInteractable
{
    [Header("高亮顏色（Emission 顏色）")]
    public Color highlightColor;
    public float emissionIntensity = 2f;

    private Renderer[] renderers;

    private Material[] originalMats;
    private Material[] highlightMats;

    [Space]

    [Header("互動設定")]
    public Hint hintData;
    public ItemData itemData;

    public GameObject airCollider;

    public GameObject NextHighLightObject;

    public bool canPickUp = true;

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
        if (toggle)
        {
            guideLine.StartGuide(this.transform);
        }
        else
        {
            guideLine.StopGuide();
        }
    }

    public virtual void Interact(PlayerInteraction player)
    {
        if (canPickUp)
        {
            player.GetComponent<PlayerItem>().AddItem(this.itemData);
            if (hintData != null)
            {
                HintUI.Instance.ShowHint(this.hintData);
            }
            if (airCollider != null)
            {
                airCollider.SetActive(false);
            }
            this.HighLight(false);
            if(NextHighLightObject != null)
            {
                NextHighLightObject.GetComponent<IInteractable>().HighLight(true);
            }
            this.gameObject.SetActive(false);
        }
    }
}
