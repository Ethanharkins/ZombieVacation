using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Necessary for TextMeshPro components
using UnityEngine.UI; // Include for UI elements like InputField
using System; // Added for TimeSpan

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject pauseMenu; // Assign in the Inspector
    [SerializeField] private GameObject endScreen; // Assign in the Inspector
    [SerializeField] private TextMeshProUGUI scoreText; // Reference for the score display
    [SerializeField] private TextMeshProUGUI finalScoreText; // For displaying final score on the end screen
    [SerializeField] private TextMeshProUGUI finalTimeText; // For displaying final time on the end screen
    [SerializeField] private TMP_InputField nameInputField; // Adjusted for TMP input field
    [SerializeField] private TextMeshProUGUI leaderboardText; // For displaying the leaderboard

    private bool isGamePaused = false;
    private int score = 0; // Holds the game's score

    // Public property to access the score
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            UpdateScoreUI();
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false); // Ensure the pause menu is not active on start
        if (endScreen != null)
            endScreen.SetActive(false); // Ensure the end screen is not active on start
        UpdateScoreUI(); // Initialize score UI at the start
    }

    void Update()
    {
        // Toggle pause menu on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused; // Toggle the pause state
        if (isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
    }

    public void ResumeGame()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // For use in the Unity Editor
#endif
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void IncreaseScore(int amount)
    {
        Score += amount; // Use the property to automatically update the UI when the score changes
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Score.ToString();
    }

    public void EndGame()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(true); // Show the end screen
            finalScoreText.text = "Final Score: " + Score.ToString();
            finalTimeText.text = "Time: " + FormatTime(Time.timeSinceLevelLoad);
            Time.timeScale = 0f; // Freeze the game
            nameInputField.text = ""; // Ensure TMP input field is reset or set to default name
        }
    }

    public void SubmitScore()
    {
        string playerName = nameInputField.text != "" ? nameInputField.text : "Anonymous";
        LeaderboardManager.Instance.TryAddLeaderboardEntry(playerName, Score, Time.timeSinceLevelLoad);
        UpdateLeaderboardDisplay();
        // Optionally, you can hide the input field and submit button after submission
    }

    private void UpdateLeaderboardDisplay()
    {
        // Implementation depends on your LeaderboardManager's design.
        // This method should retrieve the leaderboard entries and update the UI accordingly.
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
