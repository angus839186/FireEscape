using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Hint hintData;
    void OnTriggerStay(Collider other)
    {
        var playerAct = other.GetComponent<PlayerAction>();
        if(playerAct != null)
        {
            if(playerAct.usingRag == false)
            {
                var player = playerAct.GetComponent<PlayerHealth>();
                if(player != null)
                {
                    player.TakeDamage(1);
                    HintManager.Instance.ShowHint(hintData);
                }
            }
        }
    }
}
