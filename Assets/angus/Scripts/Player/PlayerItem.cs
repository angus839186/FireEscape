using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{

    public List<UsableItem> items;

    public IReadOnlyList<UsableItem> Items => items;

    public event Action OnInventoryChanged;

    // void Start()
    // {
    //     GameInputManager.Instance.dropInput += DropItem;
    // }

    // void OnDisable()
    // {
    //     GameInputManager.Instance.dropInput -= DropItem;
    // }
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
    // public void DropItem()
    // {
    //     if (currentItem != null)
    //     {
    //         // 斷開父物件
    //         currentItem.transform.SetParent(null);


    //         Rigidbody rb = currentItem.GetComponent<Rigidbody>();
    //         if (rb != null)
    //         {
    //             rb.isKinematic = false;
    //             rb.AddForce(transform.transform.forward * 1.6f, ForceMode.Impulse); // 向前丟
    //             rb.drag = 1f;
    //             rb.angularDrag = 2f;
    //             // rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    //         }

    //         currentItem = null;
    //     }
    // }

    // public void RemoveItem()
    // {
    //     Destroy(currentItem.gameObject);
    //     currentItem = null;
    // }
}
