using UnityEngine;
using UnityEngine.UI;

public class TargetListIconHandler : MonoBehaviour
{
    [SerializeField] private Color _foundColor;
    [SerializeField] private Image _bg;
    [SerializeField] private Image _iconImg;

    private void Start()
    {
        GameManager.OnGameReset += ResetIcon;
    }

    public void UpdateInfo(HiddenItemData data)
    {
        _bg.color = Color.white;
        _iconImg.sprite = data.icon;
        gameObject.name = data.itemId;
    }

    public void MarkFound()
    {
        _bg.color = _foundColor;
    }

    public void ResetIcon()
    {
        _bg.color = Color.white;
    }

    private void OnDestroy()
    {
        GameManager.OnGameReset -= ResetIcon;
    }
}
