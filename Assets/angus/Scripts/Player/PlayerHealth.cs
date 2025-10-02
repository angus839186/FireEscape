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
    }

    void UpdateHealth()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(TakeDamageRoutine(damage));
        UpdateHealth();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Player has died");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "trap")
        {
            TakeDamage(1);
        }
    }

    IEnumerator TakeDamageRoutine(int damage)
    {
        currentHealth -= damage;
        hurting = true;

        yield return new WaitForSeconds(delayTime);
        hurting = false;
        
    }
    

}
