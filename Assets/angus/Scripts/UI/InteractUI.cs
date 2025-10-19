using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInteraction playerInteraction;

    public GameObject InteractHint;
    void Awake()
    {
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();
        playerInteraction.InteractHint += ToggleInteractHint;
    }

    void OnDisable()
    {
        playerInteraction.InteractHint -= ToggleInteractHint;
    }

    public void ToggleInteractHint(bool toggle)
    {
        InteractHint.SetActive(toggle);
    }


}
