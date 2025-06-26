using UnityEngine;

public class EnemyPathMovement : MonoBehaviour
{
    public float speed = 0.5f; // Speed of the enemy movement
    private float originalSpeed; // Store the original speed for resetting
    public float enrageSpeedMultiplier = 3f; // Speed multiplier when enraged
    public float enrageDuration = 3f; // Duration of the enrage effect
    public PathData pathData;
    private int currentWaypointIndex = 0; // Index of the current waypoint

    public TextBallon textBallon; // Reference to the TextBallon component

    void Start()
    {
        originalSpeed = speed; // Store the original speed

        // Event handler for enemy death
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.dieEvent += OnEnemyDie; // Subscribe to the die event of the enemy
        }

        // Event handler for wrong answer
        if (textBallon == null)
        {
            return;
        }
        textBallon.onWrongAnswerEvent += (sender, e) =>
        {
            Enrage(); // Enrage the enemy when a wrong answer is selected
        };
    }

    void OnEnemyDie(object sender, System.EventArgs e)
    {
        // Handle enemy death, e.g., stop movement or play death animation
        Debug.Log("Enemy has died.");
        this.enabled = false; // Disable this script to stop movement
    }

    void Update()
    {
        // Check debuffs and whether the enemy can act
        Enemy enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }

        if (enemy.HasDebuff(DebuffType.Frozen))
        {
            // If the enemy is frozen, do not move
            return;
        }

        // move the enemy along the path
        if (pathData == null || pathData.waypoints == null || pathData.waypoints.Count == 0)
        {
            Debug.LogWarning("PathData or waypoints are not set.");
            return;
        }
        if (currentWaypointIndex < pathData.waypoints.Count)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = pathData.waypoints[currentWaypointIndex].position;

            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            transform.position = new Vector3(newPosition.x, newPosition.y, 0);

            // Check if the enemy has reached the current waypoint
            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Debug.Log("All waypoints reached.");
        }
    }

    public void Enrage()
    {
        GameUISoundController.Instance.PlayEnrageEnemy();
        // Increase speed when enraged
        speed *= enrageSpeedMultiplier;
        StartCoroutine(ResetSpeedAfterDelay(enrageDuration)); // Reset speed after 3 seconds
    }

    public void ResetSpeed()
    {
        // Reset speed to original value
        speed = originalSpeed;
    }

    private System.Collections.IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetSpeed(); // Reset speed after the delay
    }
}