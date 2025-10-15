using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractAction : MonoBehaviour
{
    [Header("滅火")]
    public ParticleSystem powderFX;
    public GameObject Extinguisher;
    public void ExtinguishFire()
    {
        StartCoroutine(ExtinguishFireRoutine());
    }
    IEnumerator ExtinguishFireRoutine()
    {
        ToggleInteracting(true);
        Extinguisher.SetActive(true);
        powderFX.Play();
        yield return new WaitForSeconds(3.5f);
        powderFX.Stop();
        Extinguisher.SetActive(false);
        ToggleInteracting(false);
    }
    public void ToggleInteracting(bool toggle)
    {
        PlayerController player = GetComponent<PlayerController>();
        player.interacting = toggle;
    }
}
