using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;

    public event Action<bool> InteractHint;

    void Start()
    {
        GameInputManager.Instance.interactInput += TryInteract;
    }

    void OnDisable()
    {
        GameInputManager.Instance.interactInput -= TryInteract;
    }

    void Update()
    {
        if (CanInteract(out IInteractable interactable))
        {
            InteractHint?.Invoke(true);
        }
        else
        {
            InteractHint?.Invoke(false);
        }
    }

    private void TryInteract()
    {
        if(CanInteract(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }
    private bool CanInteract(out IInteractable interactable)
    {
        interactable = null;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            if(interactable != null)
            {
                return true;
            }
        }
        return false;
    }
}
