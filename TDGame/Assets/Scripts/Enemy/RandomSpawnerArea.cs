using UnityEngine;

public class RandomSpawnerArea : MonoBehaviour
{
    public Vector2 size = new Vector2(5f, 5f);
    public GameObject objectToSpawn;
    public PathData pathData;
    public ProblemsData problemsData;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    private float spawnTimer = 0f;

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            Spawn();
            spawnTimer = 0f; // Reset the timer after spawning
        }
    }

    public void Spawn()
    {
        Vector2 center = transform.position;
        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);

        Vector2 spawnPosition = new Vector2(x, y);
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        spawnedObject.GetComponent<EnemyPathMovement>().pathData = pathData;

        // Randomize problem and answer
        TextBallon textBallon = spawnedObject.GetComponent<Enemy>().textBallon;
        if (textBallon != null)
        {
            var generated = ProblemGenerator.GenerateRandomProblem(problemsData);
            textBallon.problemsData = problemsData;
            textBallon.questionText = generated.problemText;
            textBallon.answers = generated.answers;
            textBallon.correctAnswerIndex = generated.correctAnswerIndex;
            textBallon.SetQuestionAndAnswers();
            textBallon.SetOptionListeners();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
