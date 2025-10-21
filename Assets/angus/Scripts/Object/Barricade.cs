using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : InteractableItem
{
    public int hp;
    public float destroyDelayTime;
    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            if (CheckRequirements(player, out var inv))
            {
                if (player.TryGetComponent<PlayerAction>(out var playerAction))
                {
                    playerAction.DestroyBarricade();
                    StartCoroutine(BarricadeCoroutine());
                }
            }
            else
            {
                HintManager.Instance.ShowHint(hint);
            }

        }
    }
    IEnumerator BarricadeCoroutine()
    {
        canInteract = false;
        hp--;
        if (hp == 0)
        {
            Destroy(gameObject);
            yield break;
        }
        yield return new WaitForSeconds(destroyDelayTime);
        canInteract = true;
    }

}
