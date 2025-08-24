using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public Transform holdPoint;

    private UsableItem currentItem;

    public void Use()
    {
        currentItem.Use();
    }
    public void GetItem(UsableItem item)
    {
        currentItem = item;
        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;
    }
    public void DropItem()
    {
        if (currentItem != null)
        {
            currentItem.transform.SetParent(null);
            currentItem = null;
        }
    }
}
