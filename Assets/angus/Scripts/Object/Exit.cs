using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerController>();
        if(Player != null)
        {
            GameManager.Instance.LevelEnd(true);
        }
    }
}
