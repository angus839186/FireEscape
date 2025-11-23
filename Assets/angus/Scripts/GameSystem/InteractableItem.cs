using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{

    [Header("高亮顏色（Emission 顏色）")]
    public Color highlightColor;
    public float emissionIntensity = 2f;

    private Renderer[] renderers;

    private Material[] originalMats;
    private Material[] highlightMats;

    [Space]

    [Header("互動設定")]
    [SerializeField] private ItemData requiredItem;

    public Hint hint;
    public DialogueData dialogue;

    public GameObject airCollider;

    
    public GameObject NextHighLightObject;
    public bool canInteract;

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

    protected bool CheckRequirements(PlayerInteraction player, out PlayerItem playerItem)
    {
        playerItem = null;
        if (player == null) return false;
        if (!player.TryGetComponent(out playerItem)) return false;

        if (!playerItem.HasItem(requiredItem))
            return false;
        return true;
    }

    public abstract void Interact(PlayerInteraction player);

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

