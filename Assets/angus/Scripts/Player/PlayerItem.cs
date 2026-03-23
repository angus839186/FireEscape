using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerItem : MonoBehaviour
{

    public ItemData HeldItem { get; private set; }

    public List<ItemData> items;

    public IReadOnlyList<ItemData> Items => items;

    public event Action OnInventoryChanged;


    public void AddItem(ItemData item)
    {
        if (item == null) return;

        items.Add(item);
        EquipItem(item);
    }

    public void RemoveItem(ItemData item)
    {
        if (item == null) return;
        if (items.Remove(item))
            OnInventoryChanged?.Invoke();
    }

    public void EquipItem(ItemData item)
    {
        if (!HasItem(item)) return;
        HeldItem = item;
        OnInventoryChanged?.Invoke();
    }

    public bool IsHolding(ItemData item)
    {
        return HeldItem != null && HeldItem == item;
    }

    public bool IsHoldingAction(ItemActionType actionType)
    {
        return HeldItem != null && HeldItem.actionType == actionType;
    }

    public bool HasItem(ItemData data) =>
        data != null && items.Any(i => i != null && i == data);

    public bool TryGetItem(ItemData data, out ItemData item)
    {
        item = null;
        if (data == null) return false;
        item = items.FirstOrDefault(i => i != null && i == data);
        return item != null;
    }

    public void CycleHeldItem()
    {
        if (items == null || items.Count == 0)
        {
            HeldItem = null;
            OnInventoryChanged?.Invoke();
            return;
        }

        if (HeldItem == null)
        {
            HeldItem = items[0];
            OnInventoryChanged?.Invoke();
            return;
        }

        int currentIndex = items.IndexOf(HeldItem);
        int nextIndex = (currentIndex + 1) % items.Count;
        HeldItem = items[nextIndex];
        OnInventoryChanged?.Invoke();
    }
}
