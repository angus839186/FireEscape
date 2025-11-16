using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCollider : MonoBehaviour
{
    public DialogueData dialogue;
    void OnTriggerEnter(Collider other)
    {
        PlayerTalk player = other.gameObject.GetComponent<PlayerTalk>();
        if (player != null)
        {
            player.Talk(dialogue);
        }
    }
}
