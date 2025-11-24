using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Hint hintData;

    void OnTriggerStay(Collider other)
    {
        var playerAct = other.GetComponent<PlayerAction>();
        var hp = other.GetComponent<PlayerHealth>();

        if (!hp.hurting)
        {
            bool usingRag = playerAct.usingRag;

            if (!usingRag)
            {
                hp.TakeDamage(1, DamageType.Smoke);
                HintUI.Instance.ShowHint(hintData);
            }
        }
    }
}
