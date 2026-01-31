using UnityEngine;
using UnityEngine.UI;

public class TargetListUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _iconPrefab;

    private void Start()
    {
        ItemManager.Instance.OnItemFound += MarkFound;
        BuildList();
    }

    void BuildList()
    {
        foreach (var data in ItemManager.Instance.ItemPool)
        {
            var icon = Instantiate(_iconPrefab, _container);
            icon.GetComponent<Image>().sprite = data.icon;
            icon.name = data.itemId;
        }
    }

    void MarkFound(HiddenItemData data)
    {
        var icon = _container.Find(data.itemId);
        icon.gameObject.SetActive(false);
        //if (icon)
        //    icon.GetComponent<Image>().color = Color.gray;
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnItemFound -= MarkFound;
    }
}
