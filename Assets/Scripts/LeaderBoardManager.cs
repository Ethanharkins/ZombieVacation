using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;
    public float time;

    public LeaderboardEntry(string playerName, int score, float time)
    {
        this.playerName = playerName;
        this.score = score;
        this.time = time;
    }
}

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;

    [SerializeField] private List<TextMeshProUGUI> nameTexts; // Assign in Inspector
    [SerializeField] private List<TextMeshProUGUI> scoreTexts; // Assign in Inspector
    [SerializeField] private List<TextMeshProUGUI> timeTexts; // Assign in Inspector

    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    private const int MaxEntries = 4; // Assuming 4 leaderboard slots

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
            LoadLeaderboard();
        }
    }

    public void TryAddEntry(string playerName, int score, float time)
    {
        LeaderboardEntry newEntry = new LeaderboardEntry(playerName, score, time);
        leaderboardEntries.Add(newEntry);

        // Sort and then keep only the top entries
        leaderboardEntries = leaderboardEntries
            .OrderByDescending(entry => entry.score)
            .ThenBy(entry => entry.time)
            .Take(MaxEntries)
            .ToList();

        UpdateLeaderboardUI();
        SaveLeaderboard();
    }

    private void UpdateLeaderboardUI()
    {
        for (int i = 0; i < MaxEntries; i++)
        {
            if (i < leaderboardEntries.Count)
            {
                nameTexts[i].text = leaderboardEntries[i].playerName;
                scoreTexts[i].text = leaderboardEntries[i].score.ToString();
                timeTexts[i].text = FormatTime(leaderboardEntries[i].time);
            }
            else
            {
                // Clear the text if there are less than 4 entries
                nameTexts[i].text = "";
                scoreTexts[i].text = "";
                timeTexts[i].text = "";
            }
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = (int)timeInSeconds / 60;
        int seconds = (int)timeInSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }

    private void SaveLeaderboard()
    {
        // Implement saving logic (e.g., PlayerPrefs, file system)
    }

    private void LoadLeaderboard()
    {
        // Implement loading logic
    }
}
