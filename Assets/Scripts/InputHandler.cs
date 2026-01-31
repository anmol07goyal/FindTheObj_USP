using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    #region Instance

    public static InputHandler Instance;

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PointerDown(HiddenItem item, PointerEventData eventData)
    {
        ItemManager.Instance.TrySelectItem(item);
    }
}
