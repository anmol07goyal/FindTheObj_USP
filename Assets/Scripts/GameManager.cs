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
        ItemManager.OnAllItemsFound += Win;
        TimerManager.OnTimeUp += Lose;
        //StartGame();
    }

    // this is also called from the UI start button
    public void StartGame()
    {
        _resultPanel.SetActive(false);
        ItemManager.Instance.SpawnItems(_itemParent);
    }

    // this is also called from the UI start button
    public void StartTimer()
    {
        TimerManager.Instance.StartTimer();
    }

    public void OnClickRestartBtn()
    {
        OnGameReset?.Invoke();
        StartGame();
        StartTimer();
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
        ItemManager.OnAllItemsFound -= Win;
        TimerManager.OnTimeUp -= Lose;
    }
}
