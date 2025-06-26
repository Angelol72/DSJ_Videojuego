using UnityEngine;

public class Player : Unit
{

    public int score = 0; // Player's score

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        state = UnitState.Dead; // Set the state to Dead
        Debug.Log(unitName + " has died.");
        // Additional logic for player death can be added here
        Time.timeScale = 0f;
    }

    public override void TakeDamage(int damage)
    {
        LifeController lifeManagement = GetComponent<LifeController>();
        // Sound effect on take damage
        GameUISoundController.Instance.PlayCastleTakeDamage();

        // Shake effect
        ShakeUnit shakeUnit = GetComponent<ShakeUnit>();
        if (shakeUnit != null)
        {
            shakeUnit.TriggerShake();
        }

        if (lifeManagement != null)
        {
            Unit.UnitState newState = lifeManagement.TakeDamage(damage);
            if (newState == Unit.UnitState.Dead)
            {
                Die(); // Call Die method if the player is dead
            }
        }
    }

}
