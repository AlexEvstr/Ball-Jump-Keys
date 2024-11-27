using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameWindows _gameWindows;
    private float timeRemaining = 120f;
    public Text timerText;

    public bool isTimerRunning = false;

    private void Start()
    {
        _gameWindows = GetComponent<GameWindows>();
        StartTimer();
    }

    public void StartTimer()
    {
        if (PlayerPrefs.GetInt("CurrentLevel", 1) <= 8)
            isTimerRunning = true;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerText();
                OnTimerEnd();
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimerEnd()
    {
        _gameWindows.OpenLose();
    }
}