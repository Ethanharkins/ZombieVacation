using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Necessary for TextMeshPro components
using System; // Added for TimeSpan

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenu; // Assign in the Inspector
    [SerializeField] private GameObject endScreen; // Assign in the Inspector
    [SerializeField] private TextMeshProUGUI scoreText; // Reference for the score display
    [SerializeField] private TextMeshProUGUI finalScoreText; // For displaying final score on the end screen
    [SerializeField] private TextMeshProUGUI finalTimeText; // For displaying final time on the end screen
    [SerializeField] private TMP_InputField nameInputField; // For player name input
    // [SerializeField] private TextMeshProUGUI leaderboardText; // Removed for leaderboard feature removal
    [SerializeField] private TextMeshProUGUI nameText; // For displaying the player's name in the scene

    private bool isGamePaused = false;
    private int score = 0; // Holds the game's score
    private float survivalTime = 0f; // Tracks survival time
    private int totalEnemies = 0; // Tracks the total number of enemies

    // New fields for bullet upgrades
    [SerializeField] private GameObject[] bulletPrefabs; // Array to hold different bullet prefabs
    private int currentBulletIndex = 0; // Tracks the current bullet type


    // References to audio clips
    public AudioClip ufoDestructionSound;
    public AudioClip gunShotSound;

    private AudioSource audioSource;


    // Public property to access and modify the score
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            UpdateScoreUI();
        }
    }

    // Public property to get the current bullet prefab
    public GameObject CurrentBulletPrefab
    {
        get { return bulletPrefabs[currentBulletIndex]; }
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0f; // Pause game time
            }
            else
            {
                Time.timeScale = 1f; // Resume game time
            }
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
            finalTimeText.text = "Time Survived: " + FormatTime(survivalTime);
            Time.timeScale = 0f; // Freeze the game
            nameInputField.text = ""; // Ensure TMP input field is reset
        }
    }

    public void UpgradeBullet()
    {
        int[] upgradeCosts = new int[] { 15, 30, 45 }; // Costs for each upgrade
        if (currentBulletIndex < bulletPrefabs.Length - 1 && Score >= upgradeCosts[currentBulletIndex])
        {
            Score -= upgradeCosts[currentBulletIndex]; // Deduct cost from score
            currentBulletIndex++; // Upgrade bullet
            Debug.Log("Bullet upgraded to type: " + currentBulletIndex);
            UpdateScoreUI(); // Update the score UI to reflect the deducted points
        }
        else
        {
            Debug.Log("Not enough score for upgrade or max upgrade reached.");
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    // Methods to manage enemy registration
    public void RegisterEnemy()
    {
        totalEnemies++;
        Debug.Log("Enemy Registered. Total enemies: " + totalEnemies);
    }

    public void UnregisterEnemy()
    {
        totalEnemies = Mathf.Max(0, totalEnemies - 1); // Avoid negative values
        Debug.Log("Enemy Unregistered. Remaining enemies: " + totalEnemies);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Initialize or enable pause menu here
    }

}