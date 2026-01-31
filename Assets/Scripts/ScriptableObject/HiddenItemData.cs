using UnityEngine;

[CreateAssetMenu(menuName = "Hidden Object/Item Data")]
public class HiddenItemData : ScriptableObject
{
    public string itemId;
    public Sprite icon;
    public GameObject prefab;
}
