using UnityEngine;

public class CarLives : MonoBehaviour
{
    public int lives = 3;
    public LivesUI livesUI; // Assign your LivesUI script here

    public void TakeDamage()
    {
        lives -= 1;
        livesUI.UpdateLives(lives); // Call a method in LivesUI to update the UI

        if (lives <= 0)
        {
            // Handle game over
        }
    }

    // Call TakeDamage from Enemy script or wherever applicable
}
