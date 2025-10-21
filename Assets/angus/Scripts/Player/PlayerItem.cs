using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerItem : MonoBehaviour
{

    public List<ItemData> items;

    public IReadOnlyList<ItemData> Items => items;

    public event Action OnInventoryChanged;


    public void AddItem(ItemData item)
    {
        if (item == null) return;
        items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemData item)
    {
        if (item == null) return;
        if (items.Remove(item))
            OnInventoryChanged?.Invoke();
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
}
