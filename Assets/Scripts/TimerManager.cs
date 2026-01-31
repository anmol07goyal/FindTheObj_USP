using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Instance

    public static TimerManager Instance;

    #endregion

    [SerializeField] private float _duration = 60f;

    public Action<float> OnTimeUpdate;
    public Action OnTimeUp;

    [SerializeField] private float _time;
    [SerializeField] private bool _running;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartTimer()
    {
        _time = _duration;
        _running = true;
    }

    public void StopTimer()
    {
        _running = false;
    }

    private void Update()
    {
        if (!_running) return;

        _time -= Time.deltaTime;
        OnTimeUpdate?.Invoke(_time);

        if (_time <= 0)
        {
            _running = false;
            OnTimeUp?.Invoke();
            //Debug.Log("Time's up!");
        }
    }
}
