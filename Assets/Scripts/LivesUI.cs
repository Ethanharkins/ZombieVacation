using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public GameObject[] livesIcons; // Array of life icons, assign in the Unity Editor

    public void UpdateLives(int currentLives)
    {
        for (int i = 0; i < livesIcons.Length; i++)
        {
            livesIcons[i].SetActive(i < currentLives);
        }
    }
}
