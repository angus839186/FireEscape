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
    public Hint explodeHint;
    public Hint cannotPutOutHint;

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
                ShowHint(explodeHint);
                break;

            case FireType.UnPut:
                ShowHint(cannotPutOutHint);
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
                ShowHint(explodeHint);
                break;

            case FireType.UnPut:
                ShowHint(cannotPutOutHint);
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
}

