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

    [Header("消防栓噴嘴")]
    public GameObject nozzle;
    public AudioSource waterSFX;
    public ParticleSystem waterFX;

    public float nozzleSplashTime;

    public bool holdNozzle;          // 是否拿著水管
    public bool holdExtinguisher;    // 是否拿著滅火器




    void Awake()
    {
        GameInputManager.Instance.useItemInput += useRag;
    }

    void OnDisable()
    {
        GameInputManager.Instance.useItemInput -= useRag;
    }

    public void Extinguish()
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

    public void ToggleInCar(bool toggle)
    {
        PlayerController player = GetComponent<PlayerController>();
        player.inTheCar = toggle;
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

    public void HydrantNozzle()
    {
        if (holdNozzle)
        {
            StartCoroutine(HydrantNozzleCoroutine());
        }
    }

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

    public void ToggleNozzle(bool toggle)
    {
        nozzle.gameObject.SetActive(toggle);
        holdNozzle = toggle;
    }

    // 這個方法用來切換裝備狀態
   public void SwitchEquipment(string type)
   {
    if (type == "Water")
    {
        holdNozzle = true;
        holdExtinguisher = false;
        // 這裡可以加入：顯示水管的模型、隱藏滅火器的模型
    }
    else if (type == "Extinguisher")
    {
        holdNozzle = false;
        holdExtinguisher = true;
        // 這裡可以加入：顯示滅火器的模型、隱藏水管的模型
    }
   }

   void Update()
{
    // 假設按滑鼠右鍵 (1) 來切換裝備
    if (Input.GetMouseButtonDown(1)) 
    {
        SwitchWeapon();
    }
}

public void SwitchWeapon()
{
    // 如果目前拿著水管，就換成滅火器
    if (holdNozzle)
    {
        holdNozzle = false;
        holdExtinguisher = true;
        Debug.Log("已換成：滅火器");
        
        // 這裡建議加入隱藏水管模型、顯示滅火器模型的代碼
        // nozzleModel.SetActive(false);
        // extinguisherModel.SetActive(true);
    }
    // 如果目前拿著滅火器，就換成水管
    else if (holdExtinguisher)
    {
        holdExtinguisher = false;
        holdNozzle = true;
        Debug.Log("已換成：消防栓噴嘴");
        
        // nozzleModel.SetActive(true);
        // extinguisherModel.SetActive(false);
    }
}
}
