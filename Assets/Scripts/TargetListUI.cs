using UnityEngine;
using UnityEngine.UI;

public class TargetListUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _iconPrefab;

    private void Start()
    {
        ItemManager.Instance.OnItemFound += MarkFound;
        GameManager.OnGameReset += ResetList;
        BuildList();
    }

    private void BuildList()
    {
        foreach (var data in ItemManager.Instance.ItemPool)
        {
            var icon = Instantiate(_iconPrefab, _container);
            icon.GetComponent<Image>().sprite = data.icon;
            icon.name = data.itemId;
        }
    }

    private void MarkFound(HiddenItemData data)
    {
        var icon = _container.Find(data.itemId);
        icon.gameObject.SetActive(false);
        //if (icon)
        //    icon.GetComponent<Image>().color = Color.gray;
    }

    public void ResetList()
    {
        if (_container.childCount == 0)
        {
            BuildList();
            return;
        }

        if (_container.childCount == ItemManager.Instance.ItemPool.Count)
        {
            foreach (Transform child in _container)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnItemFound -= MarkFound;
        GameManager.OnGameReset -= ResetList;
    }
}
