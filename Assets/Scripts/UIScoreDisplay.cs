using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class UIScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    void Update()
    {
        // Update to reference GameManager for the score
        if (GameManager.Instance != null)
            scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
    }
}
