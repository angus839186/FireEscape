using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    public GameObject highlightObject;
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
            if(hintData != null)
            {
                HintUI.Instance.ShowHint(hintData);
            }
            if(highlightObject != null)
            {
                highlightObject.GetComponent<IInteractable>().HighLight(true);
            }
            Fire.SetActive(true);
            TriggerOnce = false;
        }
    }
}
