using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Hint hintData;

    void OnTriggerStay(Collider other)
    {
        var playerAct = other.GetComponent<PlayerAction>();
        var playerState = other.GetComponent<PlayerController>();
        var hp = other.GetComponent<PlayerHealth>();

        if (!hp.hurting)
        {
            bool usingRag = playerAct.usingRag;
            bool crouching = playerState.wantsCrouch;

            if (!usingRag || !crouching)
            {
                hp.TakeDamage(1, DamageType.Smoke);
                HintUI.Instance.ShowHint(hintData);
            }
        }
    }
}
