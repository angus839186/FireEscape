using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerItem playerItem;
    [SerializeField] private Transform contentRoot;
    [SerializeField] private ItemSlotUI slotPrefab;

    private readonly List<ItemSlotUI> _slots = new();

    private void Awake()
    {
        playerItem = FindFirstObjectByType<PlayerItem>();
        playerItem.OnInventoryChanged += Refresh;
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void OnDestroy()
    {
        if (playerItem != null)
            playerItem.OnInventoryChanged -= Refresh;
    }

    private void Clear()
    {
        foreach (var itemslot in _slots)
        {
            if (itemslot != null) Destroy(itemslot.gameObject);
        }
        _slots.Clear();
    }

    private void Refresh()
    {
        if (playerItem == null || contentRoot == null || slotPrefab == null) return;

        Clear();

        var list = playerItem.Items;
        for (int i = 0; i < list.Count; i++)
        {
            var slot = Instantiate(slotPrefab, contentRoot);
            _slots.Add(slot);
            var data = list[i];
            slot.Set(data);
        }
    }
}
