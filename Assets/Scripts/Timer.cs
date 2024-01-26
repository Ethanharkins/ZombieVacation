using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 400; // 400 seconds
    public TextMeshProUGUI timerText; // Change to TextMeshProUGUI
    public GameObject winMenuCanvas; // Assign your win menu canvas here

    private bool isTimerPaused = false;
    private float pauseDuration = 0;

    void Start()
    {
        winMenuCanvas.SetActive(false);
    }

    void Update()
    {
        if (!isTimerPaused)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                TimeUp();
            }
        }
        else
        {
            // If the timer is paused, decrease the pause duration
            if (pauseDuration > 0)
            {
                pauseDuration -= Time.deltaTime;
            }
            else
            {
                // Resume the timer when the pause duration is over
                isTimerPaused = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // TextMeshPro text update
    }

    void TimeUp()
    {
        timeRemaining = 0;
        Time.timeScale = 0; // Stop time
        winMenuCanvas.SetActive(true); // Show win menu
    }

    // Method to get the current time remaining
    public float GetCurrentTime()
    {
        return timeRemaining;
    }

    // Method to pause the timer for a specified duration
    public void PauseTimerForSeconds(float duration)
    {
        isTimerPaused = true;
        pauseDuration = duration;
    }
}
