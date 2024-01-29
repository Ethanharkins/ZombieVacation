using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    public int Score { get; private set; } // The current score

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        // Optional: Update the score UI immediately here, if required
    }
}

