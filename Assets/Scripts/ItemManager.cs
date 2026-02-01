using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Instance

    public static ItemManager Instance;

    #endregion

    [Header("Item Pool")]
    [SerializeField] private List<HiddenItemData> _itemPool;
    [SerializeField] private int _maxItemsToFind = 10;

    private List<HiddenItemData> _maxItemPool = new();

    public List<HiddenItemData> ItemPool => _itemPool;
    public List<HiddenItemData> MaxItemPool => _maxItemPool;

    private Dictionary<string, HiddenItem> _hiddenItems = new();

    public static event Action<HiddenItemData> OnItemFound;
    public static event Action OnItemsSpawned;
    public static event Action OnAllItemsFound;

    private int _itemsFound = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.OnGameReset += ResetItems;
    }

    public void SpawnItems(Transform parent)
    {
        var itemCount = _itemPool.Count;
        _itemsFound = 0;

        // Only respawn if there are fewer active items than needed
        if (_hiddenItems.Count == 0 || _hiddenItems.Count < itemCount)
        {
            foreach (var item in _hiddenItems.Values)
                Destroy(item.gameObject);

            _hiddenItems.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                var data = _itemPool[i];
                var obj = Instantiate(data.prefab, parent);

                var item = obj.GetComponent<HiddenItem>();
                item.Data = data;

                _hiddenItems.Add(data.itemId, item);
            }
        }
        // else the items are already spawned, do nothing, just reenable them from their respective scripts

        OnlyShowMaxItems();
    }

    private void OnlyShowMaxItems()
    {
        _maxItemPool.Clear();
        var items = new List<HiddenItem>(_hiddenItems.Values);

        for (int i = 0; i < items.Count; i++)
        {
            int rand = Random.Range(i, items.Count);

            var temp = items[i];
            items[i] = items[rand];
            items[rand] = temp;
        }

        for (int i = 0; i < items.Count; i++)
        {
            var shouldShow = i < _maxItemsToFind;
            items[i].gameObject.SetActive(shouldShow);
            if (shouldShow)
                _maxItemPool.Add(items[i].Data);
        }

        OnItemsSpawned?.Invoke();
    }

    public void TrySelectItem(HiddenItem item)
    {
        if (!_hiddenItems.ContainsKey(item.Data.itemId)) 
            return;

        item.MarkFound();
        //_activeItems.Remove(item.Data.itemId);

        OnItemFound?.Invoke(item.Data);
        _itemsFound++;

        if (_itemsFound == _maxItemsToFind)
            OnAllItemsFound?.Invoke();
    }

    public void ResetItems()
    {
        //foreach (var item in _activeItems.Values)
        //    Destroy(item.gameObject);

        //_activeItems.Clear();
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= ResetItems;
    }
}
