using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    public int currentHealth;

    public bool hurting;
    public float delayTime;

    public event Action<int> OnPlayerHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        hurting = false;
    }

    
    public void TakeDamage(int damage)
    {
        if (hurting == true) return;
        StartCoroutine(TakeDamageRoutine(damage));
    }
    public void Die()
    {
        GameManager.Instance.LevelEnd(false);
        Debug.Log("Player has died");
    }

    IEnumerator TakeDamageRoutine(int damage)
    {
        hurting = true;
        currentHealth -= damage;
        OnPlayerHealthChanged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            hurting = false;
        }

    }

}
