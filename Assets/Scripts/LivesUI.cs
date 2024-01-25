using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public GameObject[] livesIcons; // Assign in the inspector, array of GameObjects representing each life

    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < livesIcons.Length; i++)
        {
            if (i < currentLives)
            {
                livesIcons[i].SetActive(true); // Show the life icon
            }
            else
            {
                livesIcons[i].SetActive(false); // Hide the life icon
            }
        }

        if (currentLives <= 0)
        {
            // Handle Game Over scenario, like showing a Game Over screen
            Debug.Log("Game Over");
        }
    }
}
