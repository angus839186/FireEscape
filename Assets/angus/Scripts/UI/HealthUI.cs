using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text healthText;
    void Start()
    {
        PlayerHealth hp = FindObjectOfType<PlayerHealth>();
        hp.OnPlayerHealthChanged += UpdateHealth;
    }

    void OnDisable()
    {
        PlayerHealth hp = FindObjectOfType<PlayerHealth>();
        if(hp != null)
        {
            hp.OnPlayerHealthChanged -= UpdateHealth;
        }
    }

    void UpdateHealth(int newHp)
    {
        healthText.text = newHp.ToString();
    }

}
