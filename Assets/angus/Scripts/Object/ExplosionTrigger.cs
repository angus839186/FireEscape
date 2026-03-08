using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : InteractableItem
{
    public ParticleSystem _explosion;

    public AudioSource audioSource;
    public GameObject Fire;

    public override void InteractSound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (TriggerOnce)
        {
            ShowHint(hint);
            NextHighLight();
            _explosion.Play();
            Fire.SetActive(true);
            TriggerOnce = false;
        }
    }
}
