using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    #region Instance

    public static TimerManager Instance;

    #endregion

    [SerializeField] private float _duration = 60f;

    public static event Action<float> OnTimeUpdate;
    public static event Action OnTimeUp;

    private float _time;
    private bool _running;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.OnGameReset += StopTimer;
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
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameReset -= StopTimer;
    }
}
