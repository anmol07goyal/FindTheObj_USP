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
    //[SerializeField] private int _itemsToFind = 10;
    public List<HiddenItemData> ItemPool => _itemPool;

    private Dictionary<string, HiddenItem> _activeItems = new();

    public event Action<HiddenItemData> OnItemFound;
    public event Action OnAllItemsFound;

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
        if (_activeItems.Count == 0 || _activeItems.Count < itemCount)
        {
            foreach (var item in _activeItems.Values)
                Destroy(item.gameObject);

            _activeItems.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                var data = _itemPool[i];
                var obj = Instantiate(data.prefab, parent);
                //obj.transform.localPosition = Random.insideUnitCircle * 3f;

                var item = obj.GetComponent<HiddenItem>();
                item.Data = data;

                _activeItems.Add(data.itemId, item);
            }
        }
        /*
        else
        {
            // Reactivate existing items
            foreach (var item in _activeItems.Values)
            {
                //item.transform.localPosition = Random.insideUnitCircle * 3f;
                //item.gameObject.SetActive(true);
            }
        }*/
    }

    public void TrySelectItem(HiddenItem item)
    {
        if (!_activeItems.ContainsKey(item.Data.itemId)) 
            return;

        item.MarkFound();
        //_activeItems.Remove(item.Data.itemId);

        OnItemFound?.Invoke(item.Data);
        _itemsFound++;

        if (_itemsFound == _itemPool.Count)
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
