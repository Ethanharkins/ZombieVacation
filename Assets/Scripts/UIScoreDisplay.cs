using UnityEngine;
using TMPro;

public class UIScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.Score;
        }
    }
}
