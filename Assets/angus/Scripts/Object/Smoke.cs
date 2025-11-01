using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Hint hintData;
    void OnTriggerStay(Collider other)
    {
        var playerAct = other.GetComponent<PlayerAction>();
        var playerState = other.GetComponent<PlayerController>();
        if (!playerAct.usingRag || !playerState.wantsCrouch)
        {
            var player = playerAct.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                HintUI.Instance.ShowHint(hintData);
            }
        }
    }
}
