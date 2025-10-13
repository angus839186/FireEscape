using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerItem : MonoBehaviour
{

    public List<UsableItem> items;

    public IReadOnlyList<UsableItem> Items => items;

    public event Action OnInventoryChanged;
    public void AddItem(UsableItem item)
    {
        if (item == null) return;
        items.Add(item);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(UsableItem item)
    {
        if (item == null) return;
        if (items.Remove(item))
            OnInventoryChanged?.Invoke();
    }

    public bool HasItem(ItemData data) =>
        data != null && items.Any(i => i != null && i.itemData == data);

    public bool TryGetItem(ItemData data, out UsableItem item)
    {
        item = null;
        if (data == null) return false;
        item = items.FirstOrDefault(i => i != null && i.itemData == data);
        return item != null;
    }
}
