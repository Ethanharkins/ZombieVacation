using UnityEngine;
using TMPro;  // Ensure you have the TextMeshPro namespace

public class TimerUp : MonoBehaviour
{
    public float timeElapsed = 0; // Time elapsed
    public TextMeshProUGUI timerText; // Assign this in the Unity Inspector

    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Update the timer display
        if (timerText != null)
        {
            timerText.text = FormatTime(timeElapsed);
        }
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }

    string FormatTime(float time)
    {
        // Format the time into minutes:seconds
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Any other methods you need...
}
