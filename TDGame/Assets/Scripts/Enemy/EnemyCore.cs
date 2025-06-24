using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyCore : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;

    public Slider healthBar; // Reference to a UI slider for health display
    public event EventHandler dieEvent; // Event to notify when the enemy dies
    public int enemyPoints = 1; // Points awarded to the player when this enemy dies

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        // Update health bar if it exists
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth; // Update the health bar value
        }
    }

    void Die()
    {
        // Animation or effects can be added here
        // Start anmation, play sound, etc.

        dieEvent?.Invoke(this, System.EventArgs.Empty); // Notify subscribers that the enemy has died

        StartCoroutine(WaitAndDestroy()); // Wait for a short time before destroying the enemy

        // Score points for the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player player = playerObject.GetComponent<Player>();
            if (player != null)
            {
                player.score += enemyPoints; // Increment player's score
            }
        }

    }

    private System.Collections.IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before destroying
        Destroy(gameObject); // Destroy the enemy game object
    }

}