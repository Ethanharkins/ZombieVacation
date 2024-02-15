using UnityEngine;
using TMPro;

public class UIScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.Instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
        }
    }
}
