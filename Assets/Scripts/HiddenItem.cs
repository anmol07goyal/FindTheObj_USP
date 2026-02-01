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

    private void Start()
    {
        GameManager.OnGameReset += ResetFound;
    }

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

    private void ResetFound()
    {
        _found = false;
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.OnGameReset -= ResetFound;
    }
}
