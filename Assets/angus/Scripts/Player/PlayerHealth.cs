using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    public int currentHealth;

    public bool hurting;
    public float delayTime;

    void Start()
    {
        currentHealth = maxHealth;
        hurting = false;
    }

    void UpdateHealth()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if (hurting == true) return;
        StartCoroutine(TakeDamageRoutine(damage));
    }
    public void Die()
    {
        Debug.Log("Player has died");
    }

    IEnumerator TakeDamageRoutine(int damage)
    {
        hurting = true;
        currentHealth -= damage;
        UpdateHealth();
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
