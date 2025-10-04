using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
        }
    }
}
