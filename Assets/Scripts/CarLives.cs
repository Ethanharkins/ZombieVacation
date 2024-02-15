using UnityEngine;

public class CarLives : MonoBehaviour
{
    public int lives = 3; // Starting lives
    public LivesUI livesUI; // Assume you have a LivesUI script for UI updates

    void Start()
    {
        // Optionally initialize lives UI at start
        UpdateLivesUI();
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        UpdateLivesUI();

        if (lives <= 0)
        {
            // Trigger game over logic
            GameManager.Instance.EndGame();
        }
    }

    private void UpdateLivesUI()
    {
        if (livesUI != null)
        {
            livesUI.UpdateLives(lives);
        }
    }
}
