using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    bool triggerOnce;

    public DialogueData dialogue;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerTalk>();
        if (player != null)
        {
            if (dialogue != null)
            {
                player.Talk(dialogue);
            }
            if (triggerOnce)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
