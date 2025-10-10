using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Set(ItemData data)
    {
        if (data == null)
        {
            icon.sprite = null;
            icon.enabled = false;
            return;
        }

        icon.enabled = data.ItemSprite != null;
        icon.sprite = data.ItemSprite;
    }
}
