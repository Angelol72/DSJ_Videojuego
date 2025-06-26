using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    // Settings for freeze spell
    [Header("Freeze Spell Settings")]
    public float freezeDuration = 2f;
    public int freezeManaCost = 30;


    // Settings for lightning strike spell
    [Header("Lightning Strike Settings")]
    public int lightningManaCost = 25; // Mana cost for lightning strike
    public int lightningDamage = 1; // Damage dealt by lightning strike

    // EFX
    public GameObject lightningEffectPrefab;

    // Misc
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Usa el nuevo sistema de input
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            LightningStrike();
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            FreezeEnemiesInView();
        }
    }

    public void FreezeEnemiesInView()
    {
        if (!HasEnoughMana(freezeManaCost))
        {
            // add sound effect or visual feedback for insufficient mana
            return;
        }

        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        //Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (Enemy enemy in allEnemies)
        {
            if (enemy.state == Unit.UnitState.Dead)
                continue;

            enemy.ApplyDebuff(DebuffType.Frozen, freezeDuration);
        }

        RemoveMana(freezeManaCost); // Remove mana when freezing enemies
    }

    public void LightningStrike()
    {
        if (!HasEnoughMana(lightningManaCost))
        {
            // add sound effect or visual feedback for insufficient mana
            return;
        }

        Enemy[] allEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Plane[] cameraPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        foreach (Enemy enemy in allEnemies)
        {
            if (enemy.state == Unit.UnitState.Dead)
                continue;

            // enemies in the camera view
            if (GeometryUtility.TestPlanesAABB(cameraPlanes, enemy.GetComponent<CircleCollider2D>().bounds))
            {
                enemy.TakeDamage(lightningDamage);
                if (lightningEffectPrefab != null)
                {
                    GameObject effectInstance = Instantiate(lightningEffectPrefab, enemy.transform.position, Quaternion.identity);
                    // randomize flip renderer to add some variety
                    SpriteRenderer spriteRenderer = effectInstance.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.flipX = Random.value > 0.5f;
                    }

                    Destroy(effectInstance, 1f); // Destroy the effect after 1 second
                }
            }
        }

        RemoveMana(lightningManaCost); // Remove mana when casting lightning strike
    }


    // method to remove mana of a player
    public void RemoveMana(int amount)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ManaController manaController = player.GetComponent<ManaController>();
            if (manaController != null)
            {
                manaController.UseMana(amount);
            }
        }
    }

    // Check if the player has enough mana to cast a spell
    public bool HasEnoughMana(int amount)
    {
        ManaController manaController = GetComponent<ManaController>();
        return manaController != null && manaController.currentMana >= amount;
    }
}