using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;

    public Sprite ItemSprite;

    public ItemUseMode useMode;
    public ItemActionType actionType;
}

public enum ItemUseMode
{
    None,
    RightClickOnly,
    TargetInteractOnly,
    Both
}

public enum ItemActionType
{
    None,
    Extinguisher,
    Rag,
    Phone,
    Nozzle,
    WindowBreaker,

}
