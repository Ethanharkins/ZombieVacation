using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public BossUFO bossUFO;
    public Image healthBarFill;

    void Update()
    {
        if (bossUFO != null)
        {
            healthBarFill.fillAmount = (float)bossUFO.health / 5f;
        }
    }
}
