using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    public Unit.UnitState TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            return Unit.UnitState.Dead; // Return Dead state if health is zero or below
        }
        return GetComponent<Unit>().state; // Return the current state of the unit
    }
}