using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirCollider : MonoBehaviour
{
    public UsableItem UsableItem;

    void Awake()
    {
        UsableItem.OnItemPicked += CloseAirCollider;
    }
    void OnDisable()
    {
        UsableItem.OnItemPicked -= CloseAirCollider;
    }

    public void CloseAirCollider()
    {
        gameObject.SetActive(false);
    }
}
