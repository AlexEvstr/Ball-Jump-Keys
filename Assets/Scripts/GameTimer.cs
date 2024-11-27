using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameWindows _gameWindows;
    private float timeRemaining = 120f; // Время в секундах (2 минуты)
    public Text timerText; // Ссылка на текст для отображения времени

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
        // Преобразуем оставшееся время в минуты и секунды
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Форматируем строку для отображения
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnTimerEnd()
    {
        _gameWindows.OpenLose();
    }
}
