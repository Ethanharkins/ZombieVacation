using System;
using System.Linq;
using UnityEngine;

[System.Serializable]

public class LeaderboardEntry
{
    public string playerName;
    public int score;
    public float time;
}

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager Instance;
    private LeaderboardEntry[] leaderboardEntries = new LeaderboardEntry[5];

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

    public void TryAddLeaderboardEntry(string playerName, int score, float time)
    {
        LeaderboardEntry newEntry = new LeaderboardEntry { playerName = playerName, score = score, time = time };
        // Determine if the new entry qualifies for the leaderboard
        bool qualifies = false;
        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            if (leaderboardEntries[i] == null || score > leaderboardEntries[i].score || (score == leaderboardEntries[i].score && time < leaderboardEntries[i].time))
            {
                qualifies = true;
                break;
            }
        }

        if (qualifies)
        {
            // Insert new entry in sorted order and remove the last one if necessary
            var tempList = leaderboardEntries.ToList();
            tempList.Add(newEntry);
            tempList.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score) != 0 ? entry2.score.CompareTo(entry1.score) : entry1.time.CompareTo(entry2.time));
            if (tempList.Count > 5) tempList.RemoveAt(5);
            leaderboardEntries = tempList.ToArray();
            SaveLeaderboard();
        }
    }


    private void LoadLeaderboard()
    {
        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            string key = "LeaderboardEntry" + i;
            string value = PlayerPrefs.GetString(key, null);
            if (!string.IsNullOrEmpty(value))
            {
                leaderboardEntries[i] = JsonUtility.FromJson<LeaderboardEntry>(value);
            }
        }
    }

    private void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboardEntries.Length; i++)
        {
            if (leaderboardEntries[i] != null)
            {
                string key = "LeaderboardEntry" + i;
                string value = JsonUtility.ToJson(leaderboardEntries[i]);
                PlayerPrefs.SetString(key, value);
            }
        }
        PlayerPrefs.Save();
    }
}
