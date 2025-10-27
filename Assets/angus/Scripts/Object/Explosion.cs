using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Hint hintData;
    public ParticleSystem _explosion;

    public AudioSource audioSource;
    public GameObject Fire;
    [SerializeField] bool TriggerOnce;
    void OnTriggerEnter(Collider other)
    {
        if(TriggerOnce)
        {
            if(audioSource.clip != null)
            {
                audioSource.Play();
            }
            _explosion.Play();
            HintManager.Instance.ShowHint(hintData);
            Fire.SetActive(true);
            TriggerOnce = false;
        }
    }
}
