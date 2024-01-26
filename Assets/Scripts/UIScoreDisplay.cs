using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class UIScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    void Update()
    {
        if (ScoreManager.Instance != null)
            scoreText.text = "Score: " + ScoreManager.Instance.Score.ToString();
    }
}
