using UnityEngine;
using TMPro; // Make sure to include this namespace if using TextMeshPro

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    public int Score { get; private set; } // The current score
    public TextMeshProUGUI scoreText; // Assign this in the inspector

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep the score manager across scenes
        }
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        // Update the score UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Score.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text not assigned in ScoreManager.");
        }
    }
}
