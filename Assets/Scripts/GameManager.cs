using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _itemParent;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _resultTxt;

    public static event Action OnGameReset;

    private void Start()
    {
        ItemManager.Instance.OnAllItemsFound += Win;
        TimerManager.Instance.OnTimeUp += Lose;
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
        OnGameReset?.Invoke();
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
