using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    public event Action<DialogueData> OnPlayerTalk;

    public void Talk(DialogueData Dialogue)
    {
        OnPlayerTalk?.Invoke(Dialogue);
    }
}
