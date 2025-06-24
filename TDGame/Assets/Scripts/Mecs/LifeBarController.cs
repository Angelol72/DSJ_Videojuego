using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    public Slider healthBar;

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            LifeController lifeController = GetComponent<LifeController>();
            if (lifeController != null)
            {
                healthBar.value = (float)lifeController.currentHealth / lifeController.maxHealth; // Update the health bar value
            }
        }
    }
}
