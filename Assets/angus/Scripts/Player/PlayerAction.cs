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

    [Header("消防斧")]
    public GameObject fireAxe;
    public AudioClip fireAxeSfx;

    public Animator fireAxeAnimator;
    public float fireAxeDelayTime;

    [Header("抹布")]
    public GameObject rag;
    public bool ragisWet;

    public bool usingRag;
    public Animator ragAnimator;



    void Awake()
    {
        GameInputManager.Instance.useItemInput += useRag;
    }

    void OnDisable()
    {
        GameInputManager.Instance.useItemInput -= useRag;
    }

    public void ExtinguishFire()
    {
        StartCoroutine(ExtinguishFireRoutine());
    }
    IEnumerator ExtinguishFireRoutine()
    {
        ToggleFreeze(true);
        Extinguisher.SetActive(true);
        powderFX.Play();
        powderSFX.Play();
        yield return new WaitForSeconds(extinguishingDelayTime);
        powderFX.Stop();
        powderSFX.Stop();
        Extinguisher.SetActive(false);
        ToggleFreeze(false);
    }
    public void ToggleFreeze(bool toggle)
    {
        PlayerController player = GetComponent<PlayerController>();
        player.interacting = toggle;
    }

    public void useRag(bool toggle)
    {
        if (ragisWet == false) return;
        usingRag = toggle;
        float startTime = usingRag ? 0f : 1f;
        ragAnimator.SetFloat("holdingRag", usingRag ? 1f : -1f);
        ragAnimator.Play("mugAnime", 0, startTime);
    }

    public void DestroyBarricade()
    {
        StartCoroutine(DestroyBarricadeCoroutine());
    }

    IEnumerator DestroyBarricadeCoroutine()
    {
        ToggleFreeze(true);
        fireAxe.SetActive(true);
        fireAxeAnimator.SetTrigger("use");
        AudioManager.Instance.PlaySound(fireAxeSfx);
        yield return new WaitForSeconds(fireAxeDelayTime);
        fireAxe.SetActive(false);
        ToggleFreeze(false);
    }
}
