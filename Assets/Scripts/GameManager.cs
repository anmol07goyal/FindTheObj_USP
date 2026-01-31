using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _itemParent;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _resultTxt;

    private void OnEnable()
    {
        ItemManager.Instance.OnAllItemsFound += Win;
        TimerManager.Instance.OnTimeUp += Lose;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _resultPanel.SetActive(false);
        ItemManager.Instance.SpawnItems(_itemParent);
        TimerManager.Instance.StartTimer();
    }

    public void OnClickRestartBtn()
    {
        TimerManager.Instance.StopTimer();
        ItemManager.Instance.ResetItems();
        StartGame();
    }

    private void Win()
    {
        TimerManager.Instance.StopTimer();
        _resultPanel.SetActive(true);
        _resultTxt.text = "You Win!";
    }

    private void Lose()
    {
        _resultPanel.SetActive(true);
        _resultTxt.text = "Time's Up!";
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnAllItemsFound -= Win;
        TimerManager.Instance.OnTimeUp -= Lose;
    }
}
