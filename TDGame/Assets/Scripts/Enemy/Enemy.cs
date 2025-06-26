using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Enemy : Unit
{
    public event EventHandler dieEvent; // Event to notify when the enemy dies
    public int enemyPoints = 1; // Points awarded to the player when this enemy dies
    public int manaRewarded = 1;
    public TextBallon textBallon; // Reference to a TextBallon component for add event handling
    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackCooldown = 1.5f; // Cooldown time between attacks
    private float lastAttackTime = -Mathf.Infinity; // Time of the last attack
    
    public Animator animator; // Animator for handling animations

    // Sprite renderer
    public SpriteRenderer spriteRenderer;

    // Debuffs applied to the enemy
    private List<Debuff> activeDebuffs = new List<Debuff>();
    public bool canAct = true;

    void Start()
    {
        SubscribeEvents(); // Subscribe to events
    }

    private void Update()
    {
        if (state == UnitState.Dead)
            return;

        canAct = true; // Reset, debuffs can change this

        // Procesa todos los debuffs activos
        for (int i = activeDebuffs.Count - 1; i >= 0; i--)
        {
            Debuff debuff = activeDebuffs[i];
            if (Time.time >= debuff.endTime)
            {
                debuff.OnEnd(this);
                activeDebuffs.RemoveAt(i);
                continue;
            }
            debuff.ApplyEffect(this);
        }

        EnemyPathMovement enemyPathMovement = GetComponent<EnemyPathMovement>();

        if (!canAct)
        {
            if (enemyPathMovement != null)
            {
                enemyPathMovement.enabled = false; // Disable movement if the enemy cannot act
            }
            return;
        }

        if (enemyPathMovement != null)
        {
            enemyPathMovement.enabled = true; // Enable movement if the enemy can act
        }

        // Check if the enemy can attack the player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);
            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                Attack(playerObject.GetComponent<Player>());
            }
        }
    }

    private void SubscribeEvents()
    {
        // Take damage when the correct answer is given
        if (textBallon == null)
            return;

        textBallon.onCorrectAnswerEvent += (sender, e) =>
        {
            TakeDamage(1);
        };
    }

    public override void Die()
    {
        state = UnitState.Dead; // Set the state to Dead

        // Disable the enemy's collider to prevent further interactions
        GetComponent<CircleCollider2D>().enabled = false;

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
                player.score += enemyPoints;
            }

            ManaController manaController = playerObject.GetComponent<ManaController>();
            if (manaController != null)
                manaController.AddMana(manaRewarded);
        }

    }

    public void Attack(Unit target)
    {
        if (state == UnitState.Dead)
            return;

        // Animation or effects can be added here
        animator.SetTrigger("Attack"); // Trigger the attack animation

        if (target != null)
        {
            target.TakeDamage(attackPower);
        }

        lastAttackTime = Time.time; // Update the last attack time
        state = UnitState.Idle; // Set the state back to Idle after attacking
    }

    public override void TakeDamage(int damage)
    {
        LifeController lifeController = GetComponent<LifeController>();
        if (lifeController != null)
        {
            UnitState tmpState = lifeController.TakeDamage(damage);
            if (tmpState == UnitState.Dead)
            {
                Die();
            }
        }

        ShakeUnit shakeUnit = GetComponent<ShakeUnit>();
        if (shakeUnit != null)
        {
            shakeUnit.TriggerShake();
        }
    }

    public void ApplyDebuff(DebuffType type, float duration)
    {
        // Refresh the debuff if it already exists
        Debuff existing = activeDebuffs.Find(d => d.type == type);
        if (existing != null)
        {
            existing.endTime = Time.time + duration;
        }
        else
        {
            switch (type)
            {
                case DebuffType.Frozen:
                    activeDebuffs.Add(new FrozenDebuff(duration, Time.time));
                    break;
                case DebuffType.Burned:
                    activeDebuffs.Add(new BurnedDebuff(duration, Time.time));
                    break;
                // add more debuff types as needed
                default:
                    break;
            }
        }
    }

    // Check if the enemy has a specific debuff
    public bool HasDebuff(DebuffType type)
    {
        return activeDebuffs.Exists(d => d.type == type);
    }

    private System.Collections.IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}