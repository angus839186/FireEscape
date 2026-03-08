using System.Collections;
using UnityEngine;

public class Fire : InteractableItem
{
    public ParticleSystem FireFX;
    public AudioSource FireSound;

    public Hint HydrantHint;

    public bool normalFire;

    public override void Interact(PlayerInteraction player)
    {
        if (!canInteract)
        {
            ShowPlayerTalk(player);
        }
        if (player.TryGetComponent<PlayerAction>(out var playerAction))
        {
            if (normalFire)
            {
                if (playerAction.holdNozzle)
                {
                    playerAction.HydrantNozzle();
                    StartCoroutine(PutOutFire());
                }
                else
                {
                    ShowHint(HydrantHint);
                }
            }
            else
            {
                if (CheckRequirements(player, out var inv))
                {
                    playerAction.Extinguish();
                    StartCoroutine(PutOutFire());
                }
                else
                {
                    ShowHint(hint);
                }
            }
        }
    }

    public override void InteractSound()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        var hp = other.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.TakeDamage(1, DamageType.Fire);
        }
    }

    IEnumerator PutOutFire()
    {
        yield return new WaitForSeconds(3f);
        if (FireFX != null) FireFX.Stop();
        if (FireSound != null) FireSound.Stop();
        var col = GetComponent<BoxCollider>();
        if (col != null) col.enabled = false;
    }
}

