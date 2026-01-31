using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class HiddenItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private HiddenItemData _data;

    public HiddenItemData Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private bool _found;

    public void MarkFound()
    {
        _found = true;
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_found) return;
        InputHandler.Instance.PointerDown(this, eventData);
    }
}
