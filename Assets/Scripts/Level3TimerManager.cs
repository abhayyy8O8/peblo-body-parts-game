using UnityEngine;
using TMPro;

public class Level3TimerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float startTimeSeconds = 180f;

    private float remainingTime;
    private bool timerRunning;
    private bool timerStopped;

    void Start()
    {
        remainingTime = startTimeSeconds;
        timerRunning = false;
        timerStopped = false;
        UpdateTimerText();
    }

    void Update()
    {
        if (!timerRunning || timerStopped)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            timerRunning = false;
            timerStopped = true;
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        if (timerRunning || timerStopped)
            return;

        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
        timerStopped = true;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }
}
