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

    private bool _lastHintVisible = false;

    void Start()
    {
        if (GameInputManager.Instance != null)
            GameInputManager.Instance.interactInput += TryInteract;
    }

    void OnDisable()
    {
        if (GameInputManager.Instance != null)
            GameInputManager.Instance.interactInput -= TryInteract;
    }

    void Update()
    {
        bool hasHitInteractableLayer = HasInteractableInSight();


        if (hasHitInteractableLayer != _lastHintVisible)
        {
            _lastHintVisible = hasHitInteractableLayer;
            InteractHint?.Invoke(hasHitInteractableLayer);
        }
    }

    private void TryInteract()
    {
        if (TryGetInteractable(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }


    private bool HasInteractableInSight()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        return Physics.Raycast(ray, interactionDistance, interactableLayer);
    }


    private bool TryGetInteractable(out IInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            return true;
        }
        return false;
    }
}
