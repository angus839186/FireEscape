using System.Collections;
using UnityEngine;


public enum FireType
{
    Normal,
    Metal,
    UnPut

}
public class Fire : InteractableItem
{
    public ParticleSystem FireFX;
    public AudioSource FireSound;
    public FireType fireType;

    public DialogueData wrongDialogue;

    public DialogueData unputDialogue;

    [Header("爆炸")]
    public ParticleSystem _explosion;

    public AudioSource audioSource;

    public override void Interact(PlayerInteraction player)
    {
        if (!player.TryGetComponent(out PlayerItem playerItem)) return;
        if (!player.TryGetComponent(out PlayerAction playerAction)) return;

        ItemData heldItem = playerItem.HeldItem;
        if (heldItem == null)
        {
            ShowHint(hint);
            return;
        }

        switch (heldItem.actionType)
        {
            case ItemActionType.Extinguisher:
                TryUseExtinguish(playerAction, player);
                break;

            case ItemActionType.Nozzle:
                TryUseNozzle(playerAction, player);
                break;

            default:
                ShowHint(hint);
                break;
        }
    }

    private void TryUseExtinguish(PlayerAction playerAction, PlayerInteraction player)
    {
        switch (fireType)
        {
            case FireType.Metal:
                playerAction.TryUseExtinguish();
                StartCoroutine(PutOutFire());
                break;

            case FireType.Normal:
                Explode();
                ShowPlayerTalk(player, wrongDialogue);
                break;

            case FireType.UnPut:
                ShowPlayerTalk(player, unputDialogue);
                break;
        }
    }

    private void TryUseNozzle(PlayerAction playerAction, PlayerInteraction player)
    {
        switch (fireType)
        {
            case FireType.Normal:
                playerAction.TryUseNozzle();
                StartCoroutine(PutOutFire());
                break;

            case FireType.Metal:
                Explode();
                ShowPlayerTalk(player, wrongDialogue);
                break;

            case FireType.UnPut:
                ShowPlayerTalk(player, unputDialogue);
                break;
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

    public void Explode()
    {
        if (_explosion != null) _explosion.Play();
        if (audioSource != null) audioSource.Play();
    }
}

