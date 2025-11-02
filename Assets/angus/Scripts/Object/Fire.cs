using System.Collections;
using UnityEngine;

public class Fire : InteractableItem
{
    public ParticleSystem FireFX;
    public AudioSource FireSound;

    public override void Interact(PlayerInteraction player)
    {
        if (!canInteract) return;
        if (CheckRequirements(player, out var inv))
        {
            if (player.TryGetComponent<PlayerAction>(out var playerAction))
            {
                playerAction.ExtinguishFire();
            }
            StartCoroutine(PutOutFire());
        }
        else
        {
            HintUI.Instance.ShowHint(hint);
        }
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
