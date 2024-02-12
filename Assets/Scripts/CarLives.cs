using UnityEngine;

public class CarLives : MonoBehaviour
{
    public int lives = 3; // Starting lives
    public LivesUI livesUI; // Reference to the LivesUI script to update the UI

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with enemy bullets
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        lives -= 1;
        livesUI.UpdateLives(lives);
        if (lives <= 0)
        {
            GameManager.Instance.EndGame(); // Trigger game over logic
        }
    }

}
