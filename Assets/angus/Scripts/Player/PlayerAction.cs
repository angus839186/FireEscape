using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [Header("滅火器")]
    public ParticleSystem powderFX;
    public AudioSource powderSFX;
    public GameObject Extinguisher;
    public float extinguishingDelayTime;

    [Header("抹布")]
    public bool canUseRag;
    public bool usingRag;
    public Animator ragAnimator;

    [Header("水管")]
    public AudioSource waterSFX;
    public ParticleSystem waterFX;
    public float nozzleSplashTime;

    [Header("手機")]
    [SerializeField] private bool canUsePhone;


    [Header("Held Item Objects")]
    [SerializeField] private GameObject phoneObject;
    [SerializeField] private GameObject ragObject;
    [SerializeField] private GameObject extinguisherObject;
    [SerializeField] private GameObject nozzleObject;

    private PlayerItem playerItem;

    void Awake()
    {
        playerItem = GetComponent<PlayerItem>();

        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.useItemInput += OnUseItemInput;
            GameInputManager.Instance.switchHeldItemInput += OnSwitchHeldItemInput;
        }

        if (playerItem != null)
        {
            playerItem.OnInventoryChanged += RefreshHeldItemVisual;
        }
    }

    void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.useItemInput -= OnUseItemInput;
            GameInputManager.Instance.switchHeldItemInput -= OnSwitchHeldItemInput;
        }

        if (playerItem != null)
        {
            playerItem.OnInventoryChanged -= RefreshHeldItemVisual;
        }
    }

    void Start()
    {
        RefreshHeldItemVisual();
    }

    private void OnUseItemInput(bool isPressed)
    {
        PlayerItem playerItem = GetComponent<PlayerItem>();
        if (playerItem == null || playerItem.HeldItem == null) return;

        switch (playerItem.HeldItem.actionType)
        {
            case ItemActionType.Phone:
                if (isPressed)
                {
                    TryUsePhone();
                }
                break;

            case ItemActionType.Rag:
                TryUseRag(isPressed);
                break;
        }
    }
    private void OnSwitchHeldItemInput()
    {
        PlayerItem playerItem = GetComponent<PlayerItem>();
        if (playerItem == null) return;

        playerItem.CycleHeldItem();

        if (playerItem.HeldItem != null)
        {
            Debug.Log("Current Held Item: " + playerItem.HeldItem.ItemName);
        }
    }

    private void TryUsePhone()
    {
        if (!canUsePhone) return;

        Debug.Log("Use phone");
    }

    private void TryUseRag(bool isPressed)
    {
        if (!canUseRag)
        {
            usingRag = false;
            return;
        }

        usingRag = isPressed;

        if (ragAnimator == null) return;

        float startTime = usingRag ? 0f : 1f;
        ragAnimator.SetFloat("holdingRag", usingRag ? 1f : -1f);
        ragAnimator.Play("mugAnime", 0, startTime);
    }

    public void TryUseExtinguish()
    {
        StartCoroutine(ExtinguishFireRoutine());
    }

    public void TryUseNozzle()
    {
        StartCoroutine(HydrantNozzleCoroutine());
    }
    IEnumerator ExtinguishFireRoutine()
    {
        ToggleFreeze(true);
        powderFX.Play();
        powderSFX.Play();
        yield return new WaitForSeconds(extinguishingDelayTime);
        powderFX.Stop();
        powderSFX.Stop();
        ToggleFreeze(false);
    }
    public void ToggleFreeze(bool toggle)
    {
        PlayerController player = GetComponent<PlayerController>();
        player.interacting = toggle;
    }

    public void ToggleInCar(bool toggle)
    {
        PlayerController player = GetComponent<PlayerController>();
        player.inTheCar = toggle;
    }

    // public void DestroyBarricade()
    // {
    //     StartCoroutine(DestroyBarricadeCoroutine());
    // }

    // IEnumerator DestroyBarricadeCoroutine()
    // {
    //     ToggleFreeze(true);
    //     fireAxe.SetActive(true);
    //     fireAxeAnimator.SetTrigger("use");
    //     AudioManager.Instance.PlaySound(fireAxeSfx);
    //     yield return new WaitForSeconds(fireAxeDelayTime);
    //     fireAxe.SetActive(false);
    //     ToggleFreeze(false);
    // }

    IEnumerator HydrantNozzleCoroutine()
    {
        ToggleFreeze(true);
        waterFX.Play();
        waterSFX.Play();
        yield return new WaitForSeconds(nozzleSplashTime);
        waterFX.Stop();
        waterSFX.Stop();
        ToggleFreeze(false);
    }

    public void RefreshHeldItemVisual()
    {
        PlayerItem playerItem = GetComponent<PlayerItem>();
        ItemData heldItem = playerItem != null ? playerItem.HeldItem : null;

        if (phoneObject != null) phoneObject.SetActive(false);
        if (ragObject != null) ragObject.SetActive(false);
        if (extinguisherObject != null) extinguisherObject.SetActive(false);
        if (nozzleObject != null) nozzleObject.SetActive(false);

        if (heldItem == null) return;

        switch (heldItem.actionType)
        {
            case ItemActionType.Phone:
                if (phoneObject != null) phoneObject.SetActive(true);
                break;

            case ItemActionType.Rag:
                if (ragObject != null) ragObject.SetActive(true);
                break;

            case ItemActionType.Extinguisher:
                if (extinguisherObject != null) extinguisherObject.SetActive(true);
                break;

            case ItemActionType.Nozzle:
                if (nozzleObject != null) nozzleObject.SetActive(true);
                break;
        }
    }

}
