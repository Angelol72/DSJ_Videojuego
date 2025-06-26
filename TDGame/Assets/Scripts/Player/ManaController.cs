using UnityEngine;

public class ManaController : MonoBehaviour
{
    public int maxMana = 100; // Maximum mana the player can have
    public int currentMana; // Current mana the player has

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMana = maxMana; // Initialize current mana to maximum mana
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMana(int amount)
    {
        currentMana += amount; // Increase current mana by the specified amount
        if (currentMana > maxMana) // Ensure current mana does not exceed maximum
        {
            currentMana = maxMana;
        }
    }

    public void UseMana(int amount)
    {
        currentMana -= amount; // Decrease current mana by the specified amount
        if (currentMana < 0) // Ensure current mana does not go below zero
        {
            currentMana = 0;
        }
    }
}
