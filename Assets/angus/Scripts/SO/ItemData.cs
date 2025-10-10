using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;

    public Sprite ItemSprite;
}
