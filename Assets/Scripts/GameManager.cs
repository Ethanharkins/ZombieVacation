using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Include for TextMeshPro support

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseMenu; // Assign in the Inspector
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for score display

    private bool isGamePaused = false;
    private int circlingEnemiesCount = 0;
    public float baseScrollSpeed = 5f; // Base speed for ObjectScroll
    private float currentScrollSpeed;
    private int score = 0; // The current score

    public int Score
    {
        get { return score; } // Public getter to expose the score
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
        UpdateScrollSpeed();
        UpdateScoreUI(); // Initialize score UI at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void RegisterEnemy()
    {
        circlingEnemiesCount++;
        UpdateScrollSpeed();
    }

    public void UnregisterEnemy()
    {
        circlingEnemiesCount = Mathf.Max(0, circlingEnemiesCount - 1);
        UpdateScrollSpeed();
    }

    void UpdateScrollSpeed()
    {
        currentScrollSpeed = baseScrollSpeed - circlingEnemiesCount;
        ObjectScroll[] scrollers = FindObjectsOfType<ObjectScroll>();
        foreach (var scroller in scrollers)
        {
            scroller.SetScrollSpeed(currentScrollSpeed);
        }
    }

    public float GetCurrentScrollSpeed()
    {
        return currentScrollSpeed;
    }

    // Score management methods
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Toggle pause game
    void TogglePauseGame()
    {
        if (!isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // Pause game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Freeze time
        isGamePaused = true;
    }

    // Resume game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume time
        isGamePaused = false;
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Load Level1
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
