using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IInteractable
{
    public Hint hint;
    public void Interact()
    {
        HintManager.Instance.ShowHint(hint.hintText);
    }
}
