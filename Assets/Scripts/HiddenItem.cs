using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class HiddenItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private HiddenItemData _data;
    [SerializeField] private GameObject _highlightGO;

    public HiddenItemData Data
    {
        get { return _data; }
        set { _data = value; }
    }

    private bool _found;

    private void Start()
    {
        GameManager.OnGameReset += ResetItem;
        _highlightGO.SetActive(false);
    }

    public void MarkFound()
    {
        _found = true;
        //gameObject.SetActive(false);
        _highlightGO.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_found) return;
        InputHandler.Instance.PointerDown(this, eventData);
    }

    private void ResetItem()
    {
        _found = false;
        gameObject.SetActive(true);
        _highlightGO.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnGameReset -= ResetItem;
    }
}
