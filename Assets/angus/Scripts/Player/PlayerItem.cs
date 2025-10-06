using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public Transform holdPoint;

    public UsableItem currentItem;

    void Start()
    {
        GameInputManager.Instance.dropInput += DropItem;
    }

    void OnDisable()
    {
        GameInputManager.Instance.dropInput -= DropItem;
    }

    public void Use()
    {
        currentItem.Use();
    }
    public void GetItem(UsableItem item)
    {
        currentItem = item;
        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 拿在手上時，不受物理影響
        }
    }
    public void DropItem()
    {
        if (currentItem != null)
        {
            // 斷開父物件
            currentItem.transform.SetParent(null);


            Rigidbody rb = currentItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(transform.transform.forward * 1.6f, ForceMode.Impulse); // 向前丟
                rb.drag = 1f;
                rb.angularDrag = 2f;
                // rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }

            currentItem = null;
        }
    }
    public void RemoveItem()
    {
        Destroy(currentItem.gameObject);
        currentItem = null;
    }

    public bool CheckItem(out UsableItem item)
    {
        item = currentItem;
        return item != null;
    }
}
