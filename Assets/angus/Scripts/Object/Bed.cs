using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {

    }

    public void SleepAndWakeUp()
    {
        StartCoroutine(SleepAndWakeUpCoroutine());
    }
    
    IEnumerator SleepAndWakeUpCoroutine()
    {
        yield return null;
    }
}
