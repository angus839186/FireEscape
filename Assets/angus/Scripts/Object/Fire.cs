using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : InteractableItem
{
    public ParticleSystem FireFX;
    public AudioSource FireSound;
    public override void Interact(PlayerInteraction player)
    {
        if (CheckRequirements(player, out var inv))
        {
            if (player.TryGetComponent<PlayerAction>(out var playerAction))
            {
                playerAction.ExtinguishFire();
            }
            StartCoroutine(PutOutFire());
        }

    }

    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("被火燙到了");
            player.TakeDamage(1);
        }
    }
    IEnumerator PutOutFire()
    {
        yield return new WaitForSeconds(3f);
        FireFX.Stop();
        FireSound.Stop();
        GetComponent<BoxCollider>().enabled = false;
    }
}
