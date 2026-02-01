using UnityEngine;
using UnityEngine.UI;

public class TargetListUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _iconPrefab;

    private void OnEnable()
    {
        ItemManager.OnItemFound += MarkFound;
        GameManager.OnGameReset += ResetList;
        ItemManager.OnItemsSpawned += BuildShowList;
    }

    private void Start()
    {
        BuildList();
    }

    private void BuildList()
    {
        foreach (var data in ItemManager.Instance.ItemPool)
        {
            var icon = Instantiate(_iconPrefab, _container);
            icon.GetComponent<TargetListIconHandler>().UpdateInfo(data);
            icon.name = data.itemId;
        }
    }

    private void BuildShowList()
    {
        foreach (Transform child in _container)
        {
            var exists = ItemManager.Instance.MaxItemPool.Exists(item => item.itemId == child.name);
            child.gameObject.SetActive(exists);
        }
    }

    private void MarkFound(HiddenItemData data)
    {
        var icon = _container.Find(data.itemId);
        icon.GetComponent<TargetListIconHandler>().MarkFound();
        //icon.gameObject.SetActive(false);
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
            BuildShowList();
        }
    }

    private void OnDisable()
    {
        ItemManager.OnItemFound -= MarkFound;
        GameManager.OnGameReset -= ResetList;
        ItemManager.OnItemsSpawned -= BuildShowList;
    }
}
