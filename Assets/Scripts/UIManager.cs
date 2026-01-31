using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text _timerTxt;

    private void Start()
    {
        TimerManager.Instance.OnTimeUpdate += UpdateTimerDisplay;
    }

    public void UpdateTimerDisplay(float timeValue)
    {
        _timerTxt.text = FormatTime(timeValue);
    }

    public string FormatTime(float timeInSec)
    {
        timeInSec = Mathf.Max(0, timeInSec);

        TimeSpan time = TimeSpan.FromSeconds(timeInSec);
        return time.ToString(@"mm\:ss");
    }

    private void OnDisable()
    {
        TimerManager.Instance.OnTimeUpdate -= UpdateTimerDisplay;
    }
}