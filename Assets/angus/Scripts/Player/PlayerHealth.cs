using System;
using System.Collections;
using UnityEngine;

public enum DamageType
{
    none = 0,
    Fire = 1,
    Smoke = 2
}

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;

    [Header("Hurt Gate / i-Frame")]
    public bool hurting;
    public float delayTime;

    public AudioClip GetFireClip;
    public AudioClip chokeClip;

    public event Action<DamageType> OnPlayerTakeDamage;
    public event Action<int> OnPlayerHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        hurting = false;
    }



    // 新：帶傷害類型
    public void TakeDamage(int damage, DamageType type)
    {
        if (hurting) return;
        hurting = true;
        StartCoroutine(TakeDamageRoutine(damage, type));
    }

    public void Die()
    {
        GameManager.Instance.LevelEnd(false);
    }

    IEnumerator TakeDamageRoutine(int damage, DamageType type)
    {
        currentHealth -= damage;
        OnPlayerHealthChanged?.Invoke(currentHealth);

        HandleFeedback(type);

        if (currentHealth <= 0)
        {
            Die();
            yield break;
        }

        yield return new WaitForSeconds(delayTime);
        hurting = false;
    }

    void HandleFeedback(DamageType type)
    {
        OnPlayerTakeDamage?.Invoke(type);
        switch (type)
        {
            case DamageType.Fire:
                AudioManager.Instance.PlaySound(GetFireClip);
                break;
            case DamageType.Smoke:
                AudioManager.Instance.PlaySound(chokeClip);
                break;

            default:
                break;
        }
    }
}
