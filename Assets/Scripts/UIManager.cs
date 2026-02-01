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
        TimeSpan ts = TimeSpan.FromSeconds(timeValue);

        // Format the TimeSpan into a string (e.g., "00:00:00" for hours:minutes:seconds)
        string formattedTime = ts.ToString(@"mm\:ss");

        _timerTxt.text = formattedTime;
    }

    private void OnDisable()
    {
        TimerManager.Instance.OnTimeUpdate -= UpdateTimerDisplay;
    }
}